using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;
[RequireComponent(typeof(LocalizedFontEvent))]
public class CustomLocalizedStringEvent : LocalizeStringEvent
{

    [SerializeField] Text proUGUI;
    [SerializeField] TextMesh pro;

    private void Awake()
    {
        proUGUI = GetComponent<Text>();
        pro = GetComponent<TextMesh>();
    }
    protected override void OnEnable()
    {
        //base.OnEnable();
        StringReference.StringChanged += StringReference_StringChanged;
    }

    private void StringReference_StringChanged(string value)
    {
        if (proUGUI != null)
        {

            proUGUI.text = value;
        }
        if (pro != null)
        {

            pro.text = value;
            //pro.fontSize -= 20;
            pro.text = pro.text;
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        StringReference.StringChanged -= StringReference_StringChanged;
    }
}
