using System.Net;
using AutoMapper;
using Core.Application.Responses.Concrete;
using Core.Domain.Entities;
using MediatR;
using webAPI.Application.Features.Users.Rules;
using webAPI.Application.Services.Repositories;

namespace webAPI.Application.Features.Users.Commands.Update;

public class UpdateUserCommand : IRequest<CustomResponseDto<UpdatedUserResponse>>
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Guid CompanyId { get; set; }

    public UpdateUserCommand()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        CompanyId = Guid.Empty;
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, CustomResponseDto<UpdatedUserResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly UserBusinessRules _userBusinessRules;

        public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper, UserBusinessRules userBusinessRules)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userBusinessRules = userBusinessRules;
        }

        public async Task<CustomResponseDto<UpdatedUserResponse>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetAsync(predicate: u => u.Id == request.Id, cancellationToken: cancellationToken);
            await _userBusinessRules.UserShouldBeExistsWhenSelected(user);
            user = _mapper.Map(request, user);

            await _userRepository.UpdateAsync(user);

            UpdatedUserResponse response = _mapper.Map<UpdatedUserResponse>(user);
            return CustomResponseDto<UpdatedUserResponse>.Success((int)HttpStatusCode.OK, response, true);
        }
    }
}