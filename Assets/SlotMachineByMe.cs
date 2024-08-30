using AYellowpaper.SerializedCollections;
using DG.Tweening;
using Mkey;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachineByMe : MonoBehaviour
{
    public GameObject[] Symbols;
    public SlotData[] SlotData;
    public RewardData[] RewardInfo;
    public int MinBet, MaxBet, value, increment;
    public Transform SymbolContent;
    public bool isShowing;
    public List<GameObject> SpawnItems;
    public Dictionary<string, int> Items = new Dictionary<string, int>();

    public Text ValueText;

    public bool IsSpawing;
    int Wincoin,WinSpin;
    [Header("Script Refferences")]
    public SlotController slotController;
    public SlotControls controls;
    [Header("Prefab Refferences")]
    public TextMesh WinText;
    private TextMesh winText;
    bool isAuto;
    [Header("Sounds")]
    public AudioSource Audio;
    public AudioClip SpinAudio;
    private void Start()
    {
        UpdateValueText();
        SpawnOnSlotData();
    }

    public async void SpawnOnSlotData()
    {

        //StartCoroutine(SpawnRoutine());
        await SpawnRoutine();

    }

    private async Task SpawnRoutine()
    {

        Items.Clear();
        foreach (var data in SlotData)
        {
            await SpawnSymbols(data);
        }


        await CheckWinningConditionsAsync();

    }

    private async Task SpawnSymbols(SlotData data)
    {
        data.Spawned = new List<GameObject>();
        float delay = 0;

        for (int i = data.Slots.Length - 1; i >= 0; i--)
        {
            var symbol = InstantiateSymbol(data.Slots[i].position, delay, data.SpawnPont);
            data.Spawned.Add(symbol);
            SpawnItems.Add(symbol);
            delay += 0.01f;

            await Task.Delay(TimeSpan.FromSeconds(/*0.3f +*/ delay));
        }

        UpdateItemsCount(data.Spawned);
    }

    private GameObject InstantiateSymbol(Vector3 position, float delay, Transform SpawnPoint)
    {
        GameObject symbol = Instantiate(Symbols[UnityEngine.Random.Range(0, Symbols.Length)], SpawnPoint.position, Quaternion.identity, SymbolContent);
        symbol.transform.DOMove(position, 0.1f).SetDelay(delay).SetEase(Ease.Flash).OnComplete(() => symbol.GetComponent<Animator>().SetTrigger("Spawn"));
        return symbol;
    }

    private void UpdateItemsCount(List<GameObject> spawned)
    {
        foreach (var symbol in spawned)
        {
            if (symbol != null)
            {
                if (!Items.ContainsKey(symbol.name))
                {
                    Items[symbol.name] = 1;
                }
                else
                {
                    Items[symbol.name]++;
                }
            }
        }
    }



    public  void SpinClick()
    {
        if(isAuto)
            isAuto = false;
        Spin();
    }

    private async Task MoveAndRespawnAsync()
    {
        Debug.LogError("Theek hai smjh gya");

        await Task.Delay(TimeSpan.FromSeconds(1));

        SpawnOnSlotData();

        Debug.LogError("oyeee");
    }

    public void SpawnMissingItems(SlotData data)
    {
        MoveNonNullToTop(data.Spawned);
        SpawnNonNullItems(data);
        SpawnNullItems(data);

    }

    private void SpawnNonNullItems(SlotData data)
    {
        float delay = 0;

        for (int i = data.Slots.Length - 1; i >= 0; i--)
        {
            if (data.Spawned[i] != null)
            {
                data.Spawned[i].transform.DOMove(data.Slots[3 - i].position, 0.3f).SetDelay(delay).SetEase(Ease.Flash).OnComplete(() => data.Spawned[i].GetComponent<Animator>().SetTrigger("Spawn"));
                delay += 0.2f;
            }
        }
    }

    private async void SpawnNullItems(SlotData data)
    {
        float delay = 0;

        for (int i = data.Slots.Length - 1; i >= 0; i--)
        {
            if (data.Spawned[i] == null)
            {
                var symbol = InstantiateSymbol(data.Slots[3 - i].position, delay, data.SpawnPont);
                data.Spawned[i] = symbol;
                SpawnItems.Add(symbol);
                delay += 0.2f;
            }
        }

        await CheckWinningConditionsAsync();
    }

    private void MoveNonNullToTop(List<GameObject> list)
    {
        list.RemoveAll(item => item == null);
        while (list.Count < list.Capacity)
        {
            list.Add(null);
        }
    }
    public string[] winningKeys;
    public async Task CheckWinningConditionsAsync()
    {
        await Task.Delay(TimeSpan.FromSeconds(1));

         winningKeys = Items.Where(pair => pair.Value >= 8).Select(pair => pair.Key).ToArray();

        foreach (var key in winningKeys)
        {
            if (Items.Count > 0)
            {
                if (Items[key] >= 8 && Items[key] <= 9)
                {
                    RewardCalculation(0, key);
                    // Handle specific condition for range 8-9 asynchronously if needed

                }
                else if (Items[key] >= 10 && Items[key] <= 11)
                {
                    RewardCalculation(1, key);

                    // Handle specific condition for range 10-11 asynchronously if needed
                }
                else if (Items[key] >= 12 && Items[key] <= 24)
                {
                    RewardCalculation(2, key);

                    // Handle specific condition for range 12-24 asynchronously if needed
                }

                Debug.LogError(key);
                await ShowWinningValue(key);
            }
        }
        Items.Clear();
        IsSpawing = false;
        await Task.Delay(TimeSpan.FromSeconds(1));
        if (isAuto)
        {
            Spin();
        }
    }

    public void RewardCalculation(int Index, string key)
    {
        for (int i = 0; i < RewardInfo.Length; i++)
        {
            if (key.Contains(RewardInfo[i].SymbolName))
            {
                switch (Index)
                {
                    case 0:
                        {
                            Wincoin = (value * 1000) * (RewardInfo[i].EightToNine* value) * value;
                            WinSpin = RewardInfo[i].EightToNineSpin;
                            break;
                        }
                    case 1:
                        {
                            Wincoin = (value * 1000) * (RewardInfo[i].TenToEleven * value) * value;
                            WinSpin = RewardInfo[i].TenToEleven;
                            break;
                        }
                    case 2:
                        {
                            Wincoin = (value * 1000) * (RewardInfo[i].TwelveToTwentyFour * value) * value;
                            WinSpin = RewardInfo[i].TwelveToTwentyFourSpin;
                            break;
                        }
                }
            }
        }
        Debug.LogError($"{RewardInfo[Index].TenToEleven}");

        ShowWinText();
    }

    public async Task ShowWinningValue(string value)
    {
        if (SpawnItems.Count > 0)
        {
            foreach (var item in SpawnItems.Where(item => item.name == value))
            {
                item.GetComponent<Symbols>().Animate();
                await Task.Yield(); // Ensures the method is asynchronous
            }
           
        }

        await Task.Delay(TimeSpan.FromSeconds(4));

        foreach (var data in SlotData)
        {
            Items.Clear();
            SpawnMissingItems(data);
        }

        IsSpawing = false;
    }
    public void IncreaseValue()
    {
        value++;
        if (value > MaxBet)
        {
            value = MinBet;
        }
        UpdateValueText();
    }

    public void MaxBetClick()
    {
       
            value = MaxBet;
        
        UpdateValueText();
    }

    public void DecreaseValue()
    {
        value--;
        if (value <= 0)
        {
            value = MaxBet;
        }
        UpdateValueText();
    }

    void UpdateValueText()
    {
        ValueText.text = (value * 1000).ToString();
    }
    public void AutoSpinClick()
    {
        isAuto = true;
        Spin();
    }

    public async void Spin()
    {
        if (controls.FreeSpins>0||SlotPlayer.Instance.Coins >= value * 1000)
        {
            if (!IsSpawing)
            {
                Audio.PlayOneShot(SpinAudio);
                Wincoin = 0;
                controls.WinAmountText.text = "0";
                WinSpin = 0;
                if (controls.FreeSpins <= 0)
                {
                    SlotPlayer.Instance.AddCoins(-(value * 1000));
                }
                else
                {
                    MessageManager.Instance.showFreeSpinPanel();
                }
                //StopAllCoroutines();
                IsSpawing = true;
                //SlotData[] data = GetComponentsInChildren<SlotData>();
                Items.Clear();
                SpawnItems.Clear();
                if (winText)
                {
                    Destroy(winText);
                }
                foreach (var slotData in SlotData)
                {
                    slotData.DestroyGameObject();
                }
                //StartCoroutine(MoveAndRespawn());
                await MoveAndRespawnAsync();

            }
        }
        else
        {
            MessageManager.Instance.ShowPanel("You Don't Have Enough Coins");
        }
    }
    public void RemoveGameObject(GameObject gameObjectToRemove)
    {
        if (SpawnItems.Remove(gameObjectToRemove))
        {
            Debug.Log($"Removed {gameObjectToRemove.name} from the list.");
            Destroy(gameObjectToRemove);
        }
        else
        {
            Debug.Log("GameObject not found in the list.");
        }
    }

    #region Wintext

    public void ShowWinText()
    {
        if (WinText)
        {
            if (!winText)
            {
                winText = Instantiate(WinText);

                winText.transform.localScale = Vector3.one;
            }
            winText.text = Wincoin.ToString();
            SlotPlayer.Instance.AddCoins(Wincoin);
            //SlotPlayer.Instance.
            controls.AddFreeSpins(WinSpin);
            controls.WinAmountText.text = Wincoin.ToString();
            winText.gameObject.SetActive(true);
            //Debug.LogError(WonCoins);
        }
        else
        {
            if (winText) winText.gameObject.SetActive(false);
        }
    }
    #endregion

    [System.Serializable]
    public class RewardData
    {
        public string SymbolName;
        public int EightToNine;
        public int TenToEleven;
        public int TwelveToTwentyFour;
        public int EightToNineSpin, TenToElevenSpin, TwelveToTwentyFourSpin;

    }
}
