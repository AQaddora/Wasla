using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SolutionManager : MonoBehaviour
{
    public static SolutionManager Instance;
    public static int emptyIndex;

    public static List<char> CorrectSolution, CurrentSolution;

    [Header("Sound Effects")] public AudioClip addEffect;
    public AudioClip dropEffect;
    
    private void Awake()
    {
        Instance = this;
    }

    public void Drop(int index, string droppedCharacter)
    {
        AudioManager.Instance.PlayEffect(dropEffect);
        if(string.IsNullOrEmpty(droppedCharacter))
            return;
        transform.GetChild(index).GetComponentInChildren<Text>().text = "";
        CurrentSolution[index] = ' ';
        QuestionsManager.Instance.unorderdParent.GetComponent<GridLayoutGroup>().enabled = true;
        QuestionsManager.Instance.AddUnorderdCharacter(droppedCharacter);
        SetEmptyIndex();
    }

    public bool Add(string addedCharacter)
    {
        QuestionsManager.Instance.unorderdParent.GetComponent<GridLayoutGroup>().enabled = false;
        AudioManager.Instance.PlayEffect(addEffect);
        if (emptyIndex >= CorrectSolution.Count)
            return false;
        Text boxText = transform.GetChild(emptyIndex).GetComponentInChildren<Text>();
        Drop(emptyIndex, boxText.text);
        boxText.text = addedCharacter;
        CurrentSolution[emptyIndex] = addedCharacter[0];
        if(!SetEmptyIndex())
        {
            if (CheckSolution())
            {
                AudioManager.Instance.PlayTrueEffect();
                QuestionsManager.Instance.SetNextQuestion();
            }
            else
            {
                AudioManager.Instance.PlayWrongEffect();
            }
        }
        return true;
    }

    public bool SetEmptyIndex()
    {
        for (int i = 0; i < CorrectSolution.Count; i++)
        {
            if(CorrectSolution[i] == ' ')
                continue;
            if (string.IsNullOrEmpty(transform.GetChild(i).GetComponentInChildren<Text>().text))
            {
                emptyIndex = i;
                return true;
            }
        }
        return false;
    }

    public bool CheckSolution()
    {
        LogList(CorrectSolution);
        LogList(CurrentSolution);
        return Enumerable.SequenceEqual(CurrentSolution, CorrectSolution);
    }

    public void LogList(List<char> list)
    {
        string output = "";
        for (int i = 0; i < list.Count; i++)
        {
            output += list[i];
        }
        Debug.Log(output);
    }
}
