using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float speed = 3f;

    private Rigidbody2D rb;
    private Vector2 target;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        target = pointA.position;
    }

    void FixedUpdate()
    {
        Vector2 newPos = Vector2.MoveTowards(
            rb.position,
            target,
            speed * Time.fixedDeltaTime
        );

        rb.MovePosition(newPos);

        if (Vector2.Distance(rb.position, pointA.position) < 0.1f)
        {
            target = pointB.position;
        }
        else if (Vector2.Distance(rb.position, pointB.position) < 0.1f)
        {
            target = pointA.position;
        }
    }
}