using System;
using System.Collections.Generic;
using System.Text;

namespace OnMenu.Models.Login
{
    public class Token
    {
        public int Id;
        public string AccessToken;
        public string ErrorDescription;
        public DateTime Expiration; 

        public Token(){}
    }
}
