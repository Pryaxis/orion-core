using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orion.Extensions;
using Orion.Grouping;
using Orion.UserAccounts;
using Orion.Utilities;

namespace Unit_Tests
{
	[TestClass]
	public class CacheTests
	{
		public OrderedCache<UserAccount> classCache;
		public Random r = new Random();
		public Stopwatch total = new Stopwatch();

		[TestMethod]
		public void TestMethod1()
		{
			total.Start();
			for (var j = 0; j < 10; j++)
			{
				classCache = new OrderedCache<UserAccount>(100000)
				{
					FlushInterval = 10000,
					ClearCount = 1
				};
				classCache.FlushEvent += FlushCallback;

				List<UserAccount> classes = new List<UserAccount>(100000);
				for (int i = 0; i < 100000; i++)
				{
                    UserAccount u = new UserAccount
                    {
						Name = r.NextString(10),
						Group = new Group(r.NextString(5)),
						ID = i
					};
					classes.Add(u);
				}

				classCache.PushMany(classes);
				classCache.Pop();
				classCache.Push(new UserAccount { Name = "name", Group = new Group("group"), ID = 100000});
				var obj2 = classCache[0];
				classCache.Push(new UserAccount { Name = "name2", Group = new Group("group2"), ID = 100001});
				var obj3 = classCache[0];
				var users = classCache.Where(u => u.Name.Length > 1).Select(u => u.Name);
				classCache.Pop();
			}
			total.Stop();
			Console.WriteLine("Total elasped (s): {0}", total.ElapsedMilliseconds/1000);
			Console.WriteLine("Average (ms): {0}", total.ElapsedMilliseconds / 10);
		}

		[TestMethod]
		public void TestMethod2()
		{
			classCache = new OrderedCache<UserAccount>(10)
			{
				FlushInterval = 5000,
				ClearCount = 1
			};
			classCache.FlushEvent += FlushCallback;

			List<UserAccount> classes = new List<UserAccount>(10);

			for (int i = 0; i < 10; i++)
			{
                UserAccount u = new UserAccount
                {
					Name = r.NextString(10),
					Group = new Group(r.NextString(7)),
					ID = i
				};
				classes.Add(u);
			}

			classCache.PushMany(classes);

			classCache.Sort();
			classCache.Push(new UserAccount { Name = "name" });
			var user = classCache[0];
			classCache.Push(new UserAccount { Name = "name2" });
			user = classCache[0];
			classCache.Push(new UserAccount { Name = "name3" });
			user = classCache[0];
			var newuser = classCache.FirstOrDefault(u => u == user);
			var user2 = classCache.Pop();
			var users = classCache.Where(u => u.Name.Length > 1).Select(u => u.Name);
			Console.WriteLine(string.Join(", ", users));

			Thread.Sleep(7000);
		}

		private bool FlushCallback(IEnumerable<UserAccount> enumerable)
		{
			return true;
		}
	}
}