using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueWriter
{
    public static IEnumerator SimpleWritingText(string inputText, TMP_Text textHolder, float symbolTime)
    {
        if (inputText == null)
        {
            yield break;
        }
        textHolder.text = string.Empty;
        for (int i = 0; i < inputText.Length; i++)
        {
            textHolder.text += inputText[i];
            yield return new WaitForSeconds(symbolTime);
        }
    }
    public static IEnumerator SimpleWritingText(string inputText, TMP_Text textHolder, float symbolTime, Action OnComplete)
    {
        if (inputText == null)
        {
            yield break;
        }
        textHolder.text = string.Empty;
        for (int i = 0; i < inputText.Length; i++)
        {
            textHolder.text += inputText[i];
            yield return new WaitForSeconds(symbolTime);
        }

        OnComplete.Invoke();
    }
}
