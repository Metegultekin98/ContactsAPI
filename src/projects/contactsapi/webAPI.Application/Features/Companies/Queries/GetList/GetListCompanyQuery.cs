using System.Net;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Application.Responses.Concrete;
using Core.Domain.Entities;
using Core.Persistence.Paging;
using MediatR;
using webAPI.Application.Services.Repositories;

namespace webAPI.Application.Features.Companies.Queries.GetList;

public class GetListCompanyQuery : IRequest<CustomResponseDto<GetListResponse<GetListCompanyListItemDto>>>
{
    public PageRequest PageRequest { get; set; }

    public GetListCompanyQuery()
    {
        PageRequest = new PageRequest { PageIndex = 0, PageSize = 10 };
    }

    public GetListCompanyQuery(PageRequest pageRequest)
    {
        PageRequest = pageRequest;
    }

    public class GetListCompanyQueryHandler : IRequestHandler<GetListCompanyQuery, CustomResponseDto<GetListResponse<GetListCompanyListItemDto>>>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public GetListCompanyQueryHandler(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<GetListResponse<GetListCompanyListItemDto>>> Handle(GetListCompanyQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Company> companies = await _companyRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListCompanyListItemDto> response = _mapper.Map<GetListResponse<GetListCompanyListItemDto>>(companies);
            return CustomResponseDto<GetListResponse<GetListCompanyListItemDto>>.Success((int)HttpStatusCode.OK, response, true);
        }
    }
}