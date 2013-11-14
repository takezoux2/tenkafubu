using System;
using Tenkafubu.Reflection;

namespace Tenkafubu.Sqlite.Converter
{
	public class ReflectionConverterGenerator : ConverterGenerator
	{
		
		ConverterRepository repo;
		public ReflectionConverterGenerator ()
		{
		}
		public void SetConverterRepository (ConverterRepository repo)
		{
			this.repo = repo;
		}
		
		
		public TableConverter<T> GenerateConverter<T> ()
		{
			var desc = ClassDescCache.Default.GetDesc<T>();
			return new ReflectionConverter<T>(desc,repo);
		}
	}
}

