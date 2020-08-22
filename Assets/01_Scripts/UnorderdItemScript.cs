using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnorderdItemScript : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(delegate
        {
            AddChar();
        });
    }

    void AddChar()
    {
        if(SolutionManager.Instance.Add(GetComponentInChildren<Text>().text));
            Destroy(transform.gameObject);
    }
}
