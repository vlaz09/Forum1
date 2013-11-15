using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniForum
{
    public class Users
    {
        public int ID { get; private set; }
        public string Login { get; private set; }
        public string Pass { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public DateTime DataRegistr { get; private set; }
        public string Status { get; private set; }
        public string Role { get; private set; }
        public bool ConfirmRegistration { get; private set; }

        public Users(int ID,string Login, string Pass, string Name, string Email,string Status, string Role, bool ConfirmRegistration)
        {
            this.ID = ID;
            this.Login = Login;
            this.Pass = Pass;
            this.Name = Name;
            this.Email = Email;
            this.Status = Status;
            this.Role = Role;
            this.ConfirmRegistration = ConfirmRegistration;
        }

        public Users(int ID, string Login, string Name, string Status, string Role, bool ConfirmRegistration)
        {
            this.ID = ID;
            this.Login = Login;
            this.Name = Name;
            this.Status = Status;
            this.Role = Role;
            this.ConfirmRegistration = ConfirmRegistration;
        }
        public Users()
        {
        }
    }

}