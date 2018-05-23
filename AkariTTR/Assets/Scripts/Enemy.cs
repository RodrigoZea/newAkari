using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    /*Se declara GameObject del ninja*/
    public GameObject ninja;

    /*Se declaran vaiables para mecanica de enemigo*/
    private float speed = 4f;
    private int pos = 1;
    private bool facingRight = true;
    AudioSource audioS;
    SpriteRenderer sr;
    public float posMin;
    public float posMax;

    // Use this for initialization
    void Start () {
        /*Se obtienen los componenetes de los objetos*/
        sr = GetComponent<SpriteRenderer>();
        audioS = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        /*Se ejecuta movimiento de ninja*/
        ninja.transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime * pos);

        if (ninja.transform.position.x <= posMin)
        {
            /*Da vuelta al llegar posicion minima*/
            pos = 1;
            facingRight = true;
        }
        if (ninja.transform.position.x >= posMax)
        {
            /*Da vuelta al llegar a posicion maxima*/
            pos = -1;
            facingRight = false;
        }
        /*Realiza el giro*/
        sr.flipX = !facingRight;
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*Al colisionar con espada...*/
        if (collision.gameObject.tag.Equals("Sword"))
        {
            /*Suma puntos y muere ninja*/
            GameController.instance.points += 25f;
            audioS.Play();
            Destroy(ninja);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*Al colisionar con espada*/
        if (collision.tag.Equals("Sword"))
        {
            /*Suma puntos y muere ninja*/
            GameController.instance.points += 25f;
            audioS.Play();
            Destroy(ninja);

        }
    }
}
