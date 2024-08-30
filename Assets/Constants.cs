using Mkey;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class Constants : MonoBehaviour
{
    public static int TotalLoop
    {
        get { return GetLoopValue(); }
        set
        {

        }
    }
    public static int StopAtCash
    {
        get
        {
            return PlayerPrefs.GetInt("SavedValue");
        }
        set
        {

        }
    }
    private void Awake()
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[PlayerPrefs.GetInt("Language")];
        if (!PlayerPrefs.HasKey("SoundM"))
        {
            PlayerPrefs.SetInt("SoundM", 1);
        }
        AudioListener.volume = PlayerPrefs.GetInt("SoundM");
    }

    private IEnumerator Start()
    {
        if (SlotPlayer.Instance.Coins<100)
        {
            //PlayerPrefs.SetInt("first", 1);
            yield return new WaitForSecondsRealtime(3);
            MessageManager.Instance.showRewardPanel("YOU GOT 5000");
            SlotPlayer.Instance.AddCoins(5000);
        }
    }
    private static int GetLoopValue()
    {
        switch (PlayerPrefs.GetInt("Loop"))
        {
            case 0:
                {
                    return 1500000;

                }
            case 1:
                {
                    return 10;

                }
            case 2:
                {
                    return 15;

                }
            case 3:
                {
                    return 20;

                }
        }
        return 0;
    }

    public static string GetLocalizedString(string tableReference, string key)
    {
        return LocalizationSettings.StringDatabase.GetLocalizedString(tableReference, key);
    }

}
