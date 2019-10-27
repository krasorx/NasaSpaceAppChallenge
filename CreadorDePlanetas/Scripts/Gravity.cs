using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    private Transform myTransform;
    public Transform sunTransform;
    public bool isSatelite;
    private float gravityForce = 200;
    public float distance;
    public float velTang;
    public float x1, y1, z1;
    private float xc, yc, zc;
    public float mass;
    private float forceSun;
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
        xc = x1;
        yc = y1;
        zc = z1;

        if (isSatelite)
        {
            gravityForce = 10;
        }

        a = apo / (1 + e);
        b = a * (Mathf.Sqrt(a / (1 - (Mathf.Pow(e, 2)))));

        c = e * a;



        momentoAng = mass * distance * velTang;
        d = (1 + Mathf.Pow(e, 2)) * a;
    }
        // Start is called before the first frame update
        void Start()
        {




        }

        // Update is called once per frame
        void Update()
        {




            //myTransform.position.x = velTang * Mathf.Cos(CartesianToPolar(myTransform.position)) ;

            //transform.position();

        }

        void FixedUpdate()
        {

            if (ang > (2 * Mathf.PI))
                ang = 0;



        //distance = Mathf.Sqrt(Mathf.Pow(myTransform.position.x, 2) + Mathf.Pow(myTransform.position.y, 2));

        //x1 = -c + Mathf.Sqrt(Mathf.Cos(ang)/( (Mathf.Pow(Mathf.Cos(ang),2)/Mathf.Pow(a,2)) +
        //   ( (Mathf.Pow(Mathf.Sin(ang),2))/(Mathf.Pow(b,2)) ) ));



        //y1 = Mathf.Sin(ang)/(Mathf.Sqrt( (Mathf.Pow(Mathf.Cos(ang),2))/Mathf.Pow(a,2) + 
        //                    (Mathf.Pow(Mathf.Sin(ang), 2)) / Mathf.Pow(b, 2))) ;

        //r = (d / (1 + e * Mathf.Cos(ang)));

        //ang += Time.fixedDeltaTime;


        //myTransform.position = new Vector3(x1, y1, 0.0f);

        // codigo gravedad viejo

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

        // .... add forces of other objects. 

        myTransform.position = new Vector3(x1, y1, 0);


    }
    
        
        public Vector3 CartesianToPolar(Vector3 transform)
        {
            Vector3 polar = new Vector3();

            //calc longitude
            polar.y = Mathf.Atan2(transform.x, transform.y);

            //this is easier to write and read than sqrt(pow(x,2), pow(y,2))!
            var xzLen = new Vector2(transform.x, transform.z).magnitude;
            //atan2 does the magic
            polar.x = Mathf.Atan2(-transform.y, xzLen);

            //convert to deg
            polar *= Mathf.Rad2Deg;

            return polar;
        }

        public Vector3 PolarToCartesian(Vector3 transform)
        {

            //an origin vector, representing lat,lon of 0,0. 

            var origin = new Vector3();
            //build a quaternion using euler angles for lat,lon
            var rotation = Quaternion.Euler(transform.x, transform.y, transform.z);
            //transform our reference vector by the rotation. Easy-peasy!
            Vector3 point = new Vector3();
            point = rotation * origin;

            return point;
        }

}

