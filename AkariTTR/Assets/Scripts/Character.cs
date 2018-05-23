using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    /*Se declara el cuerpo rigido, sprite renderer y camara*/
    Rigidbody2D rb2d;
    SpriteRenderer sr;
    public Camera cam;

    /*Se declara la fuerza de salto*/
    private float jumpForce = 225f;

    /*Se declaran las variables control para el movimiento, salto y ataque del personaje*/
    private bool facingRight = true;
    private bool jump = false;
    private bool doubleJump = false;
    private bool onGround = false;
    private bool onWalls = false;
    private bool allowMove;
    private float direction;
    private bool moving;
    private float dashTimer;

    /*Se declaran los audioclip para el sonido*/
    public AudioClip hurtClip;
    public AudioClip jumpClip;

    /*Se declara la espada y pecho*/
    public GameObject sword;
    public GameObject chest;

    /*Se declara el layerMask y raycast*/
    public LayerMask layerMask;
    RaycastHit2D raycast;

    /*Se declara vector para posicion inicial*/
    private Vector2 startingPosition;

    /*Se declara animator y audiosource*/
    Animator anim;
    AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        /*Se obtienen los componentes de los objetos*/
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        /*Se desplaza la camara hacia el personaje*/
        cam.transform.position = new Vector3(rb2d.transform.position.x, cam.transform.position.y, cam.transform.position.z);

        /*Tiempo de ataque*/
        dashTimer = 3;

        // Para respawn
        startingPosition = new Vector2(rb2d.position.x, rb2d.position.y);
        GameController.instance.score.text = "SCORE: " + GameController.instance.points.ToString("#.#");

        /*Se inicia control de movimiento*/
        allowMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        /*Se muestra en pantalla los puntos*/
        GameController.instance.score.text = "Score: " + GameController.instance.points.ToString("#");
        
        /*Se muevev la camara donde el jugador se dirige*/
        cam.transform.position = new Vector3(rb2d.transform.position.x, rb2d.transform.position.y, cam.transform.position.z);

        /*Se posible moverse si esta en el suelo*/
        if (moving || onGround)
        {
            /*Activa animacion*/
            anim.SetFloat("Speed", Mathf.Abs(direction));
            /*Da direccion al personaje*/
            handleMovement(direction);
            Flip(direction);
        }

        /*Se incrementa el tiempo y el tiempo de ataque*/
        dashTimer += Time.deltaTime;
        timer = timer + Time.deltaTime;
        
        if (timer > 1)
        {
            /*Si el tiempo es mayor a 1 es posible atacar*/
            anim.SetBool("Attack", false);
            sword.SetActive(false);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        /*Al colisionar con el suelo*/
        if (collision.gameObject.tag.Equals("Tiles"))
        {
            /*Activa animacion de salto*/
            anim.SetBool("Jump", false);

            /*Desactiva variables control*/
            jump = false;
            doubleJump = false;
            onWalls = false;
            /*Activa movimiento y que esta en suelo*/
            onGround = true;
            allowMove = true;
        }
        /*Al colisionar con enemigo*/
        else if (collision.gameObject.tag.Equals("Ninja"))
        {
            /*Solo si la espada no esta activada...*/
            if (sword.activeSelf == false)
            {
                /*Muere el personaje y aparece al inicio restando puntos*/
                audioSource.clip = hurtClip;
                audioSource.Play();
                Respawn();
                removePoints();
            }
        }
        /*Al colisionar con paredes*/
        else if (collision.gameObject.tag.Equals("Walls"))
        {
            /*Activa que esta en paredes y desactiva movimiento y que esta en suelo*/
            onGround = false;
            onWalls = true;
            allowMove = false;
        }
        /*Al colisionar con zona de muerte*/
        else if (collision.gameObject.tag.Equals("Deathzone"))
        {
            /*Muere el personaje y reaparece al inicio y puntos menos*/
            audioSource.clip = hurtClip;
            audioSource.Play();
            Respawn();
            removePoints();
        }

    }

    void removePoints() {

        /*Se restan puntos al personaje*/
        if (GameController.instance.points <= 10f)
        {
            //GameController.instance.points = 0;
        }
        else
        {
            GameController.instance.points = GameController.instance.points - 10f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*Al colisionar con zona de muerte*/
        if (collision.gameObject.tag.Equals("Deathzone"))
        {
            /*Muere el personaje y reaparece al inicio y puntos menos*/
            audioSource.clip = hurtClip;
            audioSource.Play();
            Respawn();
            removePoints();
        }
    }

    private void Flip(float horizontal)
    {
        /*Realiza un flip dependiendo del movimiento*/
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;
            sr.flipX = !facingRight;
        }
    }

    private void handleMovement(float horizontal)
    {
        /*Mantiene el movimiento del personaje*/
        rb2d.velocity = new Vector2(horizontal, rb2d.velocity.y);
    }

    public void Move(float direction)
    {
        /*Se mueve solo si la variable de movimiento esta activada*/
        if (allowMove == true)
        {
            this.direction = direction;
            this.moving = true;
        }
    }

    public void StopMove()
    {
        /*Detiene el movimiento*/
        this.direction = 0;
        moving = false;
    }

    public void Jump()
    {
        /*Realiza el salto y doble salto*/
        if (jump == false)
        {
            doJump();
            jump = true;
            doubleJump = true;
        }
        else if (doubleJump == true)
        {
            doJump();
            doubleJump = false;
        }
    }

    public void doJump()
    {
        /*Ejecuta animacion de salto y realiza salto*/
        anim.SetBool("Jump", true);
        rb2d.AddForce(Vector2.up * jumpForce);
        audioSource.Play();
    }

    public void Respawn()
    {
        /*Regresa a la posicion inicial*/
        rb2d.position = (startingPosition);

        //Ver como solucionar esto, cuando se pone esta linea ya no suena hurtClip
        audioSource.clip = jumpClip;
    }

    /* Esto ahora solo es ataque */
    public void Dash(float dashForce)
    {
        timer = 0;
        
        if (dashTimer > 1)
        {
            sword.SetActive(true);
            anim.SetBool("Attack", true);
            dashTimer = 0;
        }      
        
    }
}