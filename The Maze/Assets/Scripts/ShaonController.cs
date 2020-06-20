using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaonController : MonoBehaviour
{

    Animator anim;
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

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            anim.SetFloat("turn", 1.0f);
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            anim.SetFloat("turn", 0);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            anim.SetFloat("turn", -1.0f);
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            anim.SetFloat("turn", 0);
        }
    }
}
