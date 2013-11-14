using System;
using Tenkafubu.Util;

namespace Tenkafubu.Sqlite.Converter
{
	public class IntColumnConverter : ColumnConverter
	{
		
		public string ColumnType {
			get {
				return "INTEGER";
			}
		}
		public object ConvertTo (object v)
		{
			return v;
		}
		public object ConvertFrom (SqliteDatabase.ResultSet rs, string columnName)
		{
			return rs.GetInt(columnName);
		}
	}
	public class LongColumnConverter : ColumnConverter
	{
		
		public string ColumnType {
			get {
				return "INTEGER";
			}
		}
		public object ConvertTo (object v)
		{
			return v;
		}
		public object ConvertFrom (SqliteDatabase.ResultSet rs, string columnName)
		{
			return rs.GetLong(columnName);
		}
	}
	public class FloatColumnConverter : ColumnConverter
	{
		
		public string ColumnType {
			get {
				return "REAL";
			}
		}
		public object ConvertTo (object v)
		{
			return v;
		}
		public object ConvertFrom (SqliteDatabase.ResultSet rs, string columnName)
		{
			return rs.GetFloat(columnName);
		}
	}
	public class DoubleColumnConverter : ColumnConverter
	{
		
		public string ColumnType {
			get {
				return "REAL";
			}
		}
		public object ConvertTo (object v)
		{
			return v;
		}
		public object ConvertFrom (SqliteDatabase.ResultSet rs, string columnName)
		{
			return rs.GetDouble(columnName);
		}
	}
	public class StringColumnConverter : ColumnConverter
	{
		
		public string ColumnType {
			get {
				return "TEXT";
			}
		}
		public object ConvertTo (object v)
		{
			return v;
		}
		public object ConvertFrom (SqliteDatabase.ResultSet rs, string columnName)
		{
			return rs.GetString(columnName);
		}
	}
	public class DateTimeColumnConverter : ColumnConverter
	{
		
		public string ColumnType {
			get {
				return "INTEGER";
			}
		}
		public object ConvertTo (object v)
		{
			return TimeUtil.ToUnixTime((DateTime)v);
		}
		public object ConvertFrom (SqliteDatabase.ResultSet rs, string columnName)
		{
			return TimeUtil.FromUnixTime( rs.GetLong(columnName));
		}
	}
	public class BoolColumnConverter : ColumnConverter{
		public string ColumnType {
			get {
				return "INTEGER";
			}
		}

		public object ConvertTo (object v)
		{
			if((bool)v){
				return 1;
			}else{
				return 0;
			}
		}

		public object ConvertFrom (SqliteDatabase.ResultSet rs, string columnName)
		{
			return rs.GetInt(columnName) > 0;
		}
	}
}

