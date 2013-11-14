using System;

namespace Tenkafubu
{
	
	public class ClassInfoAttribute : Attribute{
		public string TableName;
	}
	
	public class KeyAttribute : Attribute{
		public bool AutoIncrement;
		public bool IsPrimary;
	}
	
	
	public class FieldInfoAttirbute : Attribute{
			
		public string NameForJSON;
		public string NameForDB;
		
	}
	
	public class IgnoreAttribute : Attribute{
		public bool IgnoreInJSON;
		public bool IgnoreInDB;
	}
}

