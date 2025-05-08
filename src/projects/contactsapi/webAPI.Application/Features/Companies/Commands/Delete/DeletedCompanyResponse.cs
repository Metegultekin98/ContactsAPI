using Core.Application.Responses;

namespace webAPI.Application.Features.Companies.Commands.Delete;

public class DeletedCompanyResponse : IResponse
{
    public Guid Id { get; set; }
}