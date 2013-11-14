using System;

namespace Tenkafubu.Json
{
	public interface TemplateGenerator
	{
		bool IsTarget(Type t);
		void SetRepository(TemplateRepository repository);
		JsonizeTemplate Generate(Type t);
	}
}

