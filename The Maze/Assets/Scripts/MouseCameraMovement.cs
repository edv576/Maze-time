using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseCameraMovement : MonoBehaviour
{
    public float speedH = 2.0f;
    public float speedV = 2.0f;

    public Vector3 forward;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    public Animator anim;
    public bool canRotateHead = false;

    public float vertical;

    Vector3 initialOrientation;



    // Use this for initialization
    void Start()
    {
        initialOrientation = transform.eulerAngles;
    }

    

    // Update is called once per frame
    void Update()
    {
        //vertical = anim.GetFloat("vertical");
        //if (anim.GetFloat("vertical") > -0.1 && anim.GetFloat("vertical") < 0.1 && anim.GetFloat("horizontal") > -0.1 && anim.GetFloat("horizontal") < 0.1)
        //{
        //    canRotateHead = true;
        //    //transform.eulerAngles = new Vector3(0.0f, transform.parent.eulerAngles.y, 0.0f);
        //}
        //else
        //{
        //    canRotateHead = false;
        //    //transform.eulerAngles = new Vector3(0.0f, transform.parent.eulerAngles.y, 0.0f);
        //}

        if (canRotateHead)
        {
            //yaw += speedH * Input.GetAxis("Mouse X");
            pitch -= speedV * Input.GetAxis("Mouse Y");

            transform.eulerAngles = new Vector3(pitch, transform.eulerAngles.y, 0.0f);

            forward = transform.forward;
        }

        

    }
}
