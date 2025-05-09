using System.Net;
using Core.Application.Responses.Concrete;
using MediatR;
using webAPI.Application.Features.Reports.Dtos;
using webAPI.Application.Services.UsersService;

namespace webAPI.Application.Features.Users.Commands.CreateUserUserByLocation;

public class CreateUserUserByLocationCommand : IRequest<CustomResponseDto<NoContentDto>>
{
    public CreateUserUserByLocationCommand()
    {
        CreateReportDto = default!;
    }
    public CreateReportDto CreateReportDto { get; set; }

    public class CreateUserUserByLocationCommandHandler : IRequestHandler<CreateUserUserByLocationCommand, CustomResponseDto<NoContentDto>>
    {
        private readonly IUserService _usersService;

        public CreateUserUserByLocationCommandHandler(IUserService usersService)
        {
            _usersService = usersService;
        }

        public async Task<CustomResponseDto<NoContentDto>> Handle(CreateUserUserByLocationCommand request, CancellationToken cancellationToken)
        {
            CustomResponseDto<NoContentDto> response = await _usersService.CreateReportByLocationAsync(request.CreateReportDto);

            return CustomResponseDto<NoContentDto>.Success((int)HttpStatusCode.OK, response.Data, true);
        }
    }
}