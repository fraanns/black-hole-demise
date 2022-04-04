using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageText : MonoBehaviour
{
    [SerializeField]
    TMPro.TMP_Text messageText;

    private void Start()
    {
        messageText.text = "";
    }

    public void Message(string message)
    {
        messageText.text = message;
        StartCoroutine("DisplayText");
    }

    IEnumerator DisplayText()
    {
        yield return new WaitForSeconds(4);
        messageText.text = "";
    }
}
