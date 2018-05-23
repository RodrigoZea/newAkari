using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingSteel : MonoBehaviour {

    //public static GameController instance;

    public Rigidbody2D steel;
    public GameObject platform;
    private bool fall = false;
    public static bool rest = false;

    private Vector2 startingPosition;
    private float speed = -3f;

    // Use this for initialization
    void Start () {
        steel = GetComponent<Rigidbody2D>();
        startingPosition = new Vector2(steel.position.x, steel.position.y);
    }
	
	// Update is called once per frame
	void Update () {
		if(fall == true)
        {
            steel.transform.Translate(new Vector3(0, 1, 0) * speed * Time.deltaTime);

            if (steel.position.y <= -7.89)
            {
                fall = false;
                Destroy(this.gameObject);
                Respawn();
            }
        }
	}

    public void OnCollisionEnter2D(Collision2D collision)
    {
        fall = true;
    }

    public void Respawn()
    {
        GameObject newPlat = Instantiate(platform, startingPosition, Quaternion.identity);
        steel.position = startingPosition;
    }
}
