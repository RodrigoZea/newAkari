using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class flagWin : MonoBehaviour {
    /*Llama a nuevo nivel*/
    public string levelName;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*Al colisionar el personaje cambia de escena*/
        if(collision.gameObject.tag.Equals("Player"))
        {
            SceneManager.LoadScene(levelName);
        }
    }
}
