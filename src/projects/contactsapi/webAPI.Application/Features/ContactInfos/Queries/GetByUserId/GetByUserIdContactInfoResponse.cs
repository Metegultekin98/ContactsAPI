using Core.Application.Responses;
using static Core.Domain.ComplexTypes.Enums;

namespace webAPI.Application.Features.ContactInfos.Queries.GetByUserId;

public class GetByUserIdContactInfoResponse : IResponse
{
    public Guid Id { get; set; }
    public string Type { get; set; }
    public string Value { get; set; }
    
    public Guid UserId { get; set; }
    public RecordStatu Status { get; set; }
    public DateTime CreatedDate { get; set; }


    public GetByUserIdContactInfoResponse()
    {
        Type = string.Empty;
        Value = string.Empty;
    }

    public GetByUserIdContactInfoResponse(Guid id,
        string type,
        string value,
        Guid userId,
        RecordStatu status,
        DateTime createdDate) : this()
    {
        Id = id;
        Type = type;
        Value = value;
        UserId = userId;
        Status = status;
        CreatedDate = createdDate;
    }
}