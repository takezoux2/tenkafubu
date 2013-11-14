using System;
using System.Collections;
using System.Collections.Generic;


namespace Tenkafubu.Json.Template
{
	public class MapTemplate : DynamicJsonizeTemplate
	{
		
		TemplateRepository repository;
		
		public bool IsTarget (Type t)
		{
			return typeof(IDictionary).IsAssignableFrom(t);
		}

		public void SetReopsitory (TemplateRepository repository)
		{
			this.repository = repository;
		}

		public object ToJsonValue (object obj)
		{
			if(obj == null) return JNothing.Nothing;
			else{
				var jObject = new Dictionary<string,object>();
				
				foreach(DictionaryEntry e in (obj as IDictionary)){
					var key = e.Key.ToString();
					var template = repository.GetTemplate(e.Value.GetType());
					var v = template.ToJsonValue(e.Value);
					if(v != JNothing.Nothing){
						jObject[key] = v;
					}
				}
				
				return jObject;
				
			}
		}

		public object FromJsonValue (Type t, object v)
		{
			return v;
		}
		
	}
}

