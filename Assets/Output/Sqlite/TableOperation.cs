using System;
using System.Collections.Generic;

namespace Tenkafubu.Sqlite
{
	public class TableOperation<T> where T : class
	{
		SqliteDatabase database;
		TableConverter<T> converter;

		public TableConverter<T> Converter{
			get{ return converter;}
		}

		public TableOperation (SqliteDatabase database,
			TableConverter<T> tableConverter)
		{
			this.database = database;
			this.converter = tableConverter;
		}
		

		public T GetByPK(object pk){
			var rs = database.ExecuteQuery(converter.ToSelectByPK(pk));
			using(rs){
				if(rs.Next()){
				    return converter.FromRow(rs);
				}else{
					return null;
				}
			}
		}
		public List<T> GetAll() { 
			var rs = database.ExecuteQuery(converter.ToSelectAll());
			var list = new List<T>();
			using(rs){
				while(rs.Next()){
					list.Add(converter.FromRow(rs));
				}
			}
			
			return list;
		}
		
		public bool Insert(T obj){
			if( database.ExecuteNonQuery(converter.ToInsert(obj)) > 0){
				if(converter.IsAutoIncrement){
					converter.SetAutoIncrementId(obj,GetAutoIncrementValue());
				}
				return true;
			}else return false;
		}

		protected long GetAutoIncrementValue(){
			var rs = database.ExecuteQuery("SELECT last_insert_rowid();");
			using(rs){
				if(rs.Next()){
					return rs.GetLong(0);
				}else{
					return -1;
				}
			}
		}
		public bool Update(T obj){
			return database.ExecuteNonQuery(converter.ToUpdate(obj)) > 0;
		}
		public bool UpdateOrInsert(T obj){
			if(!Update(obj)){
				return Insert(obj);
			}else return true;
		}
		
		public bool Delete(T obj){
			return database.ExecuteNonQuery(converter.ToDelete(obj)) > 0;
		}
		
		public bool DeleteAll(){
			return database.ExecuteNonQuery(converter.ToDeleteAll()) > 0;
		}
		
		public bool ReplaceAll(List<T> objList){
			DeleteAll();
			foreach(var obj in objList){
				Insert(obj);
			}
			return true;
		}
		
		public List<T> Select(String sql,params object[] args){
			var rs = database.ExecuteQuery(sql.SQLFormat(args));
			var list = new List<T>();
			using(rs){
				while(rs.Next()){
					list.Add (converter.FromRow(rs));
				}
			}
			return list;
		}
		
		public void CreateTable(){
			database.ExecuteNonQuery(converter.ToCreateTable());
		}
		
		public void DropTable(){
			database.ExecuteNonQuery(converter.ToDropTable());
		}
		
	}
}

