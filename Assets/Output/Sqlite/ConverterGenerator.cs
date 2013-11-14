using System;

namespace Tenkafubu.Sqlite
{
	public interface ConverterGenerator
	{
		void SetConverterRepository(ConverterRepository repo);
		TableConverter<T> GenerateConverter<T>();
	}
}

