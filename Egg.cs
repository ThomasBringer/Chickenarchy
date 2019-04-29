using UnityEngine;

public class Egg : MonoBehaviour
{
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (Player.ColContainsPlayer(collision)) anim.SetTrigger("Burst");
    }

    public void Burst()
    {
        Player.AddChicken(transform.position);
        Destroy(gameObject);
    }
}