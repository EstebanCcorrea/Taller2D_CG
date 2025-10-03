using UnityEngine;
using TMPro;

public class CollecItem : MonoBehaviour
{
    public TMP_Text textGema;
    public TMP_Text textZafiro;
    public TMP_Text textBlink;

    void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (textGema) textGema.text = "x" + GameManager.Instance.Gema;
        if (textZafiro) textZafiro.text = "x" + GameManager.Instance.Zafiro;
        if (textBlink) textBlink.text = "x" + GameManager.Instance.Blink;
    }
}
