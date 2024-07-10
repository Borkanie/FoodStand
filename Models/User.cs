namespace Models
{
    public class User
    {
        public User()
        {
            
        }

        /// <summary>
        /// Unique identifier.
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Email related to the account.
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// The username of the the account.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// The password of the user.
        /// </summary>
        public string Password { get; set; }
    }
}
