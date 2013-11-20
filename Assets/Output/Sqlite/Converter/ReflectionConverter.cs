using System;
using Tenkafubu.Reflection;
using Tenkafubu.Sqlite;
using System.Text;
using System.Collections.Generic;

namespace Tenkafubu.Sqlite.Converter
{
	public class ReflectionConverter<T> : TableConverter<T>
	{
		ClassDesc desc;
		ConverterRepository repo;
		public string TableName {
			get {
				return desc.tableName;
			}
		}

		public void SetAutoIncrementId (T obj, long id)
		{
			if(desc.primaryKeyField.FieldType == typeof(long)){
				desc.primaryKeyField.Set(obj,id);
			}else{
				desc.primaryKeyField.Set (obj,(int)id);
			}
		}

		public bool IsAutoIncrement {
			get {
				return desc.primaryKeyField.autoIncrement;
			}
		}
		
		public ReflectionConverter (ClassDesc desc,ConverterRepository repo)
		{
			this.desc = desc;
			this.repo = repo;
		}
		public T FromRow (SqliteDatabase.ResultSet resultSet)
		{
			var instance = ReflectionSupport.CreateNewInstance(typeof(T));
			foreach(var f in desc.dbFields){
				var cc = repo.GetColumConverter(f.FieldType);
				f.Set (instance,cc.ConvertFrom(resultSet,f.nameForDB));
			}
			
			return (T)instance;
		}
		
		public string ToSelectByPK (object pkValue)
		{
			if(desc.primaryKeyField == null) throw new Exception("Class " + desc.t + " doesn't have primary key field");
			
			return "SELECT * FROM " + TableName + 
				WherePKValue(pkValue) + ";";
			
		}

		public string ToSelectAll ()
		{
			return "SELECT * FROM " + TableName + ";";
		}
		public string ToInsert (T obj)
		{
			var builder = new StringBuilder();	
			builder.Append("INSERT INTO " + desc.tableName + " (");
			
			var fields = new List<Tenkafubu.Reflection.FieldDesc>(desc.dbFields);
			if(desc.primaryKeyField != null && desc.primaryKeyField.autoIncrement){
				fields.Remove(desc.primaryKeyField);
			}
			
			foreach( var f in fields){
				builder.Append(f.nameForDB+ ",");
			}
			builder.Remove(builder.Length - 1,1);
			builder.Append(") VALUES (");
			
			foreach( var f in fields){
				var cc = repo.GetColumConverter(f.FieldType);
				object v = f.Get(obj);
				builder.Append(SQLMaker.ValueToBlock(cc.ConvertTo(v)) + ",");
			}
			builder.Remove(builder.Length - 1,1);
			builder.Append(");");
			
			return builder.ToString();
		}

		public string ToUpdate (T v)
		{
			if(desc.primaryKeyField == null) throw new Exception("Class " + desc.t + " doesn't have primary key field");
			var builder = new StringBuilder();	
			builder.Append("UPDATE " + desc.tableName + " SET ");
			foreach(var f in desc.dbFields){
				if(f.ignoreInDB) continue;
				if(f.isPrimary) continue;
				var cc = repo.GetColumConverter(f.FieldType);
				object fieldValue = f.Get(v);
				builder.Append(f.nameForDB + "=" + SQLMaker.ValueToBlock(cc.ConvertTo(fieldValue)) + ",");
			}
			builder.Remove(builder.Length -1 ,1);
			
			builder.Append(WherePK(v) + ";");
			
			return builder.ToString();
		}

		public string ToDelete (T v)
		{
			if(desc.primaryKeyField == null) throw new Exception("Class " + desc.t + " doesn't have primary key field");
			var builder = new StringBuilder();	
			builder.Append("DELETE FROM " + TableName);
			builder.Append(WherePK (v) + ";");
			
			return builder.ToString();
		}
		
		string WherePK(T v){
			return WherePKValue(desc.primaryKeyField.Get (v));
		}
		string WherePKValue(object v){
			var pk = desc.primaryKeyField;
			var pkCC =repo.GetColumConverter(pk.FieldType);
			return " WHERE " + pk.nameForDB + " = " + SQLMaker.ValueToBlock(pkCC.ConvertTo(v));
		}
		
		public string ToDeleteAll ()
		{
			return "DELETE FROM " + TableName + ";";
		}

		public string ToCreateTable ()
		{
			var builder = new StringBuilder();	
			builder.Append("CREATE TABLE IF NOT EXISTS " + TableName + "(");
			foreach(var f in desc.dbFields){
				var cc = repo.GetColumConverter(f.FieldType);
				builder.Append(f.nameForDB + " " + cc.ColumnType);
				if(f.isPrimary){
					builder.Append(" PRIMARY KEY");
				}
				if( f.autoIncrement){
					builder.Append(" AUTOINCREMENT");
				}
				builder.Append(",");
			}
			builder.Remove(builder.Length - 1,1);
			builder.Append(");");
			return builder.ToString();
		}
		
		public string ToDropTable ()
		{
			return "DROP TABLE IF EXISTS " + TableName + ";";
		}
		
	}
}

