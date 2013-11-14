using System;

namespace Tenkafubu.Sqlite
{
	public class DB
	{
		SqliteDatabase database;
		ConverterRepository repo;
		
		public SqliteDatabase Database{
			get{
				return database;
			}
		}
		
		public DB (SqliteDatabase database)
		{
			this.database = database;
			repo = ConverterRepository.Default;
		}
		
		public TableOperation<T> GetOperation<T>() where T : class{
			return new TableOperation<T>(database,repo.GetTableConverter<T>());
		}
		
		public void Close(){
			database.Close();
		}
		
		
	}
}

