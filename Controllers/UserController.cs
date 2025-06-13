using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BottleStoreWebApp.Data;
using BottleStoreWebApp.Models;
using BottleStoreWebApp.Repository;

namespace BottleStoreWebApp.Controllers 
{
    public class UserController : Controller 
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserRepositoryInterface _userRepositoryInterface;
  
        public UserController(UserRepositoryInterface userRepositoryInterface, ILogger<UserController> logger) {
            _logger                  = logger;
            _userRepositoryInterface = userRepositoryInterface;
        }

        [HttpPost]
        public IActionResult Create(User user) {

            _userRepositoryInterface.Add(user);
            return RedirectToAction("ListUsers", "Home");

        } 

        [HttpPost]
        public IActionResult Edit(UserListViewModel userListViewModel)
        {
            User user = userListViewModel.SelectedUser;
            User currentUser = _userRepositoryInterface.GetById(user.Id);

            if (user.Password == null || user.Password.Trim() == "")
            {
                user.Password = currentUser.Password;
            }

            if (user == null || user.Id <= 0)
            {
                _logger.LogError("User not found or invalid ID.");
            }
            else
            {
                User updatedUser = _userRepositoryInterface.Update(user);

                if (updatedUser != null)
                {
                    _logger.LogInformation($"User with ID {user.Id} updated successfully.");
                }
                else
                {
                    _logger.LogError($"Failed to update user with ID {user.Id}.");
                }
            }

            return RedirectToAction("ListUsers", "Home");
        }

        [HttpPost]
        public IActionResult BlockUser(int id)
        {
            User user = _userRepositoryInterface.GetById(id);

            if (user == null)
            {
                _logger.LogError($"User with ID {id} not found.");
                return NotFound();
            }

            if (user.IsActive)
            {
                _userRepositoryInterface.BlockUser(id);
                _logger.LogInformation($"User with ID {id} has been blocked.");
            }

            return RedirectToAction("ListUsers", "Home");
        }

        [HttpPost]
        public IActionResult UnBlockUser(int id)
        {
            User user = _userRepositoryInterface.GetById(id);

            if (user == null)
            {
                _logger.LogError($"User with ID {id} not found.");
                return NotFound();
            }

            if (!user.IsActive) {
                _userRepositoryInterface.UnblockUser(id);   
                _logger.LogInformation($"User with ID {id} has been unblocked.");
            }

            return RedirectToAction("ListBlockedUsers", "Home");
        }
    }
}
