using System;
using Tenkafubu.Reflection;
using System.Collections.Generic;

namespace Tenkafubu.Json.Template
{
	public abstract class TypeHintClassTemplate : WithTargetClass
	{
		public abstract Type[] Classes{get;}
		public abstract string TypeToHint(Type t);
		public abstract Type HintToType(string hint);
		public abstract string HintField{ get;}

		protected Dictionary<Type,JsonizeTemplate> templates;

		public TypeHintClassTemplate(TemplateRepository repo){

			templates = new Dictionary<Type, JsonizeTemplate>();
			var gen = new ReflectionTemplateGenerator();
			gen.SetRepository(repo);
			foreach(var t in Classes){
				templates[t] = gen.Generate(t);
			}

		}


		public object ToJsonValue (object obj)
		{
			var t = obj.GetType();
			var hint = TypeToHint(t);
			var template = templates[t];

			var o = template.ToJsonValue(obj) as Dictionary<string,object>;
			o[HintField] = hint;
			return o;
		}

		public object FromJsonValue (Type t, object v)
		{
			var jsonObj = (Dictionary<string,object>)v;

			if(jsonObj.ContainsKey(HintField)){
				var trueType = HintToType(jsonObj[HintField].ToString());
				var template = templates[trueType];
				var o = template.FromJsonValue(trueType,v);
				return o;
			}else{
				throw new Exception("Can't find hint field:" + HintField);
			}


		}
		
	}
}

