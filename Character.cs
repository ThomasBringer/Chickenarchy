using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    public bool endGame=false;

    [System.Serializable]
    public struct Speeches
    {
        public string[] onlyOnce;
        public string[] everytime;
        public string[] yes;
        public string[] no;
        public string[] last;
    }
    public Speeches speeches;

    enum Discuss { First, Any, End }
    Discuss discuss = Discuss.First;

    public GameManager.ItemType requiredItem;
    public GameManager.ItemType giftItem;
    public int price;

    public GameObject balloon;
    public Text speechText;

    bool allowed = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (Player.ColContainsPlayer(collision)) StartCoroutine(Speak());
    }

    IEnumerator Speak()
    {
        balloon.SetActive(true);
        GameManager.speaking = true;
        Physics2D.autoSimulation = false;

        Cam.Focus(transform.position);

        string[] speeches = ChooseSpeech();

        foreach (var speech in speeches)
        {
            speechText.text = speech;

            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
        }

        balloon.SetActive(false);
        GameManager.speaking = false;
        Physics2D.autoSimulation = true;
        
        if (discuss != Discuss.End)
        {
            discuss = allowed ? Discuss.End : Discuss.Any;
        }

        if (allowed)
        {
            if (endGame)
            {
                SceneManager.LoadScene(2);
            }

            Player.KillChicken(price);
            if (requiredItem != GameManager.ItemType.None)
            {
                GameManager.CurrentItem = GameManager.ItemType.None; GameManager.SpawnItem(transform, requiredItem);
            }

            GameManager.CurrentItem = giftItem;
        }
    }

    string[] ChooseSpeech()
    {
        var speech = new List<string[]>();

        switch (discuss)
        {
            case Discuss.First:
                speech.Add(speeches.onlyOnce);
                speech.Add(EverytimeSpeech());
                break;

            case Discuss.Any:
                speech.Add(EverytimeSpeech());
                break;

            case Discuss.End:
                allowed = false;
                speech.Add(speeches.last);
                break;
        }

        return CombineArrays(speech.ToArray());
    }

    string[] EverytimeSpeech()
    {
        var speech = new List<string[]>();

        speech.Add(speeches.everytime);

        allowed = (Player.Score > price) && (requiredItem == GameManager.ItemType.None || requiredItem == GameManager.CurrentItem);

        speech.Add(allowed ? speeches.yes : speeches.no);

        return CombineArrays(speech.ToArray());
    }

    T[] CombineArrays<T>(T[][] array2D)
    {
        var list = new List<T>();

        foreach (T[] array1D in array2D)
        {
            foreach (T item in array1D)
            {
                list.Add(item);
            }
        }

        return list.ToArray();
    }
}