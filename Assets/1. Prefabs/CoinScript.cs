using DG.Tweening;
using Mkey;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinScript : MonoBehaviour
{
    public Text Coins;
    public TextMesh CoinMesh;

    private string saveCoinsKey = "mk_slot_coins"; // current coins
    public static CoinScript instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //if (Coins)

        //    Coins.text = PlayerPrefs.GetInt(saveCoinsKey).ToString();
        //if (CoinMesh)
        //    CoinMesh.text = PlayerPrefs.GetInt(saveCoinsKey).ToString();
    }
    public void AddCoins(int Amount)
    {
        float val = PlayerPrefs.GetInt(saveCoinsKey);
        //PlayerPrefs.SetInt(saveCoinsKey, PlayerPrefs.GetInt(saveCoinsKey) + Amount);
        SlotPlayer.Instance.AddCoins(Amount);
        DOTween.To(() => val, x => val = x, PlayerPrefs.GetInt(saveCoinsKey), 3f).OnUpdate(() =>
        {
            if (Coins)
                Coins.text = val.ToString();

            if (CoinMesh)
                CoinMesh.text = val.ToString();



        });

    }
}
