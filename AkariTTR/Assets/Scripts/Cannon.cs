using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {
    public GameObject bullet;
    private float timer;
    public float firePower;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer >= 3)
        {
            shootFireball();
            timer = 0;
        }
	}

    void shootFireball()
    {
        GameObject newBullet = Instantiate(bullet, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
        newBullet.transform.Rotate(Vector3.left * 90);

        Rigidbody2D newBulletRB = newBullet.GetComponent<Rigidbody2D>();
        newBulletRB.AddForce(Vector2.down * firePower, ForceMode2D.Impulse);

        //Luego destruir...
        Destroy(newBullet, 4.0f);
    }
}
