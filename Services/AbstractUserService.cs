using Models;

namespace Services
{
    public abstract class AbstractUserService
    {
        private static AbstractUserService instance;
        public static AbstractUserService GetService()
        {
            return instance;
        }

        public abstract User signupUser(string name, string email, string password);

        public abstract User? findUser(string password, string email="", string name="");

        public abstract bool Removeuser(User user);

        public abstract bool UpdateName(User user, string name);

        public abstract bool UpdateEmail(User user, string email);

        public abstract bool UpdatePassword(User user, string password);
        
    }
}
