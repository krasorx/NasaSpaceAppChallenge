using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    private Transform myTransform;
    public float distance;
    public float velTang;
    public float x1, y1, z1;
    public float mass;
    private float momentoAng;
    private float velAng = 10.0f;
    private float energy;
    private float d;
    public float e; // exentricidad
    public float a; // semieje mayor
    private float b; // semieje menor
    private float c; // distancia al centro del eje
    public float apo;  //apoastro
    private float r; // radio
    public float ang;  // max = 2pi

    private void Awake()
    {
        myTransform = transform;

        a = apo / (1 + e);
        b = a * (Mathf.Sqrt(a / (1 - (Mathf.Pow(e, 2)))));

        c = e * a;



        momentoAng = mass * distance * velTang;
        d = (1 + Mathf.Pow(e, 2)) * a;
    }

        void FixedUpdate()
        {

            if (ang > (2 * Mathf.PI))
                ang = 0;

            velTang = momentoAng / (mass * Mathf.Pow(r,2));

            distance = Mathf.Sqrt(Mathf.Pow(myTransform.position.x, 2) + Mathf.Pow(myTransform.position.y, 2));
            
            r = (d / (1 + e * Mathf.Sin(ang)));
            velTang = momentoAng / (mass * Mathf.Pow(r, 2));
            ang = ang + (velTang*(Time.fixedDeltaTime));


            x1 = (r * Mathf.Sin(ang));
            y1 = (r * Mathf.Cos(ang));


            // Do the Force calculation (refer universal gravitation for more info)
            // Use numbers to adjust force, distance will be changing over time!
            forceSun = ((gravityForce * mass) / Mathf.Pow(r, 2));

            // Find the Normal direction
            Vector3 normalDirectionSun = (myTransform.position - sunTransform.position).normalized;

            // calculate the force on the object from the planet
            Vector3 normalForceSun = myTransform.position.normalized * forceSun;    //normalized position

            // Calculate for the other systems on your solar system similarly
            // Apply all these forces on current planet's rigidbody

            // Apply the force on the rigid body of the surrounding object/s
            GetComponent<Rigidbody>().AddForce(normalForceSun);

        myTransform.position = new Vector3(x1, y1, 0);


    }

}
