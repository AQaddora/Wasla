using System;
using UnityEngine;
using ArabicSupport;
using UnityEngine.UI;

public class ArabicInputField : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<InputField>().onEndEdit.AddListener(OnEndSelect_SetArabicText);
    }

    public void OnEndSelect_SetArabicText(string input)
    {
        GetComponent<InputField>().text = ArabicFixer.Fix(GetComponent<InputField>().text);
    }
}
