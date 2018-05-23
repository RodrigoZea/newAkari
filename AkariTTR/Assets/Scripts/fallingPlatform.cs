using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingPlatform : MonoBehaviour {

    /*Se declara cuerpo rigido*/
    private Rigidbody2D rb2d;

    /*Se declaran variables para nueva posicion de plataforma*/
    public float renewPos;
    bool falling = false;
    private Vector2 positionStart;
    public float fSpeed = 3f;
    bool timerStart = false;
    float timer;

    // Use this for initialization
    void Start () {
        /*Se obtienen las componentes de objetos*/
        rb2d = GetComponent<Rigidbody2D>();
        positionStart = new Vector2(rb2d.position.x, rb2d.position.y);
    }
	
	// Update is called once per frame
	void Update () {
        /*Se suma tiempo*/
        if (timerStart)
        {
            timer += Time.deltaTime;
        }

        /*Si la variable para que caiga y tiempo mayor a 1...*/
        if (falling && timer > 1)
        {
            /*Mueve la plataforma hacia abajo*/
            transform.Translate(new Vector3(0, 1) * fSpeed* Time.deltaTime * -1);
        }

        if (rb2d.position.y < renewPos)
        {
            /*Destruye la plataforma al llegar a posicion maxima*/
            Destroy(this.gameObject);
            falling = false;
            Respawn();
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*Al colisionar se activan variables*/
        timerStart = true;
        falling = true;
    }

    public void Fall()
    {
        /*Activa variable para caer*/
        falling = true;
    }

    public void Respawn()
    {
        /*Se instancia una nueva plataforma con collider y script*/
        GameObject newP = Instantiate(this.gameObject, positionStart, Quaternion.identity);
        newP.GetComponent<BoxCollider2D>().enabled = true;
        newP.GetComponent<fallingPlatform>().enabled = true;
    }
}
