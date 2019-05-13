using System;
using System.Collections.Generic;
using System.Text;

namespace OnMenu.Models.Login
{
    /// <summary>
    /// Token for a user (to be implemented on the future)
    /// </summary>
    public class Token
    {
        public int Id;
        public string AccessToken;
        public string ErrorDescription;
        public DateTime Expiration; 

        public Token(){}
    }
}
