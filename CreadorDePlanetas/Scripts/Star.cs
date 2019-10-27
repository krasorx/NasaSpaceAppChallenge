using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public float radio;
    public float masa;

    private float rotX;
    private float rotY;

    private float ang = 0;

    private Transform myTransform;



    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        


    }

    private void FixedUpdate()
    {

       // if (ang > 2 * Mathf.PI)
       //     ang = 0;

        //myTransform.Rotate(myTransform.rotation.eulerAngles,ang);

        //ang += Time.fixedDeltaTime;

    }

}
