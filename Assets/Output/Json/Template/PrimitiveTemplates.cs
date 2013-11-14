using System;


using Tenkafubu.Util;
namespace Tenkafubu.Json.Template
{
	public class BoolTemplate : JsonizeTemplate
	{
		public object ToJsonValue (object obj)
		{
			return obj;
		}

		public object FromJsonValue (Type t ,object v)
		{
			if(v is bool)return (bool)v;
			else{
				return bool.Parse(v.ToString());
			}
		}
	}
	
	public class IntTemplate : JsonizeTemplate
	{
		public object ToJsonValue (object obj)
		{
			return obj;
		}

		public object FromJsonValue (Type t ,object v)
		{
			if(v is long) return (int)(long)v;
			else if(v is int) return (int)v;
			else if(v is string) return int.Parse((string)v);
			else if(v is double) return (int)(double)v;
			else if(v is float) return (int)(float)v;
			else return int.Parse(v.ToString());
		}
	}
	
	public class LongTemplate : JsonizeTemplate
	{
		public object ToJsonValue (object obj)
		{
			return obj;
		}

		public object FromJsonValue (Type t ,object v)
		{
			if(v is long) return (long)v;
			else if(v is int) return (long)(int)v;
			else if(v is string) return long.Parse((string)v);
			else if(v is double) return (long)(double)v;
			else if(v is float) return (long)(float)v;
			else return long.Parse(v.ToString());
		}
	}
	public class FloatTemplate : JsonizeTemplate
	{
		public object ToJsonValue (object obj)
		{
			return obj;
		}

		public object FromJsonValue (Type t ,object v)
		{
			if(v is long) return (float)(long)v;
			else if(v is int) return (float)(int)v;
			else if(v is string) return float.Parse((string)v);
			else if(v is double) return (float)(double)v;
			else if(v is float) return (float)v;
			else return float.Parse(v.ToString());
		}
	}
	public class DoubleTemplate : JsonizeTemplate
	{
		public object ToJsonValue (object obj)
		{
			return obj;
		}

		public object FromJsonValue (Type t ,object v)
		{
			if(v is long) return (double)(long)v;
			else if(v is int) return (double)(int)v;
			else if(v is string) return double.Parse((string)v);
			else if(v is double) return (double)v;
			else if(v is float) return (double)(float)v;
			else return double.Parse(v.ToString());
		}
	}
	public class StringTemplate : JsonizeTemplate
	{
		public object ToJsonValue (object obj)
		{
			if(obj == null) return JNothing.Nothing;
			else return obj;
		}

		public object FromJsonValue (Type t ,object v)
		{
			if(v == null) return null;
			else if( v is string){
				return (string)v;
			}else{
				return v.ToString();
			}
		}
	}
	public class DateTimeTemplate : JsonizeTemplate
	{
		public object ToJsonValue (object obj)
		{
			return TimeUtil.ToUnixTime((DateTime) obj);
		}

		public object FromJsonValue (Type t ,object v)
		{
			if( v is long){
				return TimeUtil.FromUnixTime((long)v);
			}else{
				return TimeUtil.FromUnixTime(long.Parse(v.ToString()));
			}
		}
	}
}

