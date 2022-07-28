namespace KitchenClube.Requests.Menu
{
    public class CreateMenu
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public MenuStatus Status { get; set; }
    }
}
