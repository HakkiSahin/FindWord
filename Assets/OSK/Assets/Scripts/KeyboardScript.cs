using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardScript : MonoBehaviour
{

    private void Start()
    {
        if (Screen.width > 1400)
        {
            gameObject.GetComponent<RectTransform>().localScale = new Vector3(1.4f, 1.4f, 1);
        }
        else if (Screen.width < 800)
        {
            gameObject.GetComponent<RectTransform>().localScale = new Vector3(0.7f, .8f, 1);

        }
    }



}
