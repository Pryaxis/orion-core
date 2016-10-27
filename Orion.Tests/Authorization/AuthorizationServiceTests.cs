using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Orion.Authorization;

namespace Orion.Tests.Authorization
{
	[TestFixture]
	public class AuthorizationServiceTests
	{
		[TestCase("TestAccount")]
		[TestCase("198213032057")]
		[TestCase("something with spaces")]
		[TestCase("Ԙԛӭӱӱ")]
		[TestCase("Ԙԛӭӱ\\\\...ӱ")]
		public void UserAccount_AddAccount(string accountName)
		{
			using (Orion orion = new Orion())
			{
				IUserAccountService userAccountService = orion.GetService<PlainTextAccountService>();
				userAccountService.AddUser(accountName);

				IUserAccount account = userAccountService.GetUser(accountName);
				Assert.IsNotNull(account);
				Assert.AreEqual(account.AccountName, accountName);

				userAccountService.DeleteUser(account);
			}
		}

		[TestCase("")]
		[TestCase(null)]
		public void UserAccount_AddAccountShouldThrowArgumentNullException(string accountName)
		{
			using (Orion orion = new Orion())
			{
				IUserAccountService userAccountService = orion.GetService<PlainTextAccountService>();
				Assert.That(() => userAccountService.AddUser(accountName), Throws.TypeOf<ArgumentNullException>());
			}
		}

		[Test]
		public void UserAccount_AddAccountShouldThrowInvalidOperationException()
		{
			using (Orion orion = new Orion())
			{
				IUserAccountService userAccountService = orion.GetService<PlainTextAccountService>();
				IUserAccount account = userAccountService.AddUser("duplicateAccount");
				Assert.That(() =>
				{
					userAccountService.AddUser("duplicateAccount");
				}, Throws.TypeOf<InvalidOperationException>());

				userAccountService.DeleteUser(account);
			}
		}

		[TestCase("")]
		[TestCase(null)]
		public void UserAccount_SetPasswordWithNullValueShouldThrowArgumentNullException(string password)
		{
			using (Orion orion = new Orion())
			{
				IUserAccountService userAccountService = orion.GetService<PlainTextAccountService>();

				IUserAccount userAccount = userAccountService.GetUser("nullPasswordTest") ??
										   userAccountService.AddUser("nullPasswordTest");

				Assert.That(() =>
				{
					userAccount.SetPassword(password);
				}, Throws.TypeOf<ArgumentNullException>());

				userAccountService.DeleteUser(userAccount);
			}
		}

		[TestCase("SamplePassword")]
		[TestCase("Test1!")]
		public void UserAccount_SetPasswordShouldSucceed(string password)
		{
			using (Orion orion = new Orion())
			{
				IUserAccountService userAccountService = orion.GetService<PlainTextAccountService>();

				IUserAccount userAccount = userAccountService.GetUser("SetPasswordShouldSucceedTest") ??
										   userAccountService.AddUser("SetPasswordShouldSucceedTest");

				userAccount.SetPassword(password);

				Assert.IsTrue(userAccount.Authenticate(password));

				userAccountService.DeleteUser(userAccount);
			}
		}

		[TestCase("test", "test", ExpectedResult = true)]
		[TestCase("test", "test1", ExpectedResult = false)]
		public bool UserAccount_AuthenticateValues(string passwordToSet, string passwordToCompare)
		{
			bool val = false;

			using (Orion orion = new Orion())
			{
				IUserAccountService userAccountService = orion.GetService<PlainTextAccountService>();

				IUserAccount userAccount = userAccountService.GetUser("Authenticate") ??
										   userAccountService.AddUser("Authenticate");

				userAccount.SetPassword(passwordToSet);

				val = userAccount.Authenticate(passwordToCompare);

				userAccountService.DeleteUser(userAccount);
			}

			return val;
		}
	}
}
