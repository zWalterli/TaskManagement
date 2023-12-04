using TaskManagement.Domain.ViewModel.Report;
using TaskManagement.Domain.ViewModel.Result;

namespace TaskManagement.Domain.Interface.Application
{
    public interface IReportApplication
    {
        Task<Result<IList<ReportGetViewModel>>> GetAmountTasksFinishedPerUserInAMounth();
    }
}
