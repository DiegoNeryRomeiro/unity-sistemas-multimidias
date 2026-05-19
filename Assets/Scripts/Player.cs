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

    void Update()
    {   
        // 1. Salvamos o estado do chão nesta variável para usar em todo o Update
        bool noChao = IsGrounded();

        // Passamos a variável 'noChao' para o Pulo saber se pode pular
        Jump(noChao);

        // Pega o input
        float horizontal = Input.GetAxis("Horizontal");

        // Processa o input
        flip(horizontal);
        corpo.linearVelocity = new Vector2(horizontal * velocidade, corpo.linearVelocity.y);
        anime.SetFloat("speed", Mathf.Abs(horizontal));

        // 2. CONTROLE DA QUEDA: Ativa/Desativa o bool sem nenhuma outra função atropelar
        if (!noChao && corpo.linearVelocity.y < -0.1f)
        {
            anime.SetBool("Falling", true);
        }
        else if (noChao)
        {
            anime.SetBool("Falling", false);
        }

        // Passamos o 'noChao' para atualizar os pesos das camadas e o ataque
        Layers(noChao);
        Attack(noChao);
    }

    private void flip(float horizontal)
    {
        if (horizontal > 0)       sprite.flipX = false;
        else if (horizontal < 0)  sprite.flipX = true;
    }

    // Atualizado para receber o estado do chão
    private void Jump(bool noChao)
    {
        if (Input.GetKeyDown(KeyCode.Space) && noChao)
        {
            corpo.AddForce(new Vector2(0, 7.0f), ForceMode2D.Impulse);
            anime.SetTrigger("Jump");
        }
    }
   
    // Atualizado para receber o estado do chão
    private void Attack(bool noChao)
    {
        if (Input.GetKeyDown(KeyCode.L) && noChao)
        {
            anime.SetTrigger("Attacking");
            corpo.linearVelocity = Vector2.zero; // Para o personagem no momento do ataque
        }
    }

    // LIMPO: Agora ela só checa o chão de verdade, sem efeitos colaterais
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }

    // Atualizado para usar a variável do frame de forma segura
    private void Layers(bool noChao)
    {
        if (!noChao)
        {
            anime.SetLayerWeight(1, 1f); // Ativa a Air_Layer (Peso 1)
        } 
        else 
        {
            anime.SetLayerWeight(1, 0f); // Desativa a Air_Layer (Peso 0) e volta pra Base
        }
    }
}