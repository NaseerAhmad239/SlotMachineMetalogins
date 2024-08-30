using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Purchaser : MonoBehaviour
{
    public static Purchaser instance;
    private string saveCoinsKey = "mk_slot_coins"; // current coins
    public Text CoinsText;
    public GameObject PurchasePrefab;
    private GameObject ShopPanel;
    public void Awake()
    {
        instance = this;
    }
   
    public void ObjectDestory()
    {
        Destroy(ShopPanel);
    }


    public void OnShopPanel()
    {
         ShopPanel = Instantiate(PurchasePrefab,this.transform);
        ShopPanel.transform.GetChild(0).GetChild(0).GetComponent<Button>().onClick.AddListener(ObjectDestory);
    }
    public void OnPurchaseFailed()
    {
        MessageManager.Instance.ShowPanel("Purchase Failed:)");
    }

    public void OnPurchaseSucceed(int Coins)
    {
        CoinScript.instance.AddCoins(Coins);
    }

    
    
}
    
