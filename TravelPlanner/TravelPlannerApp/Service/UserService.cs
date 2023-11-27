using TravelDatabase.Models;
using TravelDatabase.Repositories;

namespace TravelPlanner.TravelPlannerApp.Service {
	public class UserService
    {
        private readonly UserRepository _userRepository = new();

        public UserModel AddUser(int id, string name, string email, bool isAdmin = false)
        {
            UserModel newUser = new(id, name, email, isAdmin);

            return _userRepository.AddUser(newUser);
        }

        public List<UserModel> GetUserAll()
        {
            return _userRepository.GetUserAll();
        }

        public UserModel? GetUserByEmail(string email)
        {
            return _userRepository.GetUserByEmail(email);
        }
    }
}