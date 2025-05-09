using System.Net;
using AutoMapper;
using Core.Application.Responses.Concrete;
using Core.Domain.Entities;
using MediatR;
using webAPI.Application.Features.Users.Rules;
using webAPI.Application.Services.Repositories;

namespace webAPI.Application.Features.Users.Commands.Create;

public class CreateUserCommand : IRequest<CustomResponseDto<CreatedUserResponse>>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Guid CompanyId { get; set; }

    public CreateUserCommand()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        CompanyId = Guid.Empty;

    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CustomResponseDto<CreatedUserResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly UserBusinessRules _userBusinessRules;

        public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper, UserBusinessRules userBusinessRules)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userBusinessRules = userBusinessRules;
        }

        public async Task<CustomResponseDto<CreatedUserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            User user = _mapper.Map<User>(request);
            
            User createdUser = await _userRepository.AddAsync(user);

            CreatedUserResponse response = _mapper.Map<CreatedUserResponse>(createdUser);
            return CustomResponseDto<CreatedUserResponse>.Success((int)HttpStatusCode.OK, response, true);
        }
    }
}