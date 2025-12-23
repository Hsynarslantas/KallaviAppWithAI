namespace YumApp.UI.Models
{
    public class RevenueChartViewModel
    {
        public List<string> Labels { get; set; }
        public List<int> Income { get; set; }
        public List<int> Expense { get; set; }
        public int TotalReservations { get; set; }
        public int ApprovedReservations { get; set; }
        public int OldRezervations { get; set; }
    }
}
