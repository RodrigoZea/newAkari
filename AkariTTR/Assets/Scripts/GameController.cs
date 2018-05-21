using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    /*Instancia para GameController*/
    public static GameController instance;
    /*Tiempo transcurrido para score*/
    public float points;
    /*UI para mostrar texto con score*/
    public Text score;
    
    private void Awake()
    {
        /*Se crea el instance al ejecutarse el programa*/
        instance = this;
        points = 0;
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        score.text = "SCORE: " + points.ToString();
    }
}
