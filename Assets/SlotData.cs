using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotData : MonoBehaviour
{
    public Transform SpawnPont;
    public Transform[] Slots;
    public List<GameObject> Spawned = new List<GameObject>(4);
    public GameObject Particles;

    private SlotMachineByMe slotMachine;

    private void Start()
    {
        slotMachine = FindAnyObjectByType<SlotMachineByMe>();
        //slotMachine.SpawnOnSlotData();
    }

    public void DestroyGameObject()
    {
        float delay = 0;
        for (int i = 0; i < Spawned.Count; i++)
        {
            var symbol = Spawned[i];
            if (symbol != null)
            {
                symbol.transform.DOMove(Slots[i].position - new Vector3(0f, 50f, 0f), 0.8f)
                    .SetDelay(delay)
                    .SetEase(Ease.Flash)
                    .OnComplete(() => Destroy(symbol));
                delay += 0.2f;
            }
        }

    }

    
}
