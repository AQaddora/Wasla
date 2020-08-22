using System;
using System.IO;
using System.Security.AccessControl;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ContentInserter : MonoBehaviour
{
    public Content content;

    string directoryPath = "C:\\Users\\HP\\Documents\\GitHub\\UnityProjects\\Wasla\\Contents";
    [Header("Faders")] 
    [SerializeField] Fader addUnits;
    [SerializeField] Fader addLessons, addQuestions;

    [Header("Unit Stuff")] public InputField unitNameInput;

    [Header("Lesson Stuff")] public InputField lessonNameInput;

    [Header("Question Stuff")] public InputField questionText;
    public InputField answerInput;
    public InputField imageUrlInput;
    
    Unit currentUnit;
    Lesson currentLesson;
    
    private void Awake()
    {
        content = new Content();
    }

    public void AddUnit_Btn()
    {
        currentUnit = new Unit(unitNameInput.text);
        content.units.Add(currentUnit);
        addUnits.Hide();
        addLessons.Show();
        unitNameInput.text = "";
    }

    public void AddLesson_Btn()
    {
        currentLesson = new Lesson(lessonNameInput.text);
        currentUnit.lessons.Add(currentLesson);
        addLessons.Hide();
        addQuestions.Show();
        lessonNameInput.text = "";
    }

    public void AddQuestion()
    {
        string question = questionText.text;
        string[] answer = answerInput.text.Split(' ');
        bool hasImage = string.IsNullOrEmpty(imageUrlInput.text);
        string imageUrl = imageUrlInput.text;
        Question q = new Question(question, answer,imageUrl, hasImage);
        currentLesson.questions.Add(q);
        ClearQuestion_Btn();
    }

    public void ClearQuestion_Btn()
    {
        questionText.text = "";
        answerInput.text = "";
        imageUrlInput.text = "";
    }

    public void AddAnotherLesson()
    {
        addLessons.Show();
        addQuestions.Hide();
        addUnits.Hide();
    }

    public void AddAnotherUnit()
    {
        addLessons.Hide();
        addQuestions.Hide();
        addUnits.Show();
    }
    public void SaveData()
    {
        string jsonOutput = JsonUtility.ToJson(content);
        Debug.Log(jsonOutput);
        if (!PlayerPrefs.HasKey("LAST_SAVED"))
        {
            PlayerPrefs.SetInt("LAST_SAVED",1);
        }

        int lastSaved = PlayerPrefs.GetInt("LAST_SAVED");
        string fileName = lastSaved.ToString();
        lastSaved++;
        PlayerPrefs.SetInt("LAST_SAVED", lastSaved);
        string path = directoryPath + "\\" + fileName + ".data";
        
        File.Create(path);
        //SET ACCESS
        //File.WriteAllText(path,jsonOutput);
    }
}
