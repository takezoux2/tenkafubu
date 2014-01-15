using System;

using System.Collections.Generic;

namespace Tenkafubu.Json
{
	public interface JsonizeTemplate
	{
		object ToJsonValue(object obj);
		object FromJsonValue(Type t ,object v);
	}
	
	public interface DynamicJsonizeTemplate : JsonizeTemplate{
		bool IsTarget(Type t);
		void SetReopsitory(TemplateRepository repository);
	}

	public interface WithTargetClass : JsonizeTemplate
	{
		Type[] Classes{get;}
	}
	
	
}

