using UnityEngine;
using System.Collections;
using System;
using Tenkafubu.Json;

public class SampleCode : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		var user = new User();
		user.id = 3;
		user.name = "hifoe";
		user.age = 34;
		user.lastLogin = DateTime.Now;
		
		var jsonizer = Jsonizer.Default;
		var json = jsonizer.ToJson(user);
		
		Debug.Log("Json = " + json);
		
		var deserialize = jsonizer.FromJson<User>(json);
		
		Debug.Log("Des = " + deserialize);
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

public class User{
	public long id;
	public string name;
	public int age;
	public DateTime lastLogin;
	
	public override string ToString ()
	{
		return "(" + id + "," + name + "," + age + "," + lastLogin + ")";
	}
}
