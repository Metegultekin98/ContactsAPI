using ContactsAPI.Controllers.Base;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Application.Responses.Concrete;
using Microsoft.AspNetCore.Mvc;
using webAPI.Application.Features.Reports.Queries.GetById;
using webAPI.Application.Features.Reports.Queries.GetList;

namespace ContactsAPI.Controllers;

public class ReportsController : BaseController
{
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdReportQuery getByIdReportQuery)
    {
        CustomResponseDto<GetByIdReportResponse> result = await Mediator.Send(getByIdReportQuery);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListReportQuery getListReportQuery = new() { PageRequest = pageRequest };
        CustomResponseDto<GetListResponse<GetListReportListItemDto>> result = await Mediator.Send(getListReportQuery);
        return Ok(result);
    }
}
