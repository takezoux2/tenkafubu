using System;
using System.Collections;
using System.Collections.Generic;
using Tenkafubu.Json.Template;

namespace Tenkafubu.Json
{
	public class TemplateRepository
	{
		
		static TemplateRepository instance;
		public static TemplateRepository Instance{
			get{
				if(instance == null){
					instance = new TemplateRepository();
					RegisterDefaultTemplates(instance);
				}
				
				return instance;
			}
		}
		
		static void RegisterDefaultTemplates(TemplateRepository repo){
			
			repo.AddGenerator(new ReflectionTemplateGenerator());
			repo.Register(new ArrayTemplate());
			repo.Register(new ListTemplate());
			repo.Register(typeof(int),new IntTemplate());
			repo.Register(typeof(long),new LongTemplate());
			repo.Register(typeof(float),new FloatTemplate());
			repo.Register(typeof(double),new DoubleTemplate());
			repo.Register(typeof(DateTime),new DateTimeTemplate());
			repo.Register(typeof(string),new StringTemplate());
			repo.Register(typeof(bool), new BoolTemplate());
		}
		
		
		Dictionary<Type,JsonizeTemplate> templates = new Dictionary<Type,JsonizeTemplate>();
		List<DynamicJsonizeTemplate> dynamicTemplates = new List<DynamicJsonizeTemplate>();
		List<TemplateGenerator> generators = new List<TemplateGenerator>();
		
		private TemplateRepository ()
		{
		}
		
		public void Register(Type t , JsonizeTemplate template){
			if(template is DynamicJsonizeTemplate){
				var dt = template as DynamicJsonizeTemplate;
				dt.SetReopsitory(this);
				dynamicTemplates.Add(dt);
			}else{
				templates[t] = template;
			}
		}
		public void Register(DynamicJsonizeTemplate template){
			template.SetReopsitory(this);
			dynamicTemplates.Add(template);
		}
		public void AddGenerator(TemplateGenerator generator){
			generator.SetRepository(this);
			if(generators.Count == 0){
				generators.Add(generator);
			}else{
				generators.Insert(0,generator);
			}
		}
		public void AddGeneratorToLast(TemplateGenerator generator){
			generators.Add(generator);
		}
		
		public JsonizeTemplate GetTemplate<T>(){
			return GetTemplate(typeof(T));
		}
		public JsonizeTemplate GetTemplate(Type t){
			if(templates.ContainsKey(t)){
				return templates[t];
			}else{
				DynamicJsonizeTemplate dTemplate = FindDynamicTemplate(t);
				if(dTemplate != null){
					return dTemplate;
				}
				
				var template = GenerateTemplate(t);
				if(template != null){
					templates[t] = template;
					return template;
				}else{
					throw new TemplateNotFoundException(t);
				}
			}
		}
		protected DynamicJsonizeTemplate FindDynamicTemplate(Type t){
			foreach(var template in dynamicTemplates){
				if(template.IsTarget(t)) return template;
			}
			return null;
		}
		
		protected JsonizeTemplate GenerateTemplate(Type t){
			foreach(var generator in generators){
				if(generator.IsTarget(t)){
					return generator.Generate(t);
				}
			}
			return null;
		}
		
	}
}

