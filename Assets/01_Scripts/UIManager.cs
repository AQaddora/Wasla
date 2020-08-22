using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [Header("Screens")] [SerializeField] Fader title;
    [SerializeField] private Fader settings, quitPanel, game, units, lessons; 
    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void HideAll()
    {
        foreach (Fader fader in FindObjectsOfType<Fader>())
        {
            fader.Hide();
        }
    }
    
    #region ButtonsFunctions
    public void PlayButton_OnClick()
    {
        HideAll();
        units.Show();
    }

    public void ShowLessons()
    {
        HideAll();
        lessons.Show();
    }

    public void StartGame()
    {
        HideAll();
        game.Show();
        QuestionsManager.Instance.SetNextQuestion();
    }


    public void PauseButton()
    {
        //PauseGame
        Settings_OnClick();
    }
    public void Settings_OnClick()
    {
        //HideAll();
        settings.Show();
    }

    public void ShowHomeScreen()
    {
        HideAll();
        title.Show();
    }
    
    public void SettingsBack()
    {
        //Unpause game if 'Paused'
        settings.Hide();
    }
    
    public void Quit_OnClick()
    {
        //HideAll();
        quitPanel.Show();
    }
    
    /*in quit panel YES button*/
    public void QuitForSure()
    {
        Application.Quit();
        Debug.Log("Quitting, We will miss you!");
    }

    /*in quit panel NO button*/
    public void DontQuit()
    {
        quitPanel.Hide();
        //title.Show();
    }

    public void FacebookButton()
    {
        //fb.me/GamerBoxStudios
        Application.OpenURL(Constants.Instance.facebookURL);
    }

    public void twitterButton()
    {
        //https://twitter.com/AQaddora96
        Application.OpenURL(Constants.Instance.twitterURL);
    }

    public void MusicVolume(float volume)
    {
        AudioManager.Instance.musicSource.volume = volume;
        PlayerPrefs.SetFloat(Constants.MUSIC_VOLUME_STRING, volume);
    }

    public void SfxVolume(float volume)
    {
        AudioManager.Instance.sfxSource.volume = volume;
        PlayerPrefs.SetFloat(Constants.SFX_VOLUME_STRING, volume);
    }

    public void SupportButton_OnClick()
    {
        Application.OpenURL(Constants.Instance.supportUrl);
    }

    public void PrivacyButton_OnClick()
    {
        Application.OpenURL(Constants.Instance.privacyURL);
    }
    #endregion
}
