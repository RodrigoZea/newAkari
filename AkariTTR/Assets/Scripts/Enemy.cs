using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public GameObject ninjaRojo1;
    private float speed = 4f;
    private int pos = 1;
    private bool facingRight = true;
    SpriteRenderer sr;

    // Use this for initialization
    void Start () {
        sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        ninjaRojo1.transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime * pos);
        if (ninjaRojo1.transform.position.x <= 19)
        {
            pos = 1;
            facingRight = true;
        }
        if (ninjaRojo1.transform.position.x >= 25)
        {
            pos = -1;
            facingRight = false;
        }
        sr.flipX = !facingRight;
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Sword"))
        {
            GameController.instance.points += 25f;
            Destroy(this.gameObject);
        }
    }
}
