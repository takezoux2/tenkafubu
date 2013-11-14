using System;

namespace Tenkafubu.Json
{
	public class TemplateNotFoundException : Exception
	{
		public TemplateNotFoundException (Type t) : base("Template for " + t + " not found")
		{
		}
	}
}

