using Core.Application.Responses;
using static Core.Domain.ComplexTypes.Enums;

namespace webAPI.Application.Features.Users.Commands.Update;

public class UpdatedUserResponse : IResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Guid CompanyId { get; set; }
    public RecordStatu Status { get; set; }

    public UpdatedUserResponse()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
    }

    public UpdatedUserResponse(Guid id, string firstName, string lastName, Guid companyId, RecordStatu status)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        CompanyId = companyId;
        Status = status;
    }
}