using System;
using System.Reflection;
using System.Collections.Generic;

namespace Tenkafubu.Reflection
{
	public class ClassDescCache
	{
		static ClassDescCache defaultInstance;
		public static ClassDescCache Default{
			get{
				if(defaultInstance == null) defaultInstance = new ClassDescCache();
				return defaultInstance;
			}
		}
		
		Dictionary<Type,ClassDesc> cache = new Dictionary<Type, ClassDesc>();
		ClassDescMaker classDescMaker = new ClassDescMaker();
		
		public ClassDescCache ()
		{
		}
		
		
		public ClassDesc GetDesc<T>(){
			return GetDesc(typeof(T));
		}
		public ClassDesc GetDesc(Type t){
			if(cache.ContainsKey(t)){
				return cache[t];
			}else{
				var cd = classDescMaker.GetClassDesc(t);
				cache[t] = cd;
				return cd;
			}
		}
		
		public void Clear(){
			cache.Clear();
		}
		
		
	}
	
	public class ClassDescMaker{
		public ClassDesc GetClassDesc(Type t){
			
			
			var fields = t.GetFields(BindingFlags.Public | BindingFlags.Instance);
			
			var fieldDescs = new List<FieldDesc>();
			foreach(var f in fields){
				
				var fd = new FieldDesc(f);
				fieldDescs.Add(fd);
				var ignoreAtt = GetAttribute<IgnoreAttribute>(f);
				if(ignoreAtt != null){
					fd.ignoreInDB = ignoreAtt.IgnoreInDB;
					fd.ignoreInJson = ignoreAtt.IgnoreInJSON;
				}
				var nameAtt = GetAttribute<FieldInfoAttirbute>(f);
				if(nameAtt != null){
					if(nameAtt.NameForDB != null) fd.nameForDB = nameAtt.NameForDB;
					if(nameAtt.NameForJSON != null) fd.nameForJson = nameAtt.NameForJSON;
				}
				var keyAtt = GetAttribute<KeyAttribute>(f);
				if(keyAtt != null){
					fd.isKey = true;
					if(keyAtt.IsPrimary) fd.isPrimary = true;
					if(keyAtt.AutoIncrement) fd.autoIncrement = true;
				}
			}
			
			var cd = new ClassDesc(t,fieldDescs.ToArray());
			
			ValidateKey(cd,fieldDescs);
			
			var classInfoAtt = GetAttribute<ClassInfoAttribute>(t);
			if(classInfoAtt != null){
				if(classInfoAtt.TableName != null){
					cd.tableName = classInfoAtt.TableName;
				}
			}
			
			return cd;
		}
		
		void ValidateKey(ClassDesc desc,List<FieldDesc> fieldDescs){
			var keyCount = 0;
			var hasPrimary = false;
			foreach(var f in fieldDescs){
				if(f.isKey) {
					keyCount ++;
				}
				if(f.isPrimary){
					hasPrimary = true;
					desc.primaryKeyField = f;
				}
			}
			if(keyCount == 0){
				foreach(var f in fieldDescs){
					if(f.Name.ToLower() == "id"){
						f.isKey = true;
						f.isPrimary = true;
						desc.primaryKeyField = f;
					}
				}
			}else if(keyCount == 1 && !hasPrimary){
				foreach(var f in fieldDescs){
					if(f.isKey){
						f.isPrimary = true;
						desc.primaryKeyField = f;
					}
				}
			}
		}
		
		T GetAttribute<T>(Type t) where T : Attribute{
			var attributes = t.GetCustomAttributes(typeof(T),true);
			if(attributes != null && attributes.Length > 0){
				return (T)attributes[0];
			}else{
				return null;
			}
		}
		
		T GetAttribute<T>(FieldInfo field) where T : Attribute{
			var attributes = field.GetCustomAttributes(typeof(T),true);
			if(attributes != null && attributes.Length > 0){
				return (T)attributes[0];
			}else{
				return null;
			}
		}
	}
	
	public class ClassDesc{
		public Type t;
		
		public string tableName;
		
		public FieldDesc[] fields;
		
		public FieldDesc primaryKeyField;
		
		
		public ClassDesc(Type t,FieldDesc[] fields){
			this.t = t;
			tableName = t.Name;
			this.fields = fields;
		}
	}
	
	public class FieldDesc{
		
		public FieldInfo field;
		
		public string nameForJson;
		
		public string nameForDB;
		public bool ignoreInJson = false;
		public bool ignoreInDB  = false;
		
		public bool isKey = false;
		public bool isPrimary = false;
		public bool autoIncrement = false;
		public string Name{get{return field.Name;}}
		public Type FieldType{get{ return field.FieldType;}}
		
		public FieldDesc(FieldInfo field){
			this.field = field;
			nameForJson = field.Name;
			nameForDB = field.Name;
		}
		
		public void Set(object instance, object value){
			field.SetValue(instance,value);
		}
		
		public object Get(object instance){
			return field.GetValue(instance);
		}
		
		
	}
	
}

