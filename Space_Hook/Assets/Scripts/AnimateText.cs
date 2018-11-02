using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateText : MonoBehaviour
{
    private string str; 
    public Text display;
    void Start()
    {
        StartCoroutine(TextAnimate(display.text)) ;
    }

    IEnumerator TextAnimate(string strComplete)
    {
        int i = 0;
        string str = "";
        while (i < strComplete.Length)
        {
            str += strComplete[i++];
            yield return new WaitForSeconds(0.5f);
        }
    }
}
