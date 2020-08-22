using UnityEngine;
using UnityEngine.UI;

public class SolutionItemScript : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(delegate
        {
            DropChar();
        });
    }

    void DropChar()
    {
        SolutionManager.Instance.Drop(transform.GetSiblingIndex(), GetComponentInChildren<Text>().text);
    }
}
