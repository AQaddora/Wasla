using System;
using System.Collections;
using System.Collections.Generic;
using ArabicSupport;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class QuestionsManager : MonoBehaviour
{
    public static QuestionsManager Instance;
    
    [Header("Instantiaion Requirements")]
    public GameObject unorderdCharPrefab;
    public GameObject solutionCharPrefab;
    public GameObject spacePrefab;
    public Transform unorderdParent;
    public Transform solutionParent;

    [Header("UI")] 
    public GameObject questionWithImage;
    public Image questionImage;
    public Text questionText, questionTextWithImage;
    public Sprite loadingSpr;
    
    [Header("Test Questions")]
    public Question[] testQuestion;

    [HideInInspector] public List<Question> questions;
    private int index = -1;
    private void Awake()
    {
        Instance = this;
        questions = new List<Question>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && index != -1)
        {
            index = -1;
            questions = new List<Question>();
            UIManager.Instance.ShowLessons();
        }
    }
    public void SetNextQuestion()
    {
        if (index >= questions.Count - 1)
        {
            index = -1;
            questions = new List<Question>();
            UIManager.Instance.ShowLessons() ;
            return;
        }
        Question question = questions[++index];
        
        ClearQuestion();
        questionText.text = ArabicFixer.Fix(question.questionText);
        
        if (question.hasImage && !string.IsNullOrEmpty(question.imageUrl))
        {
            questionWithImage.SetActive(true);
            questionTextWithImage.text = ArabicFixer.Fix(question.questionText);
            StartCoroutine(LoadImage(question.imageUrl));
        }
        
        int rnd = Random.Range(2, 5);
        for (int i = 0; i < question.answer.Length; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (j < question.answer[i].Length)
                {
                    SolutionManager.CorrectSolution.Add(question.answer[i][j]);
                    SolutionManager.CurrentSolution.Add(' ');
                    GameObject solObj = Instantiate(solutionCharPrefab);
                    solObj.transform.parent = solutionParent;
                    solObj.transform.localScale = Vector3.one;
                    AddUnorderdCharacter(question.answer[i][j].ToString());
                }
                else
                {
                    SolutionManager.CorrectSolution.Add(' ');
                    SolutionManager.CurrentSolution.Add(' ');
                    GameObject solObj = Instantiate(spacePrefab);
                    solObj.transform.parent = solutionParent;
                    solObj.transform.localScale = Vector3.one;
                }
            }
        }
        
        for (int i = 0; i < rnd; i++)
        {
            char randomChar = (char)( 'ب' + Random.Range(0, 20));
            AddUnorderdCharacter(randomChar.ToString());
        }
        
        for (int i = 0; i < unorderdParent.childCount; i++)
        {
            unorderdParent.GetChild(i).SetSiblingIndex(Random.Range(0, unorderdParent.childCount-1));
        }
    }

    public IEnumerator LoadImage(string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if(www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
        }
        else {
            Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            questionImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
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
        questionWithImage.SetActive(false);
        unorderdParent.GetComponent<GridLayoutGroup>().enabled = true;
        SolutionManager.emptyIndex = 0;
        SolutionManager.CorrectSolution = new List<char>();
        SolutionManager.CurrentSolution = new List<char>();
        questionImage.sprite = loadingSpr;
        
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
