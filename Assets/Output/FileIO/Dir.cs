// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using System;

using UnityEngine;
using System.IO;

namespace Tenkafubu.FileIO
{
	public class Dir
	{
		static Dir resourceDir;
		static Dir tempDir;
		static Dir cacheDir;
		static Dir recycleCacheDir;
		public static Dir TempDir{
			get{
				if(tempDir == null)tempDir = new Dir(Application.temporaryCachePath);
				return tempDir;
			}
		}
		public static Dir CacheDir{
			get{
				if(cacheDir == null)cacheDir = new Dir(Application.temporaryCachePath);
				return cacheDir;
			}
		}
		public static Dir ResourceDir{
			get{
				if(resourceDir == null)resourceDir = new Dir(Application.persistentDataPath);
				return resourceDir;
			}
		}
		
		string baseDir;
		public Dir (string baseDir)
		{
			Debug.Log("Prepare " + baseDir);
			this.baseDir = baseDir;
		}
		
		public string GetAbsolutePath(string filename){
			string path = Path.Combine(baseDir,filename);
			FileInfo info = new FileInfo(path);
			if(!info.Directory.Exists){
				Debug.Log("Create dir: " + info.Directory.FullName);
				info.Directory.Create();
			}
			
			return path;
		}
		
		public string Save(string filename,byte[] data){
			string filePath = Path.Combine(baseDir,filename);
			var fi = new FileInfo(filePath);
			if(!fi.Directory.Exists){
				fi.Directory.Create();
			}
			Debug.Log(string.Format("Save file to {0}",filePath));
			var stream = fi.Create();
			stream.Write(data,0,data.Length);
			stream.Flush();
			stream.Close();
			
			return filePath;
			
		}
		
		public byte[] Load(string filename){
			string filePath = Path.Combine(baseDir,filename);
			var fi = new FileInfo(filePath);
			if(fi.Exists){
				
				var stream = fi.OpenRead();
				byte[] data = new byte[fi.Length];
				stream.Read(data,0,data.Length);
				stream.Close();
				return data;
			}else{
				Debug.Log(string.Format("File {0} not found",filePath));
				return null;
			}
			
		}
		
		public bool Exists(string filename){
			string filePath = Path.Combine(baseDir,filename);
			var fi = new FileInfo(filePath);
			return fi.Exists;
		}
		
		public bool Delete(string filename){
			string filePath = Path.Combine(baseDir,filename);
			var fi = new FileInfo(filePath);
			if(fi.Exists){
				Debug.Log("Delete file:" + filePath);
				fi.Delete();
				return true;
			}else{
				return false;
			}
			
		}
		public bool DeleteDir(string dir){
			var di = new DirectoryInfo(Path.Combine(baseDir,dir));
			di.Delete(true);
			return true;
		}
		
		public bool DeleteDir(){
			var di = new DirectoryInfo(baseDir);
			di.Delete(true);
			return true;
		}
		
	}
}
