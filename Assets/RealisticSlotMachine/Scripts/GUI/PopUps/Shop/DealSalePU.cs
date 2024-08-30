using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mkey
{
    public class DealSalePU : PopUpsController
    {
        private RealShopType realShopType = RealShopType.DealCoins;
        [SerializeField]
        private Image thingImage;
        [SerializeField]
        private Text thingTextPrice;
        [SerializeField]
        private Text thingTextCount;
        [SerializeField]
        private Text timerText;
        [SerializeField]
        private Button thingBuyButton;
        [SerializeField]
        private Text dealTimeText;

        private DealSaleController DSC{ get { return DealSaleController.Instance; } }

        public override void RefreshWindow()
        {
            DSC.WorkingDealTickRestDaysHourMinSecEvent += WorkingDealTickRestDaysHourMinSecHandler;
            DSC.WorkingDealTimePassedEvent += WorkingDealTimePassedHandler;
            DSC.WorkingDealStartEvent += WorkingDealStartHandler;
            DSC.PausedDealStartEvent += PausedDealStartHandler;
            CreateThing();
            base.RefreshWindow();
        }

        private void OnDestroy()
        {
            DSC.WorkingDealTickRestDaysHourMinSecEvent -= WorkingDealTickRestDaysHourMinSecHandler;
            DSC.WorkingDealTimePassedEvent -= WorkingDealTimePassedHandler;
            DSC.WorkingDealStartEvent -= WorkingDealStartHandler;
            DSC.PausedDealStartEvent -= PausedDealStartHandler;
        }

        private void CreateThing()
        {
            
        }

        #region event handlers
        private void DealSaleStartHandler()
        {
            if (thingBuyButton) thingBuyButton.enabled = true;
        }

        private void WorkingDealTickRestDaysHourMinSecHandler(int d, int h, int m, float s)
        {
            if (dealTimeText) dealTimeText.text = String.Format("{0:00}:{1:00}:{2:00}", h, m, s);
        }

        private void WorkingDealTimePassedHandler(double initTime, double realyTime)
        {
            if (thingBuyButton) thingBuyButton.gameObject.SetActive(false);
            if (dealTimeText) dealTimeText.text = String.Format("{0:00}:{1:00}:{2:00}", 0, 0, 0);
        }

        private void WorkingDealStartHandler()
        {
            if (thingBuyButton) thingBuyButton.gameObject.SetActive(true);
        }

        private void PausedDealStartHandler()
        {
            if (thingBuyButton) thingBuyButton.gameObject.SetActive(false);
            if (dealTimeText) dealTimeText.text = String.Format("{0:00}:{1:00}:{2:00}", 0, 0, 0);
        }
        #endregion event handlers
    }
}