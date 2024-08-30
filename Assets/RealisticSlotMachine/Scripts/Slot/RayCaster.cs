using UnityEngine;

namespace Mkey
{
    public class RayCaster : MonoBehaviour
    {
        public int ID { get; set; } // for calcs
        public int Symbol;
        public int index;
       
        public SlotSymbol GetSymbol()
        {
            Collider2D hit = Physics2D.OverlapPoint(new Vector2(transform.position.x, transform.position.y));
            if (hit)
            {
                Symbol = hit.GetComponent<SlotSymbol>().IconID;
                return hit.GetComponent<SlotSymbol>();
            }
            else return null;
        }

    }
}