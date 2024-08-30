using Mkey;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;
//using static Unity.VisualScripting.Icons;

namespace Mohsin_Aksums
{
    public class Settings : MonoBehaviour
    {

        public GameObject[] LoopButton;

        [Header("SetValue")]
        public Button increaseButton;
        public Button decreaseButton;
        public Text valueText;

        private int value;
        private const int minValue = 0;
        private int maxValue
        {
            get { return SlotPlayer.Instance.Coins; }
        }
        private const int increment = 5000;
        private const string valueKey = "SavedValue";
        public Toggle SoundState;
        public Text SoundText;


        private void Start()
        {
            for (int i = 0; i < LoopButton.Length; i++)
            {
                var r = i;
                LoopButton[i].GetComponent<Button>().onClick.AddListener(() => SetLoopStatus(r));
            }
            SetLoopStatus(PlayerPrefs.GetInt("Loop"));
            value = PlayerPrefs.GetInt(valueKey, 1000);
            UpdateValueText();

            increaseButton.onClick.AddListener(IncreaseValue);
            decreaseButton.onClick.AddListener(DecreaseValue);
            SetSoundStatus(PlayerPrefs.GetInt("SoundM") == 1);
        }
        public void SetLoopStatus(int index)
        {
            //Debug.LogError("Shurli" + index);
            PlayerPrefs.SetInt("Loop", index);
            for (int i = 0; i < LoopButton.Length; i++)
            {
                if (i == index)
                {
                    LoopButton[i].GetComponent<Image>().color = Color.green;
                }
                else
                {
                    LoopButton[i].GetComponent<Image>().color = Color.white;
                }
            }
        }



        void IncreaseValue()
        {
            if (value < maxValue)
            {
                value += increment;
                if (value > maxValue)
                {
                    value = maxValue;
                }
                SaveValue();
                UpdateValueText();
            }
        }

        void DecreaseValue()
        {
            if (value > minValue)
            {
                value -= increment;
                if (value < minValue)
                {
                    value = minValue;
                }
                SaveValue();
                UpdateValueText();
            }
        }

        void UpdateValueText()
        {
            valueText.text = FormatMoney(value);
        }

        public static string FormatMoney(double amount)
        {
            if (amount >= 1000000)
                return (amount / 1000000).ToString("0.##M");
            else if (amount >= 1000)
                return (amount / 1000).ToString("0.##K");
            else
                return amount.ToString("0");
        }
        void SaveValue()
        {
            PlayerPrefs.SetInt(valueKey, value);
            PlayerPrefs.Save();
        }

        public void SetLanguage(int index)
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
            PlayerPrefs.SetInt("Language", index);
            SetSoundStatus(PlayerPrefs.GetInt("SoundM") == 1);
            MessageManager.Instance.ShowPanel(Constants.GetLocalizedString("Setting", "Loading..."));
            //MessageManager.Instance.ShowPanel( "Loading...");

        }

        public void SetSoundStatus(bool val)
        {
            AudioListener.volume = val ? 1 : 0;
            PlayerPrefs.SetInt("SoundM", val ? 1 : 0);
            SoundText.text = PlayerPrefs.GetInt("SoundM") == 1 ? Constants.GetLocalizedString("Setting", "ON") : Constants.GetLocalizedString("Setting", "OFF");
            //SoundText.text = PlayerPrefs.GetInt("SoundM") == 1 ?"ON" : "OFF";

            SoundState.isOn = val;

        }
    }
}
