using System;
using System.Collections.Generic;
using System.Text;

namespace OnMenu.Models
{
    class User
    {
        public int Id;
        public string Name;
        public string Password;

        public User() { }
        public User(string name, string password)
        {
            Name = name;
            Password = password;
        }
    } 
}
