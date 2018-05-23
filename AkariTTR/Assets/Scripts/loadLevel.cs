using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadLevel : MonoBehaviour {
    /*Se toma el nombre del nivel*/
    public string levelName;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void changeLevelButton(string lvlName)
    {
        /*Realiza cambio de escena con boton*/
        SceneManager.LoadScene(lvlName);
    }

    public void changeLevelN()
    {
        /*Realiza cambio de escena*/
        SceneManager.LoadScene(levelName);
    }
}
