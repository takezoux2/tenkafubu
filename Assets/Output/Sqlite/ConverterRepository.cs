using System;
using System.Collections.Generic;
using Tenkafubu.Sqlite.Converter;

namespace Tenkafubu.Sqlite
{
	public class ConverterRepository
	{
		
		static ConverterRepository defaultInstance;
		public static ConverterRepository Default{
			get{
				if(defaultInstance == null){
					defaultInstance = new ConverterRepository();
					SetUp(defaultInstance);
				}
				return defaultInstance;
			}
		}
		
		static void SetUp(ConverterRepository repo){
			repo.SetTableConverterGenerator(new ReflectionConverterGenerator()); 
			repo.RegisterConverter<int>(new IntColumnConverter());
			repo.RegisterConverter<long>(new LongColumnConverter());
			repo.RegisterConverter<float>(new FloatColumnConverter());
			repo.RegisterConverter<double>(new DoubleColumnConverter());
			repo.RegisterConverter<string>(new StringColumnConverter());
			repo.RegisterConverter<DateTime>(new DateTimeColumnConverter());
			repo.RegisterConverter<bool>(new BoolColumnConverter());
		}
		
		Dictionary<Type,ColumnConverter> columnConverters = new Dictionary<Type, ColumnConverter>();
		Dictionary<Type,TableConverterRoot> tableConverters = new Dictionary<Type, TableConverterRoot>();
	    ConverterGenerator tableConvGenerator;
		
		
		public ConverterRepository ()
		{
		}
		
		public void RegisterConverter<T>(ColumnConverter c){
			columnConverters[typeof(T)] = c;
		}
		public void RegisterConverter<T>(TableConverter<T> c){
			tableConverters[typeof(T)] = c;
		}
		
		public void SetTableConverterGenerator(ConverterGenerator cg){
			cg.SetConverterRepository(this);
			tableConvGenerator = cg;
		}
		
		public ColumnConverter GetColumConverter(Type t){
			return columnConverters[t];
		}
		
		public TableConverter<T> GetTableConverter<T>(){
			Type t = typeof(T);
			if(tableConverters.ContainsKey(t)){
				return (TableConverter<T>)tableConverters[t];
			}else{
				var tableConv = tableConvGenerator.GenerateConverter<T>();
				tableConverters[t] = tableConv;
				return tableConv;
			}
		}
		
		
	}
}

