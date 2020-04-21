using UnityEditor;
using UnityEngine;

public class ClearPrefs : MonoBehaviour
{
	[MenuItem("GamerBox Studios/Clear PlayerPrefs")]
	static void ClearPlayerPrefs()
	{
		PlayerPrefs.DeleteAll();
	}
}
