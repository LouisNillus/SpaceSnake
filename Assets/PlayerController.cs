using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public float xSpeed;
    public float deltaX;

    public float zSpeed = 6f;

    Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {


        //Keyboard Debug
        if (Input.GetKey(KeyCode.RightArrow)) this.transform.position += Vector3.right * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftArrow)) this.transform.position += Vector3.left * Time.deltaTime;

        Movement();


        /*if(Input.touchCount > 0)
        {
            delta = Input.GetTouch(0).deltaPosition.x;
            this.transform.Translate(Vector3.right * delta * Time.deltaTime * xSpeed);
        }*/


    }

    private void FixedUpdate()
    {
        rb.velocity = Vector3.right * Time.deltaTime * deltaX * xSpeed;
    }

    public void Movement()
    {
        //Z Axis
        this.transform.position += Vector3.forward * Time.deltaTime * zSpeed;

        //X Axis
        if (Input.touchCount > 0)
        {
            deltaX = Input.GetTouch(0).deltaPosition.x;
        }
        else
        {
            deltaX = 0f;
        }
    }
}
