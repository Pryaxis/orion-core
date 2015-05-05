using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orion.Utilities;
using Orion.Extensions;
using Orion.Users;

namespace UnitTests
{
	[TestClass]
	public class Class
	{
		public string name;

		public override string ToString()
		{
			return name;
		}
	}

	[TestClass]
	public class CacheTests
	{
		public OrderedCache<User> classCache;
		public Random r = new Random();
		public Stopwatch total = new Stopwatch();

		[TestMethod]
		public void TestMethod1()
		{
			total.Start();
			for (var j = 0; j < 10; j++)
			{
				classCache = new OrderedCache<User>(100000)
				{
					FlushInterval = 10000,
					ClearCount = 1
				};
				classCache.FlushEvent += FlushCallback;

				List<User> classes = new List<User>(100000);
				for (int i = 0; i < 100000; i++)
				{
					User u = new User
					{
						Name = r.NextString(10),
						Group = r.NextString(5),
						ID = i
					};
					classes.Add(u);
				}

				classCache.PushMany(classes);
				classCache.Pop();
				classCache.Push(new User {Name = "name", Group = "group", ID = 100000});
				var obj2 = classCache[0];
				classCache.Push(new User {Name = "name2", Group = "group2", ID = 100001});
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
			classCache = new OrderedCache<User>(10)
			{
				FlushInterval = 5000,
				ClearCount = 1
			};
			classCache.FlushEvent += FlushCallback;

			List<User> classes = new List<User>(10);

			for (int i = 0; i < 10; i++)
			{
				User u = new User
				{
					Name = r.NextString(10),
					Group = r.NextString(7),
					ID = i
				};
				classes.Add(u);
			}

			classCache.PushMany(classes);

			classCache.Sort();
			classCache.Push(new User { Name = "name" });
			var user = classCache[0];
			classCache.Push(new User { Name = "name2" });
			user = classCache[0];
			classCache.Push(new User { Name = "name3" });
			user = classCache[0];
			var newuser = classCache.FirstOrDefault(u => u == user);
			var user2 = classCache.Pop();
			var users = classCache.Where(u => u.Name.Length > 1).Select(u => u.Name);
			Console.WriteLine(string.Join(", ", users));

			Thread.Sleep(7000);
		}

		private bool FlushCallback(IEnumerable<User> enumerable)
		{
			return true;
		}
	}
}