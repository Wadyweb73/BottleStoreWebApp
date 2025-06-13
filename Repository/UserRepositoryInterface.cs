using BottleStoreWebApp.Models;

namespace BottleStoreWebApp.Repository
{
    public interface UserRepositoryInterface
    {
        public User Add(User user);
        public List<User> ListAllUsers();
        public User Update(User user);
        public void BlockUser(int id);
        public void UnblockUser(int id);
        public User GetById(int id);
        public User GetByEmail(string email);
    }
}