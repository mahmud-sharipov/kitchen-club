namespace KitchenClube.Requests.User
{
    public class UpdateUser
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }        
        public bool IsActive { get; set; }
    }
}
