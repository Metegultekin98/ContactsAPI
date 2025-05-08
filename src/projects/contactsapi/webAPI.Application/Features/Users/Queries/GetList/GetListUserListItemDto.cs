using Core.Application.Dtos;
using static Core.Domain.ComplexTypes.Enums;
using webAPI.Application.Features.Companies.Queries.GetList;

namespace webAPI.Application.Features.Users.Queries.GetList;

public class GetListUserListItemDto : IDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public RecordStatu Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public GetListCompanyListItemDto Company { get; set; } = new();
    public GetListUserListItemDto()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
    }

    public GetListUserListItemDto(Guid id,
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