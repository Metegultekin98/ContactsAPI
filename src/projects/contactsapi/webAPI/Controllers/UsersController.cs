using ContactsAPI.Controllers.Base;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Application.Responses.Concrete;
using Microsoft.AspNetCore.Mvc;
using webAPI.Application.Features.Users.Commands.Create;
using webAPI.Application.Features.Users.Commands.Delete;
using webAPI.Application.Features.Users.Commands.Update;
using webAPI.Application.Features.Users.Queries.GetById;
using webAPI.Application.Features.Users.Queries.GetList;

namespace ContactsAPI.Controllers;

public class UsersController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdUserQuery getByIdUserQuery)
    {
        CustomResponseDto<GetByIdUserResponse> result = await Mediator.Send(getByIdUserQuery);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListUserQuery getListUserQuery = new() { PageRequest = pageRequest };
        CustomResponseDto<GetListResponse<GetListUserListItemDto>> result = await Mediator.Send(getListUserQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateUserCommand createUserCommand)
    {
        CustomResponseDto<CreatedUserResponse> result = await Mediator.Send(createUserCommand);
        return Created(uri: "", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateUserCommand updateUserCommand)
    {
        CustomResponseDto<UpdatedUserResponse> result = await Mediator.Send(updateUserCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteUserCommand deleteUserCommand)
    {
        CustomResponseDto<DeletedUserResponse> result = await Mediator.Send(deleteUserCommand);
        return Ok(result);
    }
}
