using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class ContenCompactor : MonoBehaviour
{
    public int contentsFilesCount;

    Content mainContent;
    private List<Content> _contents;
    private HashSet<string> unitNames, lessonNames, questionTexts;
    private string directoryPath;
    void Start()
    {
        
    }

    void CompactUnits()
    {
        for (int i = 0; i < mainContent.units.Count; i++)
        {
            Unit unit = mainContent.units[i];
            Unit oriUnit = FindUnitByName(unit.unitName);
            if (unitNames.Contains(unit.unitName))
            {
                for (int j = 0; j < unit.lessons.Count; j++)
                {
                    oriUnit.lessons.Add(unit.lessons[j]);
                }

                mainContent.units.Remove(unit);
            }
            else
            {
                unitNames.Add(unit.unitName);
            }
        }
    }

    void CompactLessons()
    {
        foreach (Unit unit in mainContent.units)
        {
            for (int i = 0; i < unit.lessons.Count; i++)
            {
                Lesson lesson = unit.lessons[i];
                Lesson oriLesson = FindLessonByName(lesson.lessonName, unit.lessons);
                if (lessonNames.Contains(lesson.lessonName))
                {
                    Debug.Log(lesson.questions.Count);
                    oriLesson.questions.AddRange(lesson.questions);
                    /*for (int j = 0; j < lesson.questions.Count; j++)
                    {
                        oriLesson.questions.Add(lesson.questions[j]);
                    }*/
                    unit.lessons.Remove(lesson);
                }
                else
                {
                    lessonNames.Add(lesson.lessonName);
                }
            }
            lessonNames = new HashSet<string>();
        }
    }

    void PrintContent()
    {
        foreach (Unit unit in mainContent.units)
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

    public Unit FindUnitByName(string unitName)
    {
        for (int i = 0; i < mainContent.units.Count; i++)
        {
            if (mainContent.units[i].unitName.Equals(unitName))
            {
                return mainContent.units[i];
            }
        }

        return null;
    }
    
    public Lesson FindLessonByName(string lessonName, List<Lesson> lessons)
    {
        for (int i = 0; i < lessons.Count; i++)
        {
            if (lessons[i].lessonName.Equals(lessonName))
            {
                return lessons[i];
            }
        }

        return null;
    }
    string path, jsonOutput;
    public void CompactContent()
    {
        unitNames = new HashSet<string>();
        lessonNames = new HashSet<string>();
        questionTexts = new HashSet<string>();
        mainContent = new Content();
        directoryPath = "F:\\GamerBoxStudios Company\\GitHub\\UnityProjects\\Wasla\\Contents";
        _contents = new List<Content>();
        contentsFilesCount = PlayerPrefs.GetInt("LAST_SAVED", 1) - 1;
        for (int i = 0; i < contentsFilesCount; i++)
        {
            string fileName = (i + 1).ToString();
            string json = File.ReadAllText(directoryPath + "\\" + fileName + ".data");
            Content temp = new Content();
            temp = JsonUtility.FromJson<Content>(json);
            _contents.Add(temp);
        }

        //Transfer Units to the main Content Object.
        for (int i = 0; i < _contents.Count; i++)
        {
            foreach (Unit u in _contents[i].units)
            {
                mainContent.units.Add(u);
            }
        }

        //Comnpact Units
        CompactUnits();

        //Compact Lessons
        CompactLessons();

        FindObjectOfType<ContentInserter>().content = mainContent;

        PrintContent();

        SaveCompactedData();
    }

    public void SaveCompactedData()
    {
        jsonOutput = JsonUtility.ToJson(mainContent);
        Debug.Log(jsonOutput);

        string fileName = "pure content";
        path = directoryPath + "\\" + fileName + ".data";
        File.Create(path);
        Invoke(nameof(writePureContentText), 3f);
    }

    void writePureContentText()
    {
        File.WriteAllText(path, jsonOutput);
    }
}
