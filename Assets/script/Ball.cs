using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public float speed = 12f; // aumentei a velocidade

    void Start()
    {
        // A bola só desaparece após 8 segundos
        Destroy(gameObject, 4f);
    }

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player_movement player = collision.GetComponent<player_movement>();

            if (player != null)
            {
                player.TakeDamage();
            }
        }

        // Não destruir ao bater em triggers irrelevantes
        if (!collision.CompareTag("Player"))
            return;

        Destroy(gameObject);
    }
}