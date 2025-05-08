using Core.Application.Responses;
using static Core.Domain.ComplexTypes.Enums;

namespace webAPI.Application.Features.Companies.Commands.Create;

public class CreatedCompanyResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public RecordStatu Status { get; set; }
    
    public CreatedCompanyResponse()
    {
        Name = string.Empty;
        Address = string.Empty;
        PhoneNumber = string.Empty;
    }
    
    public CreatedCompanyResponse(Guid id, string name, string address, string phoneNumber, RecordStatu status)
    {
        Id = id;
        Name = name;
        Address = address;
        PhoneNumber = phoneNumber;
        Status = status;
    }
}