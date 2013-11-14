using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;


namespace Tenkafubu.Sqlite
{
	public static class SQLMaker
	{
		
		
		
		
		public static string ValueToBlock(object v){
			if(v == null){
				return "NULL";
			}else if(v is string){
				return "'" + Escape(v.ToString()) + "'";
			}else {
				return v.ToString();
			}
		}
		
		public static string Escape(string str){
			return str.Replace("'","''");
		}
		
		
	}
	public static class SQLExtension{
		
		public static string SQLFormat(this string str,params object[] ps){
			
			var v = from p in ps select Convert(p);
			
			return string.Format(str, v.ToArray());
		}
		
		public static string Convert(object v){
			if(v == null){
				return "NULL";
			}else if(v is DateTime){
				return ((DateTime)v).ToBinary().ToString();
			}else if( v is string){
				return "'" + SQLMaker.Escape((string)v) + "'";
			}else{
				return v.ToString();
			}
		}
	}
}

