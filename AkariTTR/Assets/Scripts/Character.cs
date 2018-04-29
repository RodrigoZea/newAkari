using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    Rigidbody2D rb2d;
    SpriteRenderer sr;
    public Camera cam;
    private float speed = 5f;
    private float jumpForce = 225f;
    private bool facingRight = true;
    private bool jump = false;
    Animator anim;
    //AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        cam.transform.position = new Vector3(rb2d.transform.position.x, cam.transform.position.y, cam.transform.position.z);
        anim = GetComponent<Animator>();
        //audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(move));
        if (move != 0)
        {
            rb2d.transform.Translate(new Vector3(1, 0, 0) * move * speed * Time.deltaTime);
            cam.transform.position = new Vector3(rb2d.transform.position.x, cam.transform.position.y, cam.transform.position.z);
            facingRight = move > 0;
        }

        sr.flipX = !facingRight;

        if (jump == false)
        {
            if (Input.GetButtonDown("Jump"))
            {
                anim.SetBool("Jump", true);
                rb2d.AddForce(Vector2.up * jumpForce);
                //audioSource.Play();
                jump = true;              
            }
        }

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        anim.SetBool("Jump", false);
        jump = false;
        
    }
}
