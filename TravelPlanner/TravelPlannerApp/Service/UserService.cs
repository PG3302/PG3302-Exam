using TravelDatabase.Models;
using TravelDatabase.Repositories;

namespace TravelPlanner.TravelPlannerApp.Service {
	public class UserService
    {
        private readonly UserRepository _userRepository = new();

        public UserModel AddUser(string name, string email, bool isAdmin = false)
        {
            UserModel newUser = new(null, name, email, isAdmin);

            return _userRepository.AddUser(newUser);
        }

        public List<Model> GetUserAll()
        {
            List<Model> returnList = [.. _userRepository.GetUserAll()];

            return returnList;
        }

        public UserModel? GetUserByEmail(string email)
        {
            return _userRepository.GetUserByEmail(email.ToLower());
        }
    }
}