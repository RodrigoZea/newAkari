using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {
    /*Se declara la bola de fuego*/
    public GameObject bullet;

    /*Se declara el tiempo*/
    private float timer;

    /*Se declara la fuerza de tiro de la bola de fuego*/
    public float firePower;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        /*Se aumenta el tiempo*/
        timer += Time.deltaTime;
        if (timer >= 3)
        {
            /*Lanza la bola de fuego luego de 3 segundos*/
            shootFireball();
            timer = 0;
        }
	}

    void shootFireball()
    {
        /*Instancia otra bola de fuego como GameObject*/
        GameObject newBullet = Instantiate(bullet, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
        newBullet.transform.Rotate(Vector3.left * 90);

        /*Se obtienen los componentes del cuerpo rigido*/
        Rigidbody2D newBulletRB = newBullet.GetComponent<Rigidbody2D>();
        newBulletRB.AddForce(Vector2.down * firePower, ForceMode2D.Impulse);

        //Luego destruir...
        Destroy(newBullet, 4.0f);
    }
}
