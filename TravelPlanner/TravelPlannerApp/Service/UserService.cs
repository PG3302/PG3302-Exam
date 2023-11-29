using TravelDatabase.Models;
using TravelDatabase.Repositories;

namespace TravelPlanner.TravelPlannerApp.Service {
    internal class UserService
    {
        private readonly UserRepository _userRepository = new();

        internal UserModel AddUser(string name, string email, bool isAdmin = false)
        {
            UserModel newUser = new(null, name, email, isAdmin);

            return _userRepository.AddUser(newUser);
        }

        internal List<Model> GetUserAll()
        {
            List<Model> returnList = [.. _userRepository.GetUserAll()];

            return returnList;
        }

        internal UserModel? GetUserByEmail(string email)
        {
            return _userRepository.GetUserByEmail(email.ToLower());
        }

        internal void DeleteUser(int userId)
        {
            _userRepository.DeleteUser(userId);
        }

        internal void EditUser(int userId, string name, bool admin)
        {
            _userRepository.EditUser(userId, name, admin ? 1 : 0);
        }
    }
}