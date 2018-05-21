using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public GameObject ninja;
    private float speed = 4f;
    private int pos = 1;
    private bool facingRight = true;
    SpriteRenderer sr;
    public float posMin;
    public float posMax;

    // Use this for initialization
    void Start () {
        sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        ninja.transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime * pos);
        if (ninja.transform.position.x <= posMin)
        {
            pos = 1;
            facingRight = true;
        }
        if (ninja.transform.position.x >= posMax)
        {
            pos = -1;
            facingRight = false;
        }
        sr.flipX = !facingRight;
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        

        if (collision.gameObject.tag.Equals("Sword"))
        {
            Debug.Log("Adios");
            GameController.instance.points += 25f;
            Destroy(ninja);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag.Equals("Sword"))
        {
            Debug.Log("Hola");
            GameController.instance.points += 25f;
            Destroy(ninja);
        }
    }
}
