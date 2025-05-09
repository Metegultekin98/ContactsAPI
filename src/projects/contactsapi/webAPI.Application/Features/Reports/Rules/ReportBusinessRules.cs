using System.Text.RegularExpressions;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Domain.Entities;
using webAPI.Application.Features.Reports.Constants;
using webAPI.Application.Features.Reports.Dtos;
using webAPI.Application.Services.Repositories;

namespace webAPI.Application.Features.Reports.Rules;

public class ReportBusinessRules : BaseBusinessRules
{
    private readonly IReportRepository _reportRepository;

    public ReportBusinessRules(IReportRepository reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public Task ReportShouldExistWhenSelected(Report? report)
    {
        if (report == null)
            throw new BusinessException(ReportsBusinessMessages.ReportNotExists);
        return Task.CompletedTask;
    }
}