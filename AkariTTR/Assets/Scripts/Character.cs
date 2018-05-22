using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    private float timer;

    Rigidbody2D rb2d;
    SpriteRenderer sr;
    public Camera cam;
    private float jumpForce = 225f;


    private bool facingRight = true;
    private bool jump = false;
    private bool doubleJump = false;
    private bool onGround = false;
    private bool onWalls = false;
    private float dashTimer;

    public AudioClip hurtClip;
    public AudioClip jumpClip;

    public GameObject sword;
    public LayerMask layerMask;
    RaycastHit2D raycast;

    public GameObject chest;
    private bool allowMove;

    private float direction;
    private bool moving;

    private Vector2 startingPosition;
    Animator anim;
    AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        cam.transform.position = new Vector3(rb2d.transform.position.x, cam.transform.position.y, cam.transform.position.z);
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        dashTimer = 3;
        // Para respawn
        startingPosition = new Vector2(rb2d.position.x, rb2d.position.y);
        GameController.instance.score.text = "SCORE: " + GameController.instance.points.ToString("#.#");

        allowMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        GameController.instance.score.text = "Score: " + GameController.instance.points.ToString("#");


        //GameController.instance.score.text = "SCORE: " + GameController.instance.points.ToString("#.#");
        cam.transform.position = new Vector3(rb2d.transform.position.x, rb2d.transform.position.y, cam.transform.position.z);

        if (moving || onGround)
        {
            //speed = 
            anim.SetFloat("Speed", Mathf.Abs(direction));
            handleMovement(direction);
            Flip(direction);
        }

        dashTimer += Time.deltaTime;
        timer = timer + Time.deltaTime;

        //checkOnWall();

        if (timer > 1)
        {
            anim.SetBool("Attack", false);
            sword.SetActive(false);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Tiles"))
        {
            anim.SetBool("Jump", false);
            jump = false;
            doubleJump = false;
            onGround = true;
            onWalls = false;
            allowMove = true;
        }else if (collision.gameObject.tag.Equals("Ninja"))
        {
            if (sword.activeSelf == false)
            {
                audioSource.clip = hurtClip;
                audioSource.Play();
                Respawn();
                removePoints();
            }
        }
        else if (collision.gameObject.tag.Equals("Walls"))
        {
            onGround = false;
            onWalls = true;
            allowMove = false;
        }else if (collision.gameObject.tag.Equals("Deathzone"))
        {
            audioSource.clip = hurtClip;
            audioSource.Play();
            Respawn();
            removePoints();
            
            //Destroy(collision.gameObject);
        }

    }

    void removePoints() {
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
        if (collision.gameObject.tag.Equals("Deathzone"))
        {
            audioSource.clip = hurtClip;
            audioSource.Play();
            Respawn();
            removePoints();
            //Destroy(collision.gameObject);
        }
    }

    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;
            sr.flipX = !facingRight;
        }
    }

    private void handleMovement(float horizontal)
    {
        rb2d.velocity = new Vector2(horizontal, rb2d.velocity.y);
    }

    public void Move(float direction)
    {
        if (allowMove == true)
        {
            this.direction = direction;
            this.moving = true;
        }
    }

    public void StopMove()
    {
        this.direction = 0;
        moving = false;
    }

    public void Jump()
    {
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
        anim.SetBool("Jump", true);
        rb2d.AddForce(Vector2.up * jumpForce);
        audioSource.Play();
    }

    public void Respawn()
    {
        rb2d.position = (startingPosition);
        fallingSteel.rest = true;
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