using BankingApplication;
using System;
using System.Collections;
using System.Security.Authentication;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace BankingApplication
{

    //public abstract class Admin
    //{

    //    public abstract void createAdmin(string username, string password);

    //public abstract void CreateAccount(List<Account> l);
    //public abstract void DeleteAccount(string username);
    //public abstract void ViewComplaints(List<string> complaints);
    // }  


    interface IAllMethods
    {
        void endmethod();
        bool checkCredentials();
    }

    public class InvalidAdminException : Exception
    {
        public InvalidAdminException(string errormessage) : base(errormessage)
        {
        }
    }

    public class InvalidMobileNumberException : Exception
    {
        public InvalidMobileNumberException(string errormessage) : base(errormessage)
        {
        }
    }


    class Sample : Admin, IAllMethods
    {



        static void Main(string[] args)
        {

            List<Account> l = new List<Account>();
            List<Complaint> c = new List<Complaint>();
            Sample s = new Sample();
            do
            {

                Console.WriteLine("1.Account creation" + "\n" +
                    "2.Balance Enquiry" + "\n" +
                    "3.Money Transfer" + "\n" +
                    "4.Change Mobile number" + "\n" +
                    "5.Contact Support" + "\n" +
                    "6.Delete Account" + "\n" +
                    "7.View all account" + "\n" +
                    "8.View All complaints");

                Console.Write("Enter any one option");
                int option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        s.createAccount(l);
                        break;
                    case 2:
                        checkBalance(l);
                        break;
                    case 3:
                        moneyTransfer(l);
                        break;
                    case 4:
                        updateMobile(l);
                        break;
                    case 5:
                        contactSupport(l, c);
                        break;
                    case 6:
                        s.deleteAccount(l);
                        break;
                    case 7:
                        s.viewAccount(l);
                        break;
                    case 8:
                        s.viewComplaint(c);
                        break;
                    default:
                        Console.WriteLine("Enter valid option");
                        break;
                }

                Console.WriteLine("enter 0 to exit");
                //flag = int.Parse(Console.ReadLine());
                Console.WriteLine("------------------------------------------" + "\n");

            } while (int.Parse(Console.ReadLine()) != 0);
            s.endmethod();

        }

        public override void createAccount(List<Account> l)
        {

            Console.WriteLine("Welcome to account creation Admin!\n");
            Sample s = new Sample();
            bool result = s.checkCredentials();

            try
            {

                //{
                //    if (count == 1)
                //    {
                //        Console.WriteLine("Admin Credentials Needed!\n");
                //    }
                //    if (count == 2)
                //    {
                //        Console.WriteLine("Username or Password is wrong. Try Again!");
                //        Console.WriteLine("This is warning 1\n");
                //    }
                //    if (count == 3)
                //    {
                //        Console.WriteLine("Username or Password is wrong. Try Again!");
                //        Console.WriteLine("This is warning 2\n");
                //    }

                //    Console.WriteLine("Enter Username:");
                //    string username = Console.ReadLine();

                //    Console.WriteLine("Enter Password:");
                //    string password = Console.ReadLine();

                //    if (username.Equals(correct[0]) && password.Equals(correct[1]))
                //    {
                //        flag = true;
                //        break;
                //    }
                //    count++;

                //}
                //if (count > 3)
                //{
                //    throw new InvalidAdminException("Invalid username or password.");
                //}
                int count = 1;


                if (result)
                {
                    Account a = new Account();

                    Console.Write("Enter account no");
                    a.acc_no = long.Parse(Console.ReadLine());

                    Console.Write("Enter Account Holder name");
                    a.name = Console.ReadLine();

                    Console.Write("Enter mobile number");
                    long num = long.Parse(Console.ReadLine());
                    int len = num.ToString().Length;
                    if (len == 10)
                    {
                        a.mobno = num;

                        Console.Write("Enter amount to be deposited");
                        a.balance = long.Parse(Console.ReadLine());

                        l.Add(a);

                    }
                    else
                    {
                        throw new InvalidMobileNumberException("Invalid mobile number.\nMobile number length should be equal to 10");
                    }
                }

            }
            catch (InvalidMobileNumberException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("Please enter a long value");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }

        }

        static void checkBalance(List<Account> l)
        {
            Console.WriteLine("BALANCE CHECKING SECTION!\n");
            int count = 1;
            try
            {

                Console.WriteLine("Enter mobile number to check balance");
                long number = long.Parse(Console.ReadLine());
                int len = number.ToString().Length;
                if (len == 10)
                {
                    foreach (var i in l)
                    {
                        if (i.mobno == number)
                        {
                            Console.WriteLine("Your balance is " + i.balance);
                        }
                    }
                }
                else
                {
                    throw new InvalidMobileNumberException("Invalid mobile number.\nMobile number length should be equal to 10");
                }

            }
            catch (InvalidMobileNumberException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("Please enter a long value");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }

        }

        static void moneyTransfer(List<Account> l)
        {
            Console.WriteLine("MONEY TRANSACTION SECTION !\n");
            try
            {
                Console.WriteLine("Enter the sender account mobile number");
                long sender = long.Parse(Console.ReadLine());

                if (sender.ToString().Length != 10)
                {
                    throw new InvalidMobileNumberException("Invalid sender mobile number.\nMobile number length should be equal to 10");
                }

                Console.WriteLine("Enter the receiver account mobile number");
                long receiver = long.Parse(Console.ReadLine());

                if (receiver.ToString().Length != 10)
                {
                    throw new InvalidMobileNumberException("Invalid receiver mobile number.\nMobile number length should be equal to 10");
                }

                Console.WriteLine("Enter the amount to be send");
                long amount = long.Parse(Console.ReadLine());


                Account from = GetAccount(ref sender, l);
                Account to = GetAccount(ref receiver, l);

                if (from != null && to != null)
                {
                    if (from.balance < amount)
                    {
                        Console.WriteLine("Not sufficient balance in the Sender account");
                    }
                    else
                    {
                        from.balance -= amount;
                        to.balance += amount;
                        Console.WriteLine("The balance of account number(sender) " + from.acc_no + " is: " + from.balance);
                        Console.WriteLine("The balance of account number(receiver) " + to.acc_no + " is: " + to.balance);
                    }
                }
                else if (from != null && to == null)
                {
                    Console.WriteLine("No such sender account exists");
                }
                else
                {
                    Console.WriteLine("No such receiver account exists");
                }
            }
            catch (InvalidMobileNumberException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("Please enter a long value");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }



        }

        static void updateMobile(List<Account> l)
        {
            Console.WriteLine("MOBILE NUMBER UPDATION!\n");
            try
            {
                Console.WriteLine("Enter the mobile number");
                long mobile_number = long.Parse(Console.ReadLine());
                if (mobile_number.ToString().Length != 10)
                {
                    throw new InvalidMobileNumberException("Invalid mobile number.\nMobile number length should be equal to 10");
                }

                Account a = GetAccount(ref mobile_number, l);

                if (a != null)
                {

                    Console.WriteLine("Enter the new mobile number");
                    long new_number = long.Parse(Console.ReadLine());
                    if (new_number.ToString().Length != 10)
                    {
                        throw new InvalidMobileNumberException("Invalid mobile number.\nMobile number length should be equal to 10");
                    }
                    a.mobno = new_number;
                    Console.WriteLine("Mobile number changed successfully");

                }
                else
                {
                    Console.WriteLine("No such account exists");
                }
            }
            catch (InvalidMobileNumberException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("Please enter a long value");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }

        static void contactSupport(List<Account> l, List<Complaint> c)
        {
            Console.WriteLine("CONTACT SUPPORT SECTION!\n");
            try
            {
                Console.WriteLine("Enter the mobile number");
                long mobile_number = long.Parse(Console.ReadLine());
                if (mobile_number.ToString().Length != 10)
                {
                    throw new InvalidMobileNumberException("Invalid mobile number.\nMobile number length should be equal to 10");
                }

                Account a = GetAccount(ref mobile_number, l);

                if (a != null)
                {

                    Random random = new Random();
                    //long complaintNumber = (long)(random.Next(1000000000, 999999999) * 1000000000L + random.Next(1000000000));
                    //long complaintNumber = (long)(random.Next(10000, 100000));
                    int firstPart = random.Next(100_000, 999_999);
                    int secondPart = random.Next(100_000, 999_999);

                    long complaintNumber = (long)firstPart * 1_000_000 + secondPart;
                    Complaint cobj = new Complaint();

                    cobj.mobno = a.mobno;
                    cobj.acc_no = a.acc_no;
                    cobj.complaint_number = complaintNumber;
                    c.Add(cobj);

                    Console.WriteLine($"We have registered your complaint. Your complaint number is: {complaintNumber}");
                    Console.WriteLine("Our customer executive will contact you in 24 hours.");

                }
                else
                {
                    Console.WriteLine("No such account exists");
                }
            }
            catch (InvalidMobileNumberException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("Please enter a long value");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }

        }

        public override void deleteAccount(List<Account> l)
        {
            Console.WriteLine("ACCOUNT DELETION SECTION!\n");
            try
            {
                Sample s = new Sample();
                bool result = s.checkCredentials();
                if (result)
                {
                    Console.WriteLine("Enter the mobile number");
                    long mobile_number = long.Parse(Console.ReadLine());
                    if (mobile_number.ToString().Length != 10)
                    {
                        throw new InvalidMobileNumberException("Invalid mobile number.\nMobile number length should be equal to 10");
                    }

                    Account a = GetAccount(ref mobile_number, l);

                    if (a != null)
                    {

                        l.Remove(a);
                        Console.WriteLine("The account " + a.acc_no + " with mobile number " + a.mobno + " is deleted successfully");

                    }
                    else
                    {
                        Console.WriteLine("No such account exists");
                    }
                }
            }
            catch (InvalidMobileNumberException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("Please enter a long value");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }



        }
        public override void viewAccount(List<Account> l)
        {

            Console.WriteLine("ALL ACCOUNTS!\n");
            try
            {

                Sample s = new Sample();
                bool result = s.checkCredentials();
                if (l.Count > 0)
                {
                    if (result)
                    {

                        int index = 1;
                        foreach (var i in l)
                        {
                            Console.WriteLine(index + ". Name: " + i.name + "\n" + " Account Number: " + i.acc_no + "\n" + " Mobile Number: " + i.mobno + "\n");
                            index++;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("There is no complaint");
                }

            }
            catch (InvalidMobileNumberException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("Please enter a long value");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }


        }

        public override void viewComplaint(List<Complaint> complaint)
        {
            Console.WriteLine("ALL COMPLAINTS!\n");

            try
            {
                Sample s = new Sample();
                bool result = s.checkCredentials();
                if (complaint.Count > 0)
                {
                    if (result)
                    {
                        Console.WriteLine("The complaints received are:\n");
                        int index = 1;
                        foreach (var i in complaint)
                        {
                            Console.WriteLine(index + ". Complaint Number: " + i.complaint_number + "\n" + "Account Number: " + i.acc_no + "\n" + "Mobile Number: " + i.mobno + "\n");
                            index++;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("There is no complaint");
                }

            }
            catch (InvalidMobileNumberException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("Please enter a long value");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }

        }

        static Account GetAccount(ref long mobileNumber, List<Account> l)
        {

            foreach (var a in l)
            {
                if (a.mobno == mobileNumber)
                {
                    return a;
                }
            }
            return null;
        }

        public bool checkCredentials()
        {

            bool flag = false;
            int count = 1;
            List<string> correct = new List<string>();
            correct = credentials();

            try
            {
                while (count <= 3)
                {
                    if (count == 1)
                    {
                        Console.WriteLine("Admin Credentials Needed!\n");
                    }
                    if (count == 2)
                    {
                        Console.WriteLine("Username or Password is wrong. Try Again!");
                        Console.WriteLine("This is warning 1\n");
                    }
                    if (count == 3)
                    {
                        Console.WriteLine("Username or Password is wrong. Try Again!");
                        Console.WriteLine("This is warning 2\n");
                    }

                    Console.Write("Enter Username:");
                    string username = Console.ReadLine();

                    Console.Write("Enter Password:");
                    string password = Console.ReadLine();

                    if (username.Equals(correct[0]) && password.Equals(correct[1]))
                    {
                        flag = true;
                        break;
                    }
                    count++;

                }
                if (count > 3)
                {
                    throw new InvalidAdminException("Invalid username or password.");
                }

            }
            catch (InvalidAdminException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }

            return flag;
        }

        public void endmethod()
        {
            Console.WriteLine("Thank You!");
        }



    }
}








