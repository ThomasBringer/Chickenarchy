using UnityEngine;

public class Chicken : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;

    bool dead;

    void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    public void Kill()
    {
        dead = true;
        anim.SetTrigger("Kill");
    }

    void Update()
    {
        if (dead) { rb.velocity = Vector2.zero; return; }

        bool speaking = GameManager.speaking;
        anim.SetBool("Idle", speaking);
        if (speaking) { rb.velocity = Vector2.zero; return; }

        Vector2 input = Cam.MousePos - (Vector2)transform.position;

        Face(input);
        Move(input);
    }

    void Face(Vector2 input)
    {
        transform.localScale = new Vector2(Mathf.Sign(input.x), 1);
    }

    void Move(Vector2 input)
    {
        rb.AddForce(input * Player.player.speed);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}