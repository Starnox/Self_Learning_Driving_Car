using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class back_menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	public void back_to_menu()
    {
        Application.LoadLevel("Meniu");
    }
}
