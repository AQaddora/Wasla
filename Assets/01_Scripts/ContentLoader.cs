using ArabicSupport;
using Boo.Lang;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class ContentLoader : MonoBehaviour
{
    public GameObject unitBtnPrefab, lessonBtnPrefab;
    public Transform unitsParent, lessonsParent;
    public Content content;
    public static ContentLoader Instance;
    public Text lessonText, unitText;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        string directoryPath = "F:\\GamerBoxStudios Company\\GitHub\\UnityProjects\\Wasla\\Contents";
        string fileName = "pure content";
        string json = File.ReadAllText(directoryPath + "\\" + fileName + ".data");
        content = new Content();
        content = JsonUtility.FromJson<Content>(json);

        PrintContent();

        for (int i = 0; i < content.units.Count; i++)
        {
            GameObject unit = Instantiate(unitBtnPrefab, unitsParent);
            unit.GetComponentInChildren<Text>().text = ArabicFixer.Fix((i+1).ToString());
            unit.GetComponent<UnitSelecScript>().unit = i;
        }
    }
   
    public void LoadUnitLessons(int unitIndex)
    {
        foreach (Transform transform in lessonsParent)
        {
            Destroy(transform.gameObject);
        }
        unitText.text = ArabicFixer.Fix(content.units[unitIndex].unitName);
        for (int i = 0; i < content.units[unitIndex].lessons.Count; i++)
        {
            GameObject lesson = Instantiate(lessonBtnPrefab, lessonsParent);
            lesson.GetComponentInChildren<Text>().text = ArabicFixer.Fix((i+1).ToString());
            lesson.GetComponent<LessonSelectScript>().lesson =i;
            lesson.GetComponent<LessonSelectScript>().unit = unitIndex;
        }
        UIManager.Instance.ShowLessons();

    }

    public void LoadLessonQuestions(int lessonIndex, int unitIndex)
    {
        lessonText.text = ArabicFixer.Fix(content.units[unitIndex].lessons[lessonIndex].lessonName);
        Debug.Log(QuestionsManager.Instance.questions.Count);
        QuestionsManager.Instance.questions = content.units[unitIndex].lessons[lessonIndex].questions;
        UIManager.Instance.StartGame();
    }

    void PrintContent()
    {
        foreach (Unit unit in content.units)
        {
            string s = "\n";
            foreach (Lesson lesson in unit.lessons)
            {
                s += lesson.lessonName + " -> ";
                foreach (Question question in lesson.questions)
                {
                    s += question.questionText + "\n\t";
                }
            }
            Debug.Log(unit.unitName + " -> " + s);
        }


    }
}
