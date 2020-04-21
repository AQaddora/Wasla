using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class Fader : MonoBehaviour
{
	public float duration = 0.8f;
	private bool doFade = false;
	[HideInInspector]
	public CanvasGroup cg;
	private int direction = -1;
	private Image bg;

	void Awake()
	{
		bg = GetComponent<Image>();
		cg = GetComponent<CanvasGroup>();
	}

	void Update()
	{
		if (doFade)
		{
			cg.alpha += direction * Time.deltaTime / duration;
			cg.blocksRaycasts = cg.alpha >= 0.5f;
			doFade = !(cg.alpha == 1 || cg.alpha == 0);
		}
	}

	public void SetBgColor(Color color)
	{
		bg.color = color;
	}

	public void Show()
	{
		direction = 1;
		doFade = true;
	}

	public void ShowAfterDuration()
	{
		Invoke("Show", duration);
	}

	public void Hide()
	{
		direction = -1;
		doFade = true;
	}

	public void Toggle()
	{
		if (cg.alpha == 1)
		{
			Hide();
		}
		else
		{
			Show();
		}
	}
}
