using ContactsAPI.Controllers.Base;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Application.Responses.Concrete;
using Microsoft.AspNetCore.Mvc;
using webAPI.Application.Features.Companies.Commands.Create;
using webAPI.Application.Features.Companies.Commands.Delete;
using webAPI.Application.Features.Companies.Commands.Update;
using webAPI.Application.Features.Companies.Queries.GetById;
using webAPI.Application.Features.Companies.Queries.GetList;

namespace ContactsAPI.Controllers;

public class CompaniesController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdCompanyQuery getByIdCompanyQuery)
    {
        CustomResponseDto<GetByIdCompanyResponse> result = await Mediator.Send(getByIdCompanyQuery);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListCompanyQuery getListCompanyQuery = new() { PageRequest = pageRequest };
        CustomResponseDto<GetListResponse<GetListCompanyListItemDto>> result = await Mediator.Send(getListCompanyQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateCompanyCommand createCompanyCommand)
    {
        CustomResponseDto<CreatedCompanyResponse> result = await Mediator.Send(createCompanyCommand);
        return Created(uri: "", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateCompanyCommand updateCompanyCommand)
    {
        CustomResponseDto<UpdatedCompanyResponse> result = await Mediator.Send(updateCompanyCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteCompanyCommand deleteCompanyCommand)
    {
        CustomResponseDto<DeletedCompanyResponse> result = await Mediator.Send(deleteCompanyCommand);
        return Ok(result);
    }
}