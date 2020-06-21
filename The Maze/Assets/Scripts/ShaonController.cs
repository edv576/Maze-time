using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaonController : MonoBehaviour
{

    Animator anim;
    public float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("vertical", Input.GetAxis("Vertical"));
        anim.SetFloat("horizontal", Input.GetAxis("Horizontal"));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("jump", true);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            anim.SetBool("jump", false);
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            anim.SetFloat("turn", 1.0f);
            transform.eulerAngles += new Vector3(0, rotationSpeed * Time.deltaTime, 0);
        }

        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            anim.SetFloat("turn", 0);
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            anim.SetFloat("turn", -1.0f);
            transform.eulerAngles -= new Vector3(0, rotationSpeed * Time.deltaTime, 0);
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
        {
            anim.SetFloat("turn", 0);
        }
    }
}
