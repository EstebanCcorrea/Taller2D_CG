using UnityEngine;
using TMPro;
using UnityEngine.UI;

[SerializeField]
public class CollecItem : MonoBehaviour
{
    private int Gema;
    private int Zafiro;
    private int Blink;


    public TMP_Text textGema;
    public TMP_Text textZafiro;
    public TMP_Text textBlink;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Gema = 0;
        Zafiro = 0;
        Blink = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Gema"))
        {
            Destroy(collision.gameObject);
            Gema++;
            textGema.text = "x" + Gema;
            Debug.Log("Gemas: " + Gema);
            
        }

        if (collision.transform.CompareTag("Zafiro"))
        {
            Destroy(collision.gameObject);
            Zafiro++;
            textZafiro.text = "x" + Zafiro;
            Debug.Log("Zafiros: " + Zafiro);
        }

        if (collision.transform.CompareTag("Blink"))
        {
            Destroy(collision.gameObject);
            Blink++;
            textBlink.text = "x" + Blink;
            Debug.Log("Bananas: " + Blink);
        }

    }

}
