using System.Linq;


namespace TMN
{
    class Program
    {
        static void Main(string[] args)
        {
			Account account = AccountHandler.GetAccount(args[0]);
			if (account == null)
			{
				if (args.Length >= 2)
					AccountHandler.CreateAccount(args[0], args[1]);
				else
					AccountHandler.CreateAccount(args[0], "123");
			}
			else
			{
				AccountHandler.SetPassword(args[0], args[1]);
			}
        }
    }
}
