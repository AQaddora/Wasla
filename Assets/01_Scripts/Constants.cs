using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour
{
    public static Constants Instance;

    public static string MUSIC_VOLUME_STRING = "MusicVolume";
    public static string SFX_VOLUME_STRING = "SfxVolume";
    
    [Header("Strings")] 
    public string privacyURL;
    public string facebookURL;
    public string twitterURL;
    public string supportUrl;
    
    private void Awake()
    {
        Instance = this;
    }
}
