using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingPlatform : MonoBehaviour {

    private Rigidbody2D rb2d;
    public float renewPos;
    bool falling = false;
    private Vector2 positionStart;
    public float fSpeed = 3f;
    bool timerStart = false;
    float timer;

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        positionStart = new Vector2(rb2d.position.x, rb2d.position.y);
    }
	
	// Update is called once per frame
	void Update () {
        if (timerStart)
        {
            timer += Time.deltaTime;
        }


        if (falling && timer > 1)
        {
            transform.Translate(new Vector3(0, 1) * fSpeed* Time.deltaTime * -1);
        }

        if (rb2d.position.y < renewPos)
        {
            Destroy(this.gameObject);
            falling = false;
            Respawn();
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        timerStart = true;
        falling = true;
    }

    public void Fall()
    {
        falling = true;
    }

    public void Respawn()
    {
        GameObject newP = Instantiate(this.gameObject, positionStart, Quaternion.identity);
        newP.GetComponent<BoxCollider2D>().enabled = true;
        newP.GetComponent<fallingPlatform>().enabled = true;
    }
}
