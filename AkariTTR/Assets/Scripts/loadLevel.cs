using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadLevel : MonoBehaviour {
    public string levelName;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void changeLevelButton(string lvlName)
    {
        SceneManager.LoadScene(lvlName);
    }

    public void changeLevelN()
    {
        SceneManager.LoadScene(levelName);
    }
}
