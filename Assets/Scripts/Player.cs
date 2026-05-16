using UnityEngine;

public class Player : MonoBehaviour
{
    
    private Animator anime;
    private Rigidbody2D corpo;
    public float velocidade = 3.0f;
    private SpriteRenderer sprite;
    void Start()
    {
        corpo = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anime = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Pega o input
        float horizontal = Input.GetAxis("Horizontal");
        //Debug.Log(horizontal);

        //Processa o input
        flip(horizontal);
        corpo.linearVelocity = new Vector2(horizontal * velocidade, corpo.linearVelocity.y);
        anime.SetFloat("speed", Mathf.Abs(horizontal)); // |-1| ou |1| = 1
    }

    private void flip(float horizontal)
    {
        if (horizontal > 0)
        {
            sprite.flipX = false;
        }
        else if (horizontal < 0)
        {
            sprite.flipX = true;
        }
    }
}
