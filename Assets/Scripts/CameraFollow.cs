using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Alvo para Seguir")]
    public Transform alvo;

    [Header("Configuração de Suavidade")]
    [Range(0f, 1f)]
    public float suavidade = 0.125f;

    [Header("Controle de Movimento")]
    public bool seguirNoEixoX = true;
    public bool seguirNoEixoY = false; // Começa falso para resolver o seu problema!

    [Header("Limites do Cenário (Se o Y estiver ativo)")]
    public bool usarLimites = false;
    public float minX, maxX;
    public float minY, maxY;

    private float yInicial;

    void Start()
    {
        // Guarda a altura exata que você deixou a câmera no editor
        yInicial = transform.position.y;
    }

    void LateUpdate()
    {
        if (alvo == null) return;

        // Se 'seguirNoEixoX' for true, segue o player. Se não, fica parada no X atual.
        float posX = seguirNoEixoX ? alvo.position.x : transform.position.x;

        // Se 'seguirNoEixoY' for true, segue o pulo do player. Se não, fica travada na altura inicial.
        float posY = seguirNoEixoY ? alvo.position.y : yInicial;

        // Se você ativar os limites, ela não passa dos valores estipulados
        if (usarLimites)
        {
            posX = Mathf.Clamp(posX, minX, maxX);
            posY = Mathf.Clamp(posY, minY, maxY);
        }

        // Monta a posição final e aplica a suavidade
        Vector3 posicaoDesejada = new Vector3(posX, posY, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, posicaoDesejada, suavidade);
    }
}