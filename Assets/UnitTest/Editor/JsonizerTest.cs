using System;

using SharpUnit;
using System.Threading;
using Tenkafubu.Util;
using System.Collections.Generic;
namespace Tenkafubu.Json
{
	public class JsonizerTest : TestCase
	{
		
		[UnitTest]
		public void TestSerializeAnObject(){
			
			var user = new User();
			user.id = 3;
			user.name = "hifoe";
			user.age = 34;
			user.rate = 0.24f;
			user.rate2 = 0.0023;
			user.lastLogin = DateTime.Now;
			
			var jsonizer = Jsonizer.Default;
			
			var json = jsonizer.ToJson(user);
			Assert.NotNull(json);
			
			var des = jsonizer.FromJson<User>(json);
			
			AssertUser(user,des);
			
			
		}
		void AssertUser(User expect,User got){
			
			Assert.Equal(expect.id,got.id,"Wrong id");
			Assert.Equal(expect.name,got.name,"Wrong name");
			Assert.Equal(TimeUtil.ToUnixTime(expect.lastLogin),TimeUtil.ToUnixTime(got.lastLogin));
			Assert.Equal(expect.age,got.age,"Wrong age");
			Assert.Equal(expect.rate,got.rate,"Wrong rate");
			Assert.True(Math.Abs(expect.rate2 - got.rate2) < 0.01);
			
			Assert.Equal(expect.age,got.age,"Wrong age");
		}
		
		[UnitTest]
		public void TestSerializeUserArray(){
			var users = new User[3];
			users[0] = new User();
			users[1] = new User();
			users[2] = new User();
			
			var jsonizer = Jsonizer.Default;
			var json = jsonizer.ToJson(users);
			
			var des = jsonizer.FromJson<User[]>(json);
			
			Assert.Equal(users.Length,des.Length);
			for(int i = 0;i < users.Length;i++){
				AssertUser(users[i],des[i]);
			}
			
		}
		[UnitTest]
		public void TestSerializeUserList(){
			var users = new List<User>();
			users.Add(new User());
			users.Add(new User());
			users.Add(new User());
			
			var jsonizer = Jsonizer.Default;
			var json = jsonizer.ToJson(users);
			
			var des = jsonizer.FromJsonByList<User>(json);
			
			Assert.Equal(users.Count,des.Count);
			for(int i = 0;i < users.Count;i++){
				AssertUser(users[i],des[i]);
			}
		}
	}
	
	public class User{
		public long id;
		public string name;
		public int age;
		public float rate;
		public double rate2;
		public DateTime lastLogin;
		
		public override string ToString ()
		{
			return "(" + id + "," + name + "," + age + "," + rate + "," + rate2 + "," + lastLogin + ")";
		}
	}
}

