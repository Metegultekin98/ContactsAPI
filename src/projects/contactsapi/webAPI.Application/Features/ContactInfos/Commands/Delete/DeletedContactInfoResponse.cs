using Core.Application.Responses;

namespace webAPI.Application.Features.ContactInfos.Commands.Delete;

public class DeletedContactInfoResponse : IResponse
{
    public Guid Id { get; set; }
}