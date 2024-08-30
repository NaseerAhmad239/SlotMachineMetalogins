using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Square : MonoBehaviour
{
    public Text valueText;
    private int value;

    public void SetValue(int newValue)
    {
        value = newValue;
        valueText.gameObject.SetActive(true);
        valueText.text = Constants.GetLocalizedString("Setting",  "OPEN");
    }

    public int RevealValue()
    {
        valueText.text = value.ToString();
        valueText.gameObject.SetActive(true);
        //GetComponentInParent<TokenManager>().AddCoins(value);
        return value;
    }

    public int GetValue()
    {
        return value;
    }
}
