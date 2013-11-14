using System;
using Tenkafubu.Json;
namespace Tenkafubu.Sqlite.Converter
{
	public class JsonColumnConverter : ColumnConverter
	{
		Jsonizer jsonizer;
		Type t;
		public JsonColumnConverter(Type t){
			this.t = t;
			jsonizer = Jsonizer.Default;
		}
		public JsonColumnConverter(Type t,Jsonizer jsonizer){
			this.jsonizer = jsonizer;
			this.t = t;
		}
		
		public string ColumnType {
			get {
				return "TEXT";
			}
		}

		public object ConvertTo (object v)
		{
			return jsonizer.ToJson(v);
		}

		public object ConvertFrom (SqliteDatabase.ResultSet rs, string columnName)
		{
			return jsonizer.FromJson(t,rs.GetString(columnName));
		}
	}
}

