using Core.Application.Responses;
using static Core.Domain.ComplexTypes.Enums;

namespace webAPI.Application.Features.Companies.Commands.Update;

public class UpdatedCompanyResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public RecordStatu Status { get; set; }
    
    public UpdatedCompanyResponse()
    {
        Name = string.Empty;
        Address = string.Empty;
        PhoneNumber = string.Empty;
    }
    
    public UpdatedCompanyResponse(Guid id, string name, string address, string phoneNumber, RecordStatu status)
    {
        Id = id;
        Name = name;
        Address = address;
        PhoneNumber = phoneNumber;
        Status = status;
    }
}