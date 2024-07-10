using Models;
using MongoDB.Driver;
using Services;

namespace UserService
{
    public class UserMongoDBService : AbstractUserService
    {
        public static MongoClient dbClient = null;

        public static UserMongoDBService instance = null;
        private UserMongoDBService()
        {
            string atlasString = "";
            dbClient = new MongoClient(atlasString);
        }


        public override User signupUser(string name, string email, string password)
        {
            throw new NotImplementedException();
        }

        public override User? findUser(string password, string email = "", string name = "")
        {
            throw new NotImplementedException();
        }

        public override bool Removeuser(User user)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateName(User user, string name)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateEmail(User user, string email)
        {
            throw new NotImplementedException();
        }

        public override bool UpdatePassword(User user, string password)
        {
            throw new NotImplementedException();
        }
    }
}
