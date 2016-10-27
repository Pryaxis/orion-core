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
	public class AccountServiceTests
	{
		[TestCase("TestAccount")]
		[TestCase("198213032057")]
		[TestCase("something with spaces")]
		[TestCase("Ԙԛӭӱӱ")]
		[TestCase("Ԙԛӭӱ\\\\...ӱ")]
		public void AccountService_AddAccount(string accountName)
		{
			using (Orion orion = new Orion())
			{
				IAccountService accountService = orion.GetService<PlainTextAccountService>();
				accountService.AddAccount(accountName);

				IAccount account = accountService.GetAccount(accountName);
				Assert.IsNotNull(account);
				Assert.AreEqual(account.AccountName, accountName);

				accountService.DeleteAccount(account);
			}
		}

		[TestCase("")]
		[TestCase(null)]
		public void AccountService_AddAccountShouldThrowArgumentNullException(string accountName)
		{
			using (Orion orion = new Orion())
			{
				IAccountService accountService = orion.GetService<PlainTextAccountService>();
				Assert.That(() => accountService.AddAccount(accountName), Throws.TypeOf<ArgumentNullException>());
			}
		}

		[TestCase("")]
		[TestCase(null)]
		public void AccountService_SetPasswordWithNullValueShouldThrowArgumentNullException(string password)
		{
			using (Orion orion = new Orion())
			{
				IAccountService accountService = orion.GetService<PlainTextAccountService>();

				IAccount account = accountService.GetAccount("nullPasswordTest")
					?? accountService.AddAccount("nullPasswordTest");

				Assert.That(() =>
				{
					account.SetPassword(password);
				}, Throws.TypeOf<ArgumentNullException>());

				accountService.DeleteAccount(account);
			}
		}

		[TestCase("SamplePassword")]
		[TestCase("Test1!")]
		public void AccountService_SetPasswordShouldSucceed(string password)
		{
			using (Orion orion = new Orion())
			{
				IAccountService accountService = orion.GetService<PlainTextAccountService>();

				IAccount account = accountService.GetAccount("SetPasswordShouldSucceedTest")
					?? accountService.AddAccount("SetPasswordShouldSucceedTest");

				account.SetPassword(password);

				Assert.IsTrue(account.Authenticate(password));

				accountService.DeleteAccount(account);
			}
		}

		[TestCase("test", "test", ExpectedResult = true)]
		[TestCase("test", "test1", ExpectedResult = false)]
		public bool AccountService_AuthenticateValues(string passwordToSet, string passwordToCompare)
		{
			bool val = false;

			using (Orion orion = new Orion())
			{
				IAccountService accountService = orion.GetService<PlainTextAccountService>();

				IAccount account = accountService.GetAccount("Authenticate")
					?? accountService.AddAccount("Authenticate");

				account.SetPassword(passwordToSet);

				val = account.Authenticate(passwordToCompare);

				accountService.DeleteAccount(account);
			}

			return val;
		}
	}
}
