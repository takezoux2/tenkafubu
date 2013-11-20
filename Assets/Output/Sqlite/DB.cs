using System;

namespace Tenkafubu.Sqlite
{
	public class DB
	{
		SqliteDatabase database;
		ConverterRepository repo;
		string filePath;

		public SqliteDatabase Database{
			get{
				return database;
			}
		}
		
		public DB (string filePath)
		{
			this.filePath = filePath;
			Reopen();
			repo = ConverterRepository.Default;
		}
		
		public TableOperation<T> GetOperation<T>() where T : class{
			return new TableOperation<T>(database,repo.GetTableConverter<T>());
		}

		public void Reopen(){
			if(database != null){
				Close ();
			}
			database = new SqliteDatabase();
			database.Open(filePath);
		}


		public void DeleteDBFile(){
			Close ();
			System.IO.File.Delete(filePath);
			Reopen ();
		}
		
		public void Close(){
			if(database !=null){
				database.Close();
				database = null;
			}
		}
		
		
	}
}

