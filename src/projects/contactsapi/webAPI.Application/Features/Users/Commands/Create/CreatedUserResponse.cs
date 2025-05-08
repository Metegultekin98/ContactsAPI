using Core.Application.Responses;
using static Core.Domain.ComplexTypes.Enums;

namespace webAPI.Application.Features.Users.Commands.Create;

public class CreatedUserResponse: IResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Guid CompanyId { get; set; }
    public RecordStatu Status { get; set; }

    public CreatedUserResponse()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
    }

    public CreatedUserResponse(Guid id, string firstName, string lastName, Guid companyId, RecordStatu status)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        CompanyId = companyId;
        Status = status;
    }
}