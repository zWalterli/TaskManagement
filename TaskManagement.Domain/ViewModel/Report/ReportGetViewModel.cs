namespace TaskManagement.Domain.ViewModel.Report
{
    public class ReportGetViewModel
    {
        public int UserId { get; set; }
        public int AmountDoneTasks { get; set; }
        public List<TaskGetViewModel> Tasks { get; set; }
    }
}
