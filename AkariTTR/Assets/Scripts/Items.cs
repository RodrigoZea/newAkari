﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Items : MonoBehaviour {

    public GameObject goldCoin;
    public GameObject spring;
    public Rigidbody2D akari;
    
    private float springForce = 1050f;

    // Use this for initialization
    void Start () {

        //GameObject.Find("Star1").SetActive(false);
        //GameObject.Find("Star2").SetActive(false);
        //GameObject.Find("Star3").SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("GoldCoin"))
        {
            GameController.instance.points += 10f;
            GameObject.Destroy(goldCoin);
        }
        if (collision.tag.Equals("Spring"))
        {
            akari.AddForce(Vector2.up * springForce);

        }
        if (collision.tag.Equals("Star"))
        {
            GameController.instance.points += 100f;

            if (collision.name == "star1")
            {
                GameObject.Destroy(GameObject.Find("star1"));
                //GameObject.Find("Star1").SetActive(true);
            }
            if (collision.name == "star2")
            {
                GameObject.Destroy(GameObject.Find("star2"));
                //GameObject.Find("Star2").SetActive(true);
            }
            if (collision.name == "star3")
            {
                GameObject.Destroy(GameObject.Find("star3"));
                //GameObject.Find("Star3").SetActive(true);
            }
        }

        
    }
}