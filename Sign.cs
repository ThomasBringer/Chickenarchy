using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
    public string speech;
    public GameObject balloon;
    public Text speechText;

    void Awake()
    {
        speechText.text = speech;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (Player.ColContainsPlayer(collision)) Speak();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (Player.ColContainsPlayer(collision)) Speak(false);
    }

    void Speak(bool speak = true)
    {
        balloon.SetActive(speak);
    }
}