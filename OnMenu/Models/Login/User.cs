namespace OnMenu.Models.Login
{
    /// <summary>
    /// User register (to be implemented on the future)
    /// </summary>
    class User
    {
        /// <summary>
        /// User id
        /// </summary>
        public int Id;
        /// <summary>
        /// User name
        /// </summary>
        public string Name;
        /// <summary>
        /// User password
        /// </summary>
        public string Password;

        /// <summary>
        /// Empty constructor
        /// </summary>
        public User() { }

        /// <summary>
        /// Starts a new user with the given name and password
        /// </summary>
        /// <param name="name">the name</param>
        /// <param name="password">the password</param>
        public User(int id, string name, string password)
        {
            Id = id;
            Name = name;
            Password = password;
        }
    }
}
