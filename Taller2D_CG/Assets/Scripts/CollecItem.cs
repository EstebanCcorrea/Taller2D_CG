using UnityEngine;
using TMPro;

public class CollecItem : MonoBehaviour
{
    private int Gema = 0;
    private int Zafiro = 0;
    private int Blink = 0;

    public TMP_Text textGema;
    public TMP_Text textZafiro;
    public TMP_Text textBlink;

    void Start()
    {
        UpdateUI();
    }

    public void AddItem(string type)
    {
        switch (type)
        {
            case "Gema":
                Gema++;
                Debug.Log("Gemas: " + Gema);
                break;
            case "Zafiro":
                Zafiro++;
                Debug.Log("Zafiros: " + Zafiro);
                break;
            case "Blink":
                Blink++;
                Debug.Log("Blinks: " + Blink);
                break;
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (textGema) textGema.text = "x" + Gema;
        if (textZafiro) textZafiro.text = "x" + Zafiro;
        if (textBlink) textBlink.text = "x" + Blink;
    }
}
