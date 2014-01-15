// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using System;


using SharpUnit;
using System.Threading;
using Tenkafubu.Util;
using System.Collections.Generic;
using Tenkafubu.Json.Template;

namespace Tenkafubu.Json
{
	public class PolyMorphJsonizeTest : TestCase
	{
		public PolyMorphJsonizeTest ()
		{
		}

		[UnitTest]
		public void TestPolyMorph(){
			var jsonizer = Jsonizer.Default;
			jsonizer.RegisterTemplate(new CommandMorph(jsonizer.Repo));

			var command1 = jsonizer.ToJson(new Command1());
			Console.Write(command1);
			var des = jsonizer.FromJson<BaseCommand>(command1);

			Assert.True(des is Command1);

		}

	}


	public interface BaseCommand{}

	public class Command1 : BaseCommand{

		public string message;
		public Command1(){}
		public Command1(string m) {
			this.message = m;
		}
	}

	public class Command2 : BaseCommand{

		public int age;
		public Command2(){}
		public Command2(int age){
			this.age = age;
		}
	}

	public class CommandMorph : TypeHintClassTemplate{


		public CommandMorph(TemplateRepository repo) : base(repo){
		}


		public override string TypeToHint (Type t)
		{
			if(t == typeof(Command1)) return "1";
			else if(t == typeof(Command2)) return "2";
			else return "unknown";
		}

		public override Type HintToType (string hint)
		{
			if(hint == "1") return typeof(Command1);
			else return typeof(Command2);
		}

		public override Type[] Classes {
			get {
				return new Type[]{typeof(BaseCommand), typeof(Command1),typeof(Command2)};
			}
		}
		public override string HintField {
			get {
				return "commandType";
			}
		}
	}


}

