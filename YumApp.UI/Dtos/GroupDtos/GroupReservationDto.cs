namespace YumApp.UI.Dtos.GroupDtos
{
    public class GroupReservationDto
    {
        public int GroupReservationId { get; set; }
        public string CustomerName { get; set; }
        public string GroupTitle { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime LastProcessDate { get; set; }
        public string Priority { get; set; }
        public string Details { get; set; }
        public int? PersonCount { get; set; }
        public string? Email { get; set; }
        public string ReservationStatus { get; set; }
    }
}
