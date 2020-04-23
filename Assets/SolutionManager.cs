using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SolutionManager : MonoBehaviour
{
    public static SolutionManager Instance;
    public static int emptyIndex, length;

    private void Awake()
    {
        Instance = this;
    }

    public void Drop(int index, string droppedCharacter)
    {
        emptyIndex = index;
        transform.GetChild(emptyIndex).GetComponentInChildren<Text>().text = "";
        QuestionsManager.Instance.AddUnorderdCharacter(droppedCharacter);
    }

    public void Add(string addedCharacter)
    {
        if (emptyIndex >= length)
            return;
        transform.GetChild(emptyIndex).GetComponentInChildren<Text>().text = addedCharacter;

        if (emptyIndex + 1 < length)
            emptyIndex++;
        else
        {
            return;
        }

        while (!string.IsNullOrEmpty(transform.GetChild(emptyIndex).GetComponentInChildren<Text>().text))
        {
            if(emptyIndex < length)
                emptyIndex++;
            else
            {
                return;
            } 
        }
    }
}
