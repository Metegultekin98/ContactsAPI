using System.Net;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Application.Responses.Concrete;
using Core.Domain.Entities;
using Core.Persistence.Paging;
using MediatR;
using webAPI.Application.Services.Repositories;

namespace webAPI.Application.Features.Reports.Queries.GetList;

public class GetListReportQuery : IRequest<CustomResponseDto<GetListResponse<GetListReportListItemDto>>>
{
    public GetListReportQuery()
    {
        PageRequest = default!;
    }
    public PageRequest PageRequest { get; set; }

    public class GetListReportQueryHandler : IRequestHandler<GetListReportQuery, CustomResponseDto<GetListResponse<GetListReportListItemDto>>>
    {
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;

        public GetListReportQueryHandler(IReportRepository reportRepository, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<GetListResponse<GetListReportListItemDto>>> Handle(GetListReportQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Report> reports = await _reportRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListReportListItemDto> response = _mapper.Map<GetListResponse<GetListReportListItemDto>>(reports);
            return CustomResponseDto<GetListResponse<GetListReportListItemDto>>.Success((int)HttpStatusCode.OK, response, true);
        }
    }
}