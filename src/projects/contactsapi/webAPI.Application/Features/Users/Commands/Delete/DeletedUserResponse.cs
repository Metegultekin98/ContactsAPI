using Core.Application.Responses;

namespace webAPI.Application.Features.Users.Commands.Delete;

public class DeletedUserResponse : IResponse
{
    public Guid Id { get; set; }
}