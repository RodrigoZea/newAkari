using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weight : MonoBehaviour {

    /*Cuerda que sostiene el peso*/
    LineRenderer line;
    /*Distancia que uno la cuerda con la plataforma*/
    DistanceJoint2D distanceJoint;

	// Use this for initialization
	void Start () {
        /*Se obtienen los componentes de cada Objeto*/
        distanceJoint = GetComponent<DistanceJoint2D>();
        line = GetComponent<LineRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        /*Se asignan las posiciones de la cuerda para el peso*/
        line.SetPosition(0, transform.position);
        line.SetPosition(1, distanceJoint.connectedBody.transform.position);

    }
}
