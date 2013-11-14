using System;
using Tenkafubu.Reflection;

namespace Tenkafubu.Json.Template
{
	public class ReflectionTemplateGenerator : TemplateGenerator
	{
		
		public ClassDescCache classDescCache;
		
		public ReflectionTemplateGenerator(){
			classDescCache = ClassDescCache.Default;
		}
		
		public bool IsTarget (Type t)
		{
			return t.IsClass;
		}
		
		TemplateRepository repo;
		
		public void SetRepository (TemplateRepository repository)
		{
			this.repo = repository;
		}

		public JsonizeTemplate Generate (Type t)
		{
			var classDesc = classDescCache.GetDesc(t);
			return new ReflectionTemplate(classDesc,repo);
		}
	}
}

