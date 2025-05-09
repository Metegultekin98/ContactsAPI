using Core.Application.Responses;
using webAPI.Application.Features.Companies.Queries.GetList;
using webAPI.Application.Features.ContactInfos.Queries.GetList;
using static Core.Domain.ComplexTypes.Enums;

namespace webAPI.Application.Features.Users.Queries.GetById;

public class GetByIdUserResponse : IResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public RecordStatu Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public GetListCompanyListItemDto Company { get; set; } = new();
    public IList<GetListContactInfoListItemDto> ContactInfos { get; set; }


    public GetByIdUserResponse()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
    }

    public GetByIdUserResponse(Guid id,
        string firstName,
        string lastName,
        RecordStatu status,
        DateTime createdDate) : this()
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Status = status;
        CreatedDate = createdDate;
    }
}