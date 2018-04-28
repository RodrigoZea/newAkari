using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {
    public float speed = 5f, jumpForce = 200f;
    Rigidbody2D rb;
    SpriteRenderer sr;
    float moveVel;
    private bool isJump = false;
    private bool facingRight = true;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        //moveVel = Input.GetAxis("Horizontal") * speed;
        Move(Input.GetAxis("Horizontal"));

       

        if (Input.GetButtonDown("Jump") && (isJump == false))
        {
            rb.AddForce(Vector2.up * jumpForce);
            isJump = true;
        }
    }

    public void Move(float horizontalInput){
        moveVel = horizontalInput * speed;

        if (moveVel != 0)
        {
            rb.transform.Translate(new Vector3(1, 0, 0) * moveVel * Time.deltaTime);
            facingRight = moveVel > 0;
        }

        sr.flipX = !facingRight;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Tiles"))
        {
            isJump = false;
        }
    }
}
