using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Items : MonoBehaviour {
    public Rigidbody2D akari;
    
    private float springForce = 1050f;

    public GameObject st1, st2, st3;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Coin"))
        {
            if(collision.name == "coinGold")
            {
                GameController.instance.points += 30f;
                GameObject.Destroy(GameObject.Find("coinGold"));
            }else if(collision.name == "coinBronze")
            {
                GameController.instance.points += 10f;
                GameObject.Destroy(GameObject.Find("coinBronze"));
            }else if(collision.name == "coinSilver")
            {
                GameController.instance.points += 20f;
                GameObject.Destroy(GameObject.Find("coinSilver"));
            }
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
                st2.SetActive(true);
                
            }
            if (collision.name == "star2")
            {
                GameObject.Destroy(GameObject.Find("star2"));
                st3.SetActive(true);
            }
            if (collision.name == "star3")
            {
                GameObject.Destroy(GameObject.Find("star3"));
                st1.SetActive(true);
            }
        }

        
    }
}
