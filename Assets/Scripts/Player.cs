using UnityEngine;

public class Player : MonoBehaviour
{
    
    private Animator anime;
    private Rigidbody2D corpo;
    public float velocidade = 3.0f;
    private SpriteRenderer sprite;

    // PARTE DO PULO
    public Transform groundCheck;
    public LayerMask groundLayer;
    void Start()
    {
        corpo = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anime = GetComponent<Animator>();
    }

    


    // Update is called once per frame
    void Update()
    {   
        Jump();
        //Pega o input
        float horizontal = Input.GetAxis("Horizontal");
        //Debug.Log(horizontal);

        //Processa o input
        flip(horizontal);
        corpo.linearVelocity = new Vector2(horizontal * velocidade, corpo.linearVelocity.y);
        anime.SetFloat("speed", Mathf.Abs(horizontal)); // |-1| ou |1| = 1

        if(corpo.linearVelocity.y < -0.1f){
            anime.SetBool("Falling", true);
        }

        Layers();

        Attack();

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

    private void Jump(){
        if(Input.GetKeyDown(KeyCode.Space) && IsGrounded()){
            corpo.AddForce(new Vector2(0, 7.0f), ForceMode2D.Impulse);
            anime.SetTrigger("Jump");
        }
    }
   
    private void Attack(){
        if(Input.GetKeyDown(KeyCode.L) && IsGrounded()){
            anime.SetTrigger("Attack");
            corpo.linearVelocity = Vector2.zero; // Para o personagem no momento do ataque
        }

    }

    private bool IsGrounded(){
    // Physics2D.OverlapCircle direto retorna true se encostar na camada certa, sem precisar de 'for'
        anime.SetBool("Falling", false);
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }

    private void Layers(){

        if(!IsGrounded()){
            anime.SetLayerWeight(1, 1);
        } else {
            anime.SetLayerWeight(1, 0);
        }
        
    }
}   


