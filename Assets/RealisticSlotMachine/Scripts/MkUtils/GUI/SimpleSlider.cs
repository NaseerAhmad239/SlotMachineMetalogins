using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;
/*
 13.01.19
    -add fillImage exist
 13.05.20
    - PSlider
  18.02.2021 - OnValueChanged;
 */

namespace Mkey
{
    [ExecuteInEditMode]
    public class SimpleSlider :  PSlider
    {
        public Image fillImage;

        #region temp vars
        private RectTransform rtL;
        private RectTransform rtR;
        #endregion temp vars
        public GameObject Centercircle;
        public Text PercentageText;

        #region regular
        private void OnEnable()
        {
            Rotateit();

        }

        private void OnValidate()
        {
            fillAmount = Mathf.Clamp01(fillAmount);
        }

        private void Update()
        {
            if (!fillImage) return;
            fillImage.fillAmount = fillAmount;
        }
        #endregion regular
        public void Rotateit()
        {
            Centercircle.transform.DORotate(new Vector3(0, 0, -360), 3f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
            DOTween.To(x => fillAmount = x, 0, 100, 0.5f).OnUpdate(()=>PercentageText.text=((int)fillAmount).ToString());
        }
    }
}