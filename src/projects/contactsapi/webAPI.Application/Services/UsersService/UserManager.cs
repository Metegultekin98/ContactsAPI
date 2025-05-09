using System.Linq.Expressions;
using System.Net;
using AutoMapper;
using Core.Application.Responses;
using Core.Application.Responses.Concrete;
using Core.Domain.ComplexTypes;
using Core.Domain.Entities;
using Core.Persistence.Paging;
using Core.Utilities.MessageBrokers.RabbitMq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Newtonsoft.Json;
using webAPI.Application.Features.Reports.Dtos;
using webAPI.Application.Features.Reports.Dtos.UserReport;
using webAPI.Application.Services.Repositories;

namespace webAPI.Application.Services.UsersService;

public class UserManager : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IReportRepository _reportRepository;
    private readonly IMessageBrokerHelper _mqQueueHelper;
    private readonly IMapper _mapper;

    public UserManager(IUserRepository userRepository, 
        IReportRepository reportRepository,
        IMessageBrokerHelper mqQueueHelper,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _reportRepository = reportRepository;
        _mqQueueHelper = mqQueueHelper;
        _mapper = mapper;
    }

    public async Task<User?> GetAsync(
        Expression<Func<User, bool>> predicate,
        Func<IQueryable<User>, IIncludableQueryable<User, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        User? user = await _userRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return user;
    }

    public async Task<IPaginate<User>?> GetListAsync(
        Expression<Func<User, bool>>? predicate = null,
        Func<IQueryable<User>, IOrderedQueryable<User>>? orderBy = null,
        Func<IQueryable<User>, IIncludableQueryable<User, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<User> userList = await _userRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return userList;
    }

    public async Task<User> AddAsync(User user)
    {
        User addedUser = await _userRepository.AddAsync(user);

        return addedUser;
    }

    public async Task<User> UpdateAsync(User user)
    {
        User updatedUser = await _userRepository.UpdateAsync(user);

        return updatedUser;
    }

    public async Task<User> DeleteAsync(User user, bool permanent = false)
    {
        User deletedUser = await _userRepository.DeleteAsync(user);

        return deletedUser;
    }
    
    public async Task<CustomResponseDto<NoContentDto>> CreateReportByLocationAsync(CreateReportDto createReportDto)
    {
        IPaginate<User> users = await _userRepository.GetListAsync(
            x => x.ContactInfos.Any(y => y.Type == "Konum" && y.Value == createReportDto.RequestedFor),
            include: x => x.Include(x => x.ContactInfos),
            cancellationToken: default
        );
        
        GetListResponse<UserReportItemDto> mappedUsers =
            _mapper.Map<GetListResponse<UserReportItemDto>>(users);

        Report addedReport = await _reportRepository.AddAsync(new Report
        {
            RequestedFor = createReportDto.RequestedFor,
            RequestedDate = DateTime.UtcNow,
            ReportStatu = Enums.ReportStatu.InProgress
        });
        
        await AddReportToQueue(mappedUsers, addedReport);

        return CustomResponseDto<NoContentDto>.Success((int)HttpStatusCode.OK, new NoContentDto(), true);
    }
    
    private async Task AddReportToQueue(GetListResponse<UserReportItemDto> users, Report addedReport)
    {
        UserReportMessage message = new UserReportMessage
        {
            ReportId = addedReport.Id,
            Items = users.Items,
        };

        await _mqQueueHelper.QueueMessageAsync("report-processing-queue", JsonConvert.SerializeObject(message));
    }
}