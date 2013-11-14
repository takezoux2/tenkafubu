using System;
using Tenkafubu.Json;
using Tenkafubu.Reflection;
using Tenkafubu.Sqlite;

namespace Tenkafubu
{
	public static class MemoryCleaner
	{
		/// <summary>
		/// Cleans the memory.
		/// Set all cache reference null to make gc target.
		/// </summary>
		public static void CleanMemory(){
			ConverterRepository.CleanMemory();
			Jsonizer.CleanMemory();
			ConverterRepository.CleanMemory();
			ClassDescCache.CleanMemory();
		}
	}
}

