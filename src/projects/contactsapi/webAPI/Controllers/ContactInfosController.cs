using ContactsAPI.Controllers.Base;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Application.Responses.Concrete;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Mvc;
using webAPI.Application.Features.ContactInfos.Commands.Create;
using webAPI.Application.Features.ContactInfos.Commands.Delete;
using webAPI.Application.Features.ContactInfos.Commands.Update;
using webAPI.Application.Features.ContactInfos.Queries.GetById;
using webAPI.Application.Features.ContactInfos.Queries.GetList;
using webAPI.Application.Features.ContactInfos.Queries.GetListByDynamic;

namespace ContactsAPI.Controllers;

public class ContactInfosController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdContactInfoQuery getByIdContactInfoQuery)
    {
        CustomResponseDto<GetByIdContactInfoResponse> result = await Mediator.Send(getByIdContactInfoQuery);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListContactInfoQuery getListContactInfoQuery = new() { PageRequest = pageRequest };
        CustomResponseDto<GetListResponse<GetListContactInfoListItemDto>> result = await Mediator.Send(getListContactInfoQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateContactInfoCommand createContactInfoCommand)
    {
        CustomResponseDto<CreatedContactInfoResponse> result = await Mediator.Send(createContactInfoCommand);
        return Created(uri: "", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateContactInfoCommand updateContactInfoCommand)
    {
        CustomResponseDto<UpdatedContactInfoResponse> result = await Mediator.Send(updateContactInfoCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteContactInfoCommand deleteContactInfoCommand)
    {
        CustomResponseDto<DeletedContactInfoResponse> result = await Mediator.Send(deleteContactInfoCommand);
        return Ok(result);
    }
    
    [HttpPost("GetList/ByDynamic")]
    public async Task<ActionResult<CustomResponseDto<GetListResponse<GetListByDynamicContactInfoListItemDto>>>> GetListByDynamic([FromQuery] PageRequest pageRequest, [FromBody] DynamicQuery? dynamicQuery = null)
    {
        GetListByDynamicContactInfoQuery getListByDynamicContactInfoQuery = new()
        {
            PageRequest = pageRequest,
            DynamicQuery = dynamicQuery,
        };

        CustomResponseDto<GetListResponse<GetListByDynamicContactInfoListItemDto>> result =
            await Mediator.Send(getListByDynamicContactInfoQuery);
        return Ok(result);
    }
}
