using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulesPageManger : MonoBehaviour
{
    public GameObject[] Panels;
    public int PageIndex;
    private void Start()
    {
        PageIndex = 0;

        OnPage(PageIndex);
    }

    public void NextPage()
    {
        PageIndex++;
        if(PageIndex >= Panels.Length)
        {
            PageIndex = 0;
        }
         OnPage(PageIndex);
    }
    public void PreviousPage()
    {
        PageIndex--; 
        if(PageIndex < 0)
        {
            PageIndex = Panels.Length-1;
        }
        OnPage(PageIndex);


    }


    void OnPage(int index)
    {
        for(int i = 0; i < Panels.Length; i++)
        {
            if(i == index)
            {
                Panels[index].SetActive(true);
            }
            else
            {
                Panels[i].SetActive(false);
            }
        }
    }
}
