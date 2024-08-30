using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mkey
{
    public class ShopWindowController : PopUpsController
    {
        public ShopThingHelper shopThingPrefab;
        public RectTransform ThingsParent;
        private List<ShopThingHelper> shopThings;
        private RealShopType realShopType = RealShopType.Coins;

        public override void RefreshWindow()
        {
            CreateThingTab();
            base.RefreshWindow();
        }

        private void CreateThingTab()
        {
            ShopThingHelper[] sT = ThingsParent.GetComponentsInChildren<ShopThingHelper>();
            foreach (var item in sT)
            {
                DestroyImmediate(item.gameObject);
            }


            List<ShopThingDataReal> products = new List<ShopThingDataReal>();
          

            if (products.Count==0) return;

            shopThings = new List<ShopThingHelper>();
            for (int i = 0; i < products.Count; i++)
            {
              if(products[i]!=null && products[i].showInShop && products[i].shopType == realShopType)  shopThings.Add(ShopThingHelper.CreateThing(shopThingPrefab, ThingsParent, products[i]));
            }
        }
    }
}