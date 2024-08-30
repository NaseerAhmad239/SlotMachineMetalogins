using AYellowpaper.SerializedCollections;
using DG.Tweening;
using Mkey;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TokenManager : MonoBehaviour
{
    public Text Coins;
    public Text titleText;
    public Image ticketImage;
    public Text ticketPriceText;
    public Button conditionsButton;
    public Text notEnoughText;
    public GridLayoutGroup miniGameGrid;
    public GameObject squarePrefab;
    public Text winningsText;
    public Button closeButton;
    private string saveCoinsKey = "mk_slot_coins";

    private int ticketPrice = 20000;
    public int TotalCoins
    {
        get
        {
            return PlayerPrefs.GetInt(saveCoinsKey);
        }
        set
        {
            SlotPlayer.Instance.AddCoins(value);
        }
    }
    private List<int> values = new List<int> { 0, 0, 0, 0, 0, 5000, 5000, 5000, 5000, 5000, 5000, 25000, 25000, 25000, 100000, 100000, 100000, 1000000, 1000000, 1000000 };

    [Header("Win Panel")]
    public GameObject winPanel;
    public Text WinPanelText;
    public List<GameObject> SpawnedGameObject;
    public SerializedDictionary<int, int> valueCount = new SerializedDictionary<int, int>();

    void Start()
    {
        UpdateTicketDisplay();
        conditionsButton.onClick.AddListener(ShowConditions);
        closeButton.onClick.AddListener(CloseMiniGame);
        notEnoughText.gameObject.SetActive(false);
        UpdateUI();
    }
    public void UpdateUI()
    {
        Coins.text = TotalCoins.ToString();

    }
    public void AddCoins(int Coin)
    {
        if (Coin > 0)
        {
            //PlayerPrefs.SetInt(saveCoinsKey, PlayerPrefs.GetInt(saveCoinsKey) + Coin);
            int playerChips = TotalCoins;
            TotalCoins = Coin;
            DOTween.To(() => playerChips, x => playerChips = x, PlayerPrefs.GetInt(saveCoinsKey), 3f).OnUpdate(() => Coins.text = playerChips.ToString());
            UpdateUI();
        }


    }


    void UpdateTicketDisplay()
    {

        if (PlayerPrefs.GetInt("firstTry") == 0)
        {
            ticketPriceText.text = Constants.GetLocalizedString("Setting", "Try for free");
           // ticketPriceText.text = "Try for free";

        }
        else
        {
            ticketPriceText.text = ticketPrice.ToString();
        }
    }
    private void OnEnable()
    {
        UpdateTicketDisplay();
    }
    public void OnTicketClick()
    {
        valueCount = new SerializedDictionary<int, int>();
        if (PlayerPrefs.GetInt("firstTry") == 0 || TotalCoins >= ticketPrice)
        {
            if (PlayerPrefs.GetInt("firstTry") != 0)
            {
                TotalCoins = -ticketPrice;
            }
            PlayerPrefs.SetInt("firstTry", 1);
            StartMiniGame();
            UpdateTicketDisplay();
            //SlotPlayer.Instance.AddCoins(-20000);

        }
        else
        {
            //notEnoughText.gameObject.SetActive(true);
            MessageManager.Instance.ShowPanel("You Have Not Enough Coin");
        }
    }

    void StartMiniGame()
    {
        titleText.gameObject.SetActive(false);
        // ticketImage.gameObject.SetActive(false);
        // ticketPriceText.gameObject.SetActive(false);
        // conditionsButton.gameObject.SetActive(false);
        miniGameGrid.gameObject.SetActive(true);

        List<int> shuffledValues = new List<int>(values);
        Shuffle(shuffledValues);

        foreach (Transform child in miniGameGrid.transform)
        {
            Destroy(child.gameObject);
        }
        SpawnedGameObject.Clear();
        for (int i = 0; i < 20; i++)
        {
            GameObject square = Instantiate(squarePrefab, miniGameGrid.transform);
            square.GetComponent<Square>().SetValue(shuffledValues[i]);
            square.GetComponent<Button>().onClick.AddListener(() => OnSquareClick(square));
            SpawnedGameObject.Add(square);
        }
        UpdateUI();
    }

    void OnSquareClick(GameObject square)
    {
        int value = square.GetComponent<Square>().RevealValue();
        CheckForWinnings(square.GetComponent<Square>().GetValue());
      
    }
    void CheckForWinnings(int val)
    {
       //valueCount = 

       
            int value = val;
            if (!valueCount.ContainsKey(value))
            {
                valueCount.Add(value, 1);
            }
            else if (valueCount.ContainsKey(value))
            {
                valueCount[value]++;
            }
       

        foreach (var pair in valueCount)
        {
            Debug.LogError(pair.Key + " " + pair.Value);
            if (pair.Value >= 3)
            {
                EndMiniGame(pair.Key);
                break;
            }
        }
    }

    void EndMiniGame(int winnings)
    {
        TotalCoins = winnings;
        winningsText.text = winnings.ToString() ;
        AddCoins(winnings);
        winPanel.SetActive(true);
        SpawnedButtonStatus(false);
        winningsText.gameObject.SetActive(true);
        closeButton.gameObject.SetActive(true);
        UpdateUI();

    }

    void CloseMiniGame()
    {
        titleText.gameObject.SetActive(true);
        ticketImage.gameObject.SetActive(true);
        ticketPriceText.gameObject.SetActive(true);
        conditionsButton.gameObject.SetActive(true);
        miniGameGrid.gameObject.SetActive(false);
        winningsText.gameObject.SetActive(false);
        //closeButton.gameObject.SetActive(false);
        gameObject.SetActive(false);
        UpdateTicketDisplay();
        UpdateUI();

    }

    public void SpawnedButtonStatus(bool val)
    {
        foreach (GameObject child in SpawnedGameObject)
        {
            child.GetComponent<Button>().interactable = val;
        }
    }
    void ShowConditions()
    {
        // Implement the function to show the conditions/rules of the game
    }


    public void ShowWinPanel(string text)
    {
        winningsText.text = text;
        winPanel.SetActive(true);
    }

    void Shuffle(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}

