using System;
using Tenkafubu.Reflection;
using System.Collections.Generic;

namespace Tenkafubu.Json.Template
{
	public class ReflectionTemplate : JsonizeTemplate
	{
		ClassDesc desc;
		TemplateRepository repo;
		public ReflectionTemplate (ClassDesc desc,TemplateRepository repo)
		{
			this.desc = desc;
			this.repo = repo;
		}
		
		public object ToJsonValue (object obj)
		{
			if(obj == null)return JNothing.Nothing;
			
			var jsonObj = new Dictionary<string,object>();
			foreach(var f in desc.jsonFields){
				var v = f.Get(obj);
				
				var t = repo.GetTemplate(f.FieldType);
				var jValue = t.ToJsonValue(v);
				if(jValue != JNothing.Nothing){
					jsonObj[f.nameForJson] = jValue;
				}
			}
			return jsonObj;
			
		}

		public object FromJsonValue (Type t, object v)
		{
			var jsonObj = (Dictionary<string,object>)v;
			var instance = ReflectionSupport.CreateNewInstance(t);
			foreach(var f in desc.jsonFields){
				if(!f.ignoreInJson && jsonObj.ContainsKey(f.nameForJson)){
					var template = repo.GetTemplate(f.FieldType);
					var fv = template.FromJsonValue(f.FieldType,jsonObj[f.nameForJson]);
					if(fv != null){
						f.Set(instance,fv);
					}
				}	
			}
			return instance;
			
		}
		
	}
}

