using System;
using System.Collections.Generic;

[Serializable]
public class Lesson
{
    public List<Question> questions;
    public string lessonName;
    public Lesson(string lessonName)
    {
        this.lessonName = lessonName;
        questions = new List<Question>();
    }
}
