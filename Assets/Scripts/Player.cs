using UnityEngine;

public class Player : MonoBehaviour
{
    
    private Rigidbody2D corpo;
    public float velocidade = 3.0f;
    void Start()
    {
        corpo = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        //Debug.Log(horizontal);

        corpo.linearVelocity = new Vector2(horizontal * velocidade, corpo.linearVelocity.y);
    }
}
