using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingSteel : MonoBehaviour {

    //public static GameController instance;

    public GameObject steelO;
    public Rigidbody2D steel;
    private bool fall = false;
    public static bool rest = false;

    private Vector2 startingPosition;
    private float speed = -5.4f;

    private float t = 0f;

    private void Awake()
    {
        steel = GetComponent<Rigidbody2D>();
        startingPosition = new Vector2(steel.position.x, steel.position.y);
    }
    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        t += Time.deltaTime;

		if(fall == true)
        {
            steel.transform.Translate(new Vector3(0, 1, 0) * speed * Time.deltaTime);

            if (steel.position.y <= -7.89)
            {
                Destroy(steelO);
                fall = false;
            }
        }

        if(rest == true)
        {
            restart();
        }
	}

    public void OnCollisionEnter2D(Collision2D collision)
    {
        fall = true;
    }

    public void restart()
    {
        Instantiate(steelO, startingPosition, Quaternion.identity);
        rest = false;
    }
}
