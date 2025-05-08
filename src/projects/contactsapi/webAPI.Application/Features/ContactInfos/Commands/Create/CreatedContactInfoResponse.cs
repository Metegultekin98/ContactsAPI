using Core.Application.Responses;
using static Core.Domain.ComplexTypes.Enums;

namespace webAPI.Application.Features.ContactInfos.Commands.Create;

public class CreatedContactInfoResponse: IResponse
{
    public Guid Id { get; set; }
    public string Type { get; set; }
    public string Value { get; set; }
    
    public Guid UserId { get; set; }
    public RecordStatu Status { get; set; }

    public CreatedContactInfoResponse()
    {
        Type = string.Empty;
        Value = string.Empty;
    }

    public CreatedContactInfoResponse(Guid id, string type, string value, Guid userId, RecordStatu status)
    {
        Id = id;
        Type = type;
        Value = value;
        UserId = userId;
        Status = status;
    }
}