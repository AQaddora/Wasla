using UnityEngine;
using UnityEngine.UI;

public class LessonSelectScript : MonoBehaviour
{
    public int lesson, unit;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate {
            ContentLoader.Instance.LoadLessonQuestions(lesson, unit); });
    }
}
