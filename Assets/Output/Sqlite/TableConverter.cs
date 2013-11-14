using System;

namespace Tenkafubu.Sqlite
{
	public interface TableConverterRoot{
	}
	
	public interface TableConverter<T> : TableConverterRoot
	{
		string TableName{get;}
		
		T FromRow(SqliteDatabase.ResultSet resultSet);
		
		string ToSelectByPK(object pk);
		string ToSelectAll();
		string ToInsert(T v);
		string ToUpdate(T v);
		string ToDelete(T v);
		string ToDeleteAll();
		string ToCreateTable();
		string ToDropTable();
		
	}
}

