using System.Diagnostics;
using BottleStoreWebApp.Models;
using BottleStoreWebApp.Data;

namespace BottleStoreWebApp.Repository {
    public class UserRepository : UserRepositoryInterface
    {
        private readonly ILogger<UserRepository> _logger;
        private readonly BottleStoreDbContext _context;

        public UserRepository(ILogger<UserRepository> logger, BottleStoreDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public User Add(User user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao Registar Usuario!");
            }

            return user;
        }

        public List<User> ListAllUsers()
        {
            return _context.Users.ToList();
        }

        public User Update(User user)
        {
            try
            {
                var existingUser = _context.Users.Find(user.Id);

                if (existingUser != null)
                {
                    existingUser.Name = user.Name;
                    existingUser.PhoneNumber = user.PhoneNumber;
                    existingUser.EmailAddress = user.EmailAddress;
                    existingUser.Password = user.Password;
                    existingUser.UserType = user.UserType;

                    _context.Users.Update(existingUser);
                    _context.SaveChanges();
                    
                    return existingUser;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao Atualizar Usuario!");
            }

            return null;
        }

        public void BlockUser(int id)
        {
            try
            {
                var user = _context.Users.Find(id);

                if (user != null) {
                    user.IsActive = false;
                    _context.Users.Update(user);
                    _context.SaveChanges();
                }
            }
            catch (Exception e) {
                _logger.LogError(e, "Erro ao Bloquear Usuario!");
            }
        }

        public void UnblockUser(int id)
        {
            try
            {
                var user = _context.Users.Find(id);
                
                if (user != null) {
                    user.IsActive   = true;
                    user.IsApproved = true;
                    _context.Users.Update(user);
                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao Desbloquear Usuario!");
            }
        }

        public User? GetById(int id)
        {
            try
            {
                return _context.Users.Find(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao buscar Usuario por Id!");
                return null;
            }
        }

        public User GetByEmail(string email)
        {
            try
            {
                #pragma warning disable CS8603 // Possible null reference return.
                return _context.Users.FirstOrDefault(u => u.EmailAddress == email);
                #pragma warning restore CS8603 // Possible null reference return.
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao buscar Usuario por Email!");
                return null;
            }
        }
    }
}

// "DefaultConnection": "Server=(localdb)\\localhost;Database=bottlestore_db;Trusted_Connection=True;MultipleActiveResultSets=true;Password=Wadypaulino73_mssql-server"
