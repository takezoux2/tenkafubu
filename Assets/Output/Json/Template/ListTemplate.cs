using System;
using System.Collections;
using System.Collections.Generic;
using Tenkafubu.Reflection;

namespace Tenkafubu.Json.Template
{
	public class ListTemplate : DynamicJsonizeTemplate
	{
		
		TemplateRepository repository;
		
		public bool IsTarget (Type t)
		{
			return typeof(IList).IsAssignableFrom(t);
		}

		public void SetReopsitory (TemplateRepository repository)
		{
			this.repository = repository;
		}

		public object ToJsonValue (object obj)
		{
			IList list = (IList)obj;
			var jValues = new ArrayList(list.Count);
			foreach(var o in list){
				var template = repository.GetTemplate(o.GetType());
				var jValue = template.ToJsonValue(o);
				if(jValue != JNothing.Nothing){
					jValues.Add(jValue);
				}
			}
			return jValues;
		}

		public object FromJsonValue (Type t, object v)
		{
			var generics = t.GetGenericArguments();
			if(generics.Length == 0){
				throw new Exception("Can't detect object type from non-generic list");
			}
			var elementType = generics[0];
			var template = repository.GetTemplate(elementType);
			if(v is IList){
				var list = (IList)v;
				var objects = (IList)ReflectionSupport.CreateNewInstance(t);
				foreach(var o in list){
					var des = template.FromJsonValue(elementType,o);
					objects.Add(des);
				}
				return objects;
			}else{
				var des = template.FromJsonValue(elementType,v);
					
				var objects = (IList)ReflectionSupport.CreateNewInstance(t);
				objects.Add(des);
				return objects;
			}
		}
		
	}
}

