using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices.AccountManagement;
using Enterprise;

namespace TMN
{
	public static class AccountHandler
	{
		public static Account GetAccount(string username)
		{
			Account account = null;

			PrincipalContext context = new PrincipalContext(ContextType.Machine, Environment.MachineName);

			UserPrincipal user = new UserPrincipal(context);
			user.Name = "*";

			PrincipalSearcher ps = new PrincipalSearcher();
			ps.QueryFilter = user;

			PrincipalSearchResult<Principal> result = ps.FindAll();

			foreach (Principal p in result)
			{
				using (UserPrincipal up = (UserPrincipal)p)
				{
					if (up.Name == username)
						account = new Account(up.Name, "", up.UserCannotChangePassword, up.PasswordNeverExpires);
					else
						account = null;
				}
			}

			return account;
		}

		public static AccountCollection GetAllAccount()
		{
			AccountCollection list = new AccountCollection();

			PrincipalContext context = new PrincipalContext(ContextType.Machine, Environment.MachineName);

			UserPrincipal user = new UserPrincipal(context);
			user.Name = "*";

			PrincipalSearcher ps = new PrincipalSearcher();
			ps.QueryFilter = user;

			PrincipalSearchResult<Principal> result = ps.FindAll();

			foreach (Principal p in result)
			{
				using (UserPrincipal up = (UserPrincipal)p)
				{
					Account account = new Account(up.Name, "", up.UserCannotChangePassword, up.PasswordNeverExpires);
					list.Add(account);
				}
			}

			return list;
		}

		public static void  CreateAccount(string username, string password)
		{
			try
			{
				PrincipalContext context = new PrincipalContext(ContextType.Machine);

				UserPrincipal user = new UserPrincipal(context);
				user.SetPassword(password);
				user.Name = username;
				user.PasswordNeverExpires = true;
				user.UserCannotChangePassword = false;
				user.Save();
				

				GroupPrincipal remoteGroup = GroupPrincipal.FindByIdentity(context, "Remote Desktop Users");
				remoteGroup.Members.Add(user);
				remoteGroup.Save();

				GroupPrincipal adminGroup = GroupPrincipal.FindByIdentity(context, "Administrators");
				adminGroup.Members.Add(user);
				adminGroup.Save();

				GroupPrincipal usersGroup = GroupPrincipal.FindByIdentity(context, "Users");
				usersGroup.Members.Add(user);
				usersGroup.Save();

			}
			catch (Exception ex)
			{
				Logger.Write(ex);
			}

		}

		public static void SetPassword(string username, string newpassword)
		{
			PrincipalContext context = new PrincipalContext(ContextType.Machine, Environment.MachineName);

			UserPrincipal user = new UserPrincipal(context);
			user.Name = "*";

			PrincipalSearcher ps = new PrincipalSearcher();
			ps.QueryFilter = user;

			PrincipalSearchResult<Principal> result = ps.FindAll();

			foreach (Principal p in result)
			{
				using (UserPrincipal up = (UserPrincipal)p)
				{
					if (up.Name == username)
					{
						up.SetPassword(newpassword);
						up.Save();
					}
				}
			}
		}
	}
}
