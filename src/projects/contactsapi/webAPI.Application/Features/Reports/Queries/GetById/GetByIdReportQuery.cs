using System.Net;
using AutoMapper;
using Core.Application.Responses.Concrete;
using Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using webAPI.Application.Features.Reports.Rules;
using webAPI.Application.Services.Repositories;

namespace webAPI.Application.Features.Reports.Queries.GetById;

public class GetByIdReportQuery : IRequest<CustomResponseDto<GetByIdReportResponse>>
{
    public Guid Id { get; set; }

    public class GetByIdReportQueryHandler : IRequestHandler<GetByIdReportQuery, CustomResponseDto<GetByIdReportResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IReportRepository _reportRepository;
        private readonly ReportBusinessRules _reportBusinessRules;

        public GetByIdReportQueryHandler(IReportRepository reportRepository,IMapper mapper, ReportBusinessRules reportBusinessRules)
        {
            _mapper = mapper;
            _reportRepository = reportRepository;
            _reportBusinessRules = reportBusinessRules;
        }

        public async Task<CustomResponseDto<GetByIdReportResponse>> Handle(GetByIdReportQuery request, CancellationToken cancellationToken)
        {
            Report? report = await _reportRepository.GetAsync(predicate: r => r.Id == request.Id, 
                include: x => x.Include(x => x.Users)
                    .Include(x => x.Users).ThenInclude(x => x.ContactInfos),
                cancellationToken: cancellationToken);
            await _reportBusinessRules.ReportShouldExistWhenSelected(report);

            GetByIdReportResponse response = _mapper.Map<GetByIdReportResponse>(report);

            return CustomResponseDto<GetByIdReportResponse>.Success((int)HttpStatusCode.OK, response, true);
        }
    }
}