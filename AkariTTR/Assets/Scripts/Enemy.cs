using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public GameObject ninja;
    private float speed = 4f;
    private int pos = 1;
    private bool facingRight = true;
    AudioSource audioS;
    SpriteRenderer sr;
    public float posMin;
    public float posMax;

    // Use this for initialization
    void Start () {
        sr = GetComponent<SpriteRenderer>();
        audioS = GetComponent<AudioSource>();
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
            
            GameController.instance.points += 25f;
            audioS.Play();
            Destroy(ninja);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag.Equals("Sword"))
        {
            GameController.instance.points += 25f;
            audioS.Play();
            Destroy(ninja);

        }
    }
}
