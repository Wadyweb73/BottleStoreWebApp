namespace BottleStoreWebApp.Models
{
    public class UserListViewModel
    {
        public List<User> Users { get; set; } = new List<User>();
        public User SelectedUser { get; set; } = new User();
    }    
}

