using System;

namespace Tenkafubu.Sqlite
{
	public interface ColumnConverter
	{
		string ColumnType{ get;}
		object ConvertTo(object v);
		object ConvertFrom(SqliteDatabase.ResultSet rs,string columnName);
	}
}

