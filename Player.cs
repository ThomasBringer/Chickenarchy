using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player player;

    public static List<Chicken> chickens = new List<Chicken>();

    public float speed = .1f;
    public GameObject chicken;

    public Text scoreText;

    void Awake()
    {
        player = this;
        AddChicken(transform.position);
    }

    public static void AddChicken(Vector2 pos)
    {
        chickens.Add(Instantiate(player.chicken, pos, Quaternion.identity, player.transform).GetComponent<Chicken>());
        UpdateScoreText();
    }

    public static void KillChicken(int number)
    {
        for (int i = 0; i < number; i++)
        {
            Chicken chicken = chickens.Last();
            chicken.Kill();
            chickens.Remove(chicken);

            if (chickens.Count <= 1)
                break;
        }
        UpdateScoreText();
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        KillChicken(1);
    //    }
    //}

    static void UpdateScoreText()
    {
        int score = Score;
        player.scoreText.text = score + " chicken" + (score > 1 ? "s" : "");
    }

    public static int Score { get { return chickens.Count; } }

    public static bool ColContainsPlayer(Collider2D collision)
    {
        return chickens.Contains(collision.GetComponent<Chicken>());
    }
}