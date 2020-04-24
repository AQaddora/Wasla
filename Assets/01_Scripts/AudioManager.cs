using UnityEngine;
public class AudioManager : MonoBehaviour
{
	public static AudioManager Instance;

	[Header("Volume Controls")]
	public UnityEngine.UI.Slider musicVoumeSlider;
	public UnityEngine.UI.Slider sfxVoumeSlider;
	
	[Header("Basic Sound Effects")]
	public AudioClip[] trueSfx;
	public AudioClip[] wrongSfx;

	[Header("Audio Sources")]
	public AudioSource musicSource;
	public AudioSource sfxSource;

    void Awake()
    {
		if(Instance == null)
		{
			Instance = this;

			if (PlayerPrefs.HasKey(Constants.MUSIC_VOLUME_STRING))
			{
				musicSource.volume = PlayerPrefs.GetFloat(Constants.MUSIC_VOLUME_STRING);
			}
			if (PlayerPrefs.HasKey(Constants.SFX_VOLUME_STRING))
			{
				sfxSource.volume = PlayerPrefs.GetFloat(Constants.SFX_VOLUME_STRING);
			}
			
			sfxVoumeSlider.value = sfxSource.volume;
			musicVoumeSlider.value = musicSource.volume;
			
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(this.gameObject);
		}
    }

	public void PlayEffect(AudioClip clip)
	{
		if(clip != null)
			sfxSource.PlayOneShot(clip, 1f);
	}

	public void PlayTrueEffect()
	{
		sfxSource.Stop();
		sfxSource.PlayOneShot(trueSfx[Random.Range(0, trueSfx.Length)], 1f);
	}

	public void PlayWrongEffect()
	{
		sfxSource.Stop();
		sfxSource.PlayOneShot(wrongSfx[Random.Range(0, wrongSfx.Length)], 1f);
	}
}
