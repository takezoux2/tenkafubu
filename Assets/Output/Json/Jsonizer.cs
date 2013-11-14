using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Tenkafubu.Json{

	public class Jsonizer {
	
		TemplateRepository repository;
		
		public Jsonizer(){
			repository = TemplateRepository.Instance;
		}
		
		public void RegisterTemplate<T>(JsonizeTemplate template){
			repository.Register(typeof(T),template);
		}
		
		public JsonizeTemplate GetTemplate<T>(){
			return repository.GetTemplate<T>();
		}
		
		public object ToJsonObject(object v){
			var template = repository.GetTemplate(v.GetType());
			try{
				return template.ToJsonValue(v);
			}catch(Exception e){
				Debug.LogError(string.Format("Fail to convert {0} to json",v.GetType()));
				throw e;
			}	
		}
		public string ToJson(object v){
			var obj = ToJsonObject(v);
			return Jsonize.Serialize(obj);
		}
		
		public T FromJsonObject<T>(object jsonObj){
			var template = repository.GetTemplate<T>();
			try{
				var v = template.FromJsonValue(typeof(T),jsonObj);
				return (T)v;
			}catch(Exception e){
				Debug.LogError(string.Format("Fail to convert {0} to json",typeof(T)));
				throw e;
			}	
		}
		public T FromJson<T>(string json) {
			var jsonObj = Jsonize.Deserialize(json);
			return FromJsonObject<T>(jsonObj);
		}
		
		public List<T> FromJsonObjectByList<T>(object jsonArray){
			var template = repository.GetTemplate<T>();
			
			try{
				var list = new List<T>();
				if(jsonArray is IList){
					foreach(var o in (jsonArray as IList)){
						list.Add((T)template.FromJsonValue(typeof(T),o));
					}
				}else{
					list.Add((T)template.FromJsonValue(typeof(T),jsonArray));
				}
				return list;
			}catch(Exception e){
				Debug.LogError(string.Format("Fail to convert {0} to json",typeof(T)));
				throw e;
			}	
		}
		
		public List<T> FromJsonByList<T>(string json){
			var jsonObj = Jsonize.Deserialize(json);
			return FromJsonObjectByList<T>(jsonObj);
		}
		
		
		
		
	} 
	
}
