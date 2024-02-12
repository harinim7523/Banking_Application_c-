using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication
{
    
        abstract class Admin
        {
            public abstract void createAccount(List<Account> accounts);
            public abstract void deleteAccount(List<Account> accounts);

            public abstract void viewAccount(List<Account> accounts);
            public abstract void viewComplaint(List<Complaint> complaint);

            public List<string> credentials()
            {
                const string ValidAdminUsername = "harini";
                const string ValidAdminPassword = "mohanraj";
                List<string> adminuser = new List<string>();
                adminuser.Add(ValidAdminUsername);
                adminuser.Add(ValidAdminPassword);

                return adminuser;
            }
        }
    
}
