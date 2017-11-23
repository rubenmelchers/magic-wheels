using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginComponent : MonoBehaviour {

	public InputField username;
	public InputField password;

	private string Username;
	private string Password;
	private string form;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Username = username.text;
		Password = password.text;

//		Debug.Log (Username);
	}
}
