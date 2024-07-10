using Models;

namespace Services
{
    public class UserService
    {
        private static UserService instance;

        private UserService()
        {
            
        }

        public static UserService Instance { get
            {
                if(instance == null)
                {
                    instance = new UserService();
                }
                return instance;
            } 
        }

        public void SignupUser(string name,string email, string password)
        {

        }

        /// <summary>
        /// Login can be done with both email and name.
        /// Only on of them is necessary and email is the default value.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public User Login(string password,string name = null,string email = null)
        {
            User result = new User();

            return result;
        }

        /// <summary>
        /// Deletes a User form the database.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool RemoveUser(User user)
        {

            return true;
        }

        public bool UpdateUserName(User target, string name)
        {
            return true;
        }

        public bool UpdateUserDescription(User target, string name)
        {
            return true;
        }

        public bool UpdateUserPassword(User target, string name)
        {
            return true;
        }


    }
}
