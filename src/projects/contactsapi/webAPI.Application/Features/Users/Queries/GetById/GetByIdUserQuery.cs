using System.Net;
using AutoMapper;
using Core.Application.Responses.Concrete;
using Core.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using webAPI.Application.Features.Users.Rules;
using webAPI.Application.Services.Repositories;

namespace webAPI.Application.Features.Users.Queries.GetById;

public class GetByIdUserQuery : IRequest<CustomResponseDto<GetByIdUserResponse>>
{
    public Guid Id { get; set; }

    public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQuery, CustomResponseDto<GetByIdUserResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly UserBusinessRules _userBusinessRules;

        public GetByIdUserQueryHandler(IUserRepository userRepository, IMapper mapper, UserBusinessRules userBusinessRules)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userBusinessRules = userBusinessRules;
        }

        public async Task<CustomResponseDto<GetByIdUserResponse>> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetAsync(predicate: b => b.Id == request.Id,
                include: x => x.Include(x => x.ContactInfos)
                    .Include(x => x.Company),
                cancellationToken: cancellationToken);
            await _userBusinessRules.UserShouldBeExistsWhenSelected(user);

            GetByIdUserResponse response = _mapper.Map<GetByIdUserResponse>(user);
            return CustomResponseDto<GetByIdUserResponse>.Success((int)HttpStatusCode.OK, response, true);
        }
    }
}