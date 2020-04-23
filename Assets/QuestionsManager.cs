using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class QuestionsManager : MonoBehaviour
{
    public static QuestionsManager Instance;
    
    [Header("Instantiaion Requirements")]
    public GameObject unorderdCharPrefab;
    public GameObject solutionCharPrefab;
    public Transform unorderdParent;
    public Transform solutionParent;

    public Text questionText;

    public Question testQuestion;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetQuestion(testQuestion);
    }

    public void SetQuestion(Question question)
    {
        ClearQuestion();
        questionText.text = question.questionText;
        if (question.hasImage)
        {
            
        }

        if (question.answer.Length > 1 ||question.answer[0].Length > 7)
        {
            solutionParent.GetComponent<GridLayoutGroup>().constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            solutionParent.GetComponent<GridLayoutGroup>().constraintCount = 7;
        }
        else
        {
            solutionParent.GetComponent<GridLayoutGroup>().constraint = GridLayoutGroup.Constraint.FixedRowCount;
            solutionParent.GetComponent<GridLayoutGroup>().constraintCount = 1;
        }

        
        int rnd = Random.Range(2, 5);
        for (int i = 0; i < question.answer.Length; i++)
        {
            for (int j = 0; j < question.answer[i].Length; j++)
            {
                SolutionManager.length++;
                GameObject solObj = Instantiate(solutionCharPrefab);
                solObj.transform.parent = solutionParent;
                solObj.transform.localScale = Vector3.one;
                GameObject unordObj = Instantiate(unorderdCharPrefab);
                unordObj.transform.parent = unorderdParent;
                unordObj.transform.localScale = Vector3.one;
                unordObj.GetComponentInChildren<Text>().text  = question.answer[i][j].ToString();
            }
        }
        
        for (int i = 0; i < unorderdParent.childCount; i++)
        {
            unorderdParent.GetChild(i).SetSiblingIndex(Random.Range(0, unorderdParent.childCount-1));
        }
    }


    public void AddUnorderdCharacter(string character)
    {
        GameObject unordObj = Instantiate(unorderdCharPrefab);
        unordObj.transform.parent = unorderdParent;
        unordObj.transform.localScale = Vector3.one;
        unordObj.GetComponentInChildren<Text>().text  = character;
    }
    void ClearQuestion()
    {
        questionText.text = "";
        SolutionManager.length = 0;
        for (int i = 0; i < solutionParent.childCount; i++)
        {
            Destroy(solutionParent.GetChild(i).gameObject);
        }
        for (int i = 0; i < unorderdParent.childCount; i++)
        {
            Destroy(unorderdParent.GetChild(i).gameObject);
        }
    }
}
