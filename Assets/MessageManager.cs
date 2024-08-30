using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour
{
    public Text Description,RewardDescription;
    public GameObject MessagePanel,RewardPanel,FreeSpin;

    public static MessageManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public  void ShowPanel(string Text)
    {
        Description.text = Text;
        StartCoroutine(Showpanel(MessagePanel));
    }
    public void showRewardPanel(string Text)
    {
        RewardDescription.text = Text;
        StartCoroutine(Showpanel(RewardPanel));
    }

    public void showFreeSpinPanel()
    {
        ////RewardDescription.text = Text;
        StartCoroutine(Showpanel(FreeSpin));
    }
    // Update is called once per frame
    IEnumerator Showpanel(GameObject Obj)
    {
        Obj.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        Obj.SetActive(false);
    }
}
