using UnityEngine;
using UnityEngine.UI;

public class UnitSelecScript : MonoBehaviour
{
    public int unit;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate { ContentLoader.Instance.LoadUnitLessons(unit); });
    }
}
