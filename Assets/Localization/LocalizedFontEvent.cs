using UnityEngine.Events;
using UnityEngine.Localization.Components;
using UnityEngine.Localization;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class LocalizedFontEvent : LocalizedAssetEvent<Font, LocalizedFont, UnityEventFont>
{
    [Space]
    [SerializeField] Text textMesh;
    [SerializeField] TextMesh textU;

    private void Start()
    {
        textMesh = GetComponent<Text>();        
        textU = GetComponent<TextMesh>();
        AssetReference.AssetChanged += AssetReference_AssetChanged;
    }
    private void AssetReference_AssetChanged(Font value)
    {
        if(textMesh != null)
        textMesh.font = value;

        if(textU != null)
            textU.font = value;

        //textMesh.UpdateFontAsset();
        //textMesh.ForceMeshUpdate(true, true);
        //textMesh.ForceFix = GameDataManager.Instance.GameLanguage == GameLanguage.Arabic;
        //textMesh.UpdateText();
    }
    private void OnDestroy()
    {
        AssetReference.AssetChanged -= AssetReference_AssetChanged;
    }
}

[Serializable]
public class UnityEventFont : UnityEvent<Font> { }
[Serializable]
public class LocalizedFont : LocalizedAsset<Font> { }