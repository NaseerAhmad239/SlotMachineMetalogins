using System.Collections;
using UnityEngine;

public class Symbols : MonoBehaviour
{
    public GameObject particles;

    public void SpawnParticle()
    {
        GameObject particle = Instantiate(particles, transform.position, Quaternion.identity);
        Destroy(particle, 2f);
    }

    public void Animate()
    {
        GetComponent<Animator>().SetTrigger("Animate");
        StartCoroutine(WaitAndDestroy());
    }

    private IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSecondsRealtime(2f);
        SpawnParticle();

        var slotMachine = FindObjectOfType<SlotMachineByMe>();
        if (slotMachine != null)
        {
            slotMachine.RemoveGameObject(gameObject);
        }

        Destroy(gameObject);
    }
}
