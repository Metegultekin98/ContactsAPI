using Core.Application.Responses;
using static Core.Domain.ComplexTypes.Enums;


namespace webAPI.Application.Features.Companies.Queries.GetById;

public class GetByIdCompanyResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public RecordStatu Status { get; set; }

    public GetByIdCompanyResponse()
    {
        Name = string.Empty;
        Address = string.Empty;
        PhoneNumber = string.Empty;
    }

    public GetByIdCompanyResponse(Guid id, string name, string address, string phoneNumber, RecordStatu status)
    {
        Id = id;
        Name = name;
        Address = address;
        PhoneNumber = phoneNumber;
        Status = status;
    }
}