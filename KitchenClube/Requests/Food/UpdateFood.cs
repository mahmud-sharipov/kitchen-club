namespace KitchenClube.Requests.Food
{
    public class UpdateFood
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
