using System;
using System.Collections;

namespace Tenkafubu.Json.Template
{
	public class ArrayTemplate : DynamicJsonizeTemplate
	{
		
		TemplateRepository repository;
		
		public bool IsTarget (Type t)
		{
			return t.IsArray;
		}

		public void SetReopsitory (TemplateRepository repository)
		{
			this.repository = repository;
		}

		public object ToJsonValue (object obj)
		{
			
			Array array = (Array)obj;
			var jValues = new ArrayList(array.Length);
			foreach(var o in array){
				var template = repository.GetTemplate(o.GetType());
				var v = template.ToJsonValue(o);
				if(v != JNothing.Nothing){
					jValues.Add(v);
				}
			}
			return jValues;
		}

		public object FromJsonValue (Type t, object v)
		{
			var elementType = t.GetElementType();
			var template = repository.GetTemplate(elementType);
			if(v is IList){
				var list = (IList)v;
				var objects = Array.CreateInstance(elementType,list.Count);
				var i = 0;
				foreach(var o in list){
					var des = template.FromJsonValue(elementType,o);
					objects.SetValue(des,i++);
				}
				return objects;
			}else{
				var des = template.FromJsonValue(elementType,v);
					
				var objects = Array.CreateInstance(elementType,1);
				objects.SetValue(des,0);
				return objects;
			}
		}
		
	}
}

