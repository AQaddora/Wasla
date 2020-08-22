using System;

[Serializable]
public class Question
{
    public string questionText;
    public string[] answer;
    public string imageUrl;
    public bool hasImage;
    
    public Question(string questionText, string[] answer, string imageUrl, bool hasImage)
    {
        this.questionText = questionText;
        this.answer = answer;
        this.imageUrl = imageUrl;
        this.hasImage = hasImage;
    }
}