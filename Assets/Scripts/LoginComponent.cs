using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginComponent : MonoBehaviour {

	public InputField 	username;
	public InputField 	password;


	private string Username;
	private string Password;
	private string form;

	// Use this for initialization
	void Start () {
	}

	
	// Update is called once per frame
	void Update () {

	}

	public void LoginClick() {
		Debug.Log ("clicked on the button");

		Username = username.text;
		Password = password.text;


		if (Username == "admin" && Password == "geheim") {

			SceneManager.LoadScene ("race_area01", LoadSceneMode.Additive);

		}
	}
}
