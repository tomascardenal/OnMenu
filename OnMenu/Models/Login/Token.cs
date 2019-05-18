using System;

namespace OnMenu.Models.Login
{
    /// <summary>
    /// Token for a user (to be implemented on the future)
    /// </summary>
    public class Token
    {
        /// <summary>
        /// The token's id on the DB
        /// </summary>
        public int Id;
        /// <summary>
        /// The access token string
        /// </summary>
        public string AccessToken;
        /// <summary>
        /// Description of an error, if ocurred
        /// </summary>
        public string ErrorDescription;
        /// <summary>
        /// Expiration date of the token
        /// </summary>
        public DateTime Expiration;

        /// <summary>
        /// Default constructor
        /// </summary>
        public Token() { }
    }
}
