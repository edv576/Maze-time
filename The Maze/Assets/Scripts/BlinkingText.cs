using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BlinkingText : MonoBehaviour
{
    //public Text text;
    TextMeshProUGUI textContinue;



    private void Start()

    {

        //text = GetComponent<Text>();
        textContinue = GetComponent<TextMeshProUGUI>();

        StartBlinking();

    }



    IEnumerator Blink()

    {

        while (true)

        {
            
            switch (textContinue.color.a.ToString())

            {

                case "0":

                    textContinue.color = new Color(textContinue.color.r, textContinue.color.g, textContinue.color.b, 1);

                    yield return new WaitForSeconds(0.5f);

                    break;

                case "1":

                    textContinue.color = new Color(textContinue.color.r, textContinue.color.g, textContinue.color.b, 0);

                    yield return new WaitForSeconds(0.5f);

                    break;

            }

        }

    }



    void StartBlinking()

    {

        StopCoroutine("Blink");

        StartCoroutine("Blink");

    }



    void StopBlinking()

    {

        StopCoroutine("Blink");

    }

}

