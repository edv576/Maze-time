using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaonController : MonoBehaviour
{

    Animator anim;
    public float rotationSpeed;
    public Camera playerCamera;
    public Camera victoryCamera;
    public AudioClip winClip;
    public AudioSource checkAudioSource;
    public GameObject victoryText;

    bool victory = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        transform.position = new Vector3(0.0f, transform.position.y, 0.0f);
        playerCamera = GetComponentInChildren<Camera>();
        victoryCamera = GameObject.Find("Victory camera").GetComponent<Camera>();
        victoryCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("vertical", Input.GetAxis("Vertical"));
        anim.SetFloat("horizontal", Input.GetAxis("Horizontal"));

        if (!victory)
        {
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Victory button" && !victory)
        {
            victory = true;
            anim.SetBool("win", true);
            other.gameObject.GetComponent<Collider>().enabled = false;
            victoryCamera = other.transform.parent.gameObject.GetComponentInChildren<Camera>();
            playerCamera.enabled = false;
            victoryCamera.enabled = true;
            anim.SetFloat("turn", 0);
            anim.SetBool("jump", false);
            checkAudioSource.clip = winClip;
            checkAudioSource.Play();
            victoryText.SetActive(true);
        }
    }
}
