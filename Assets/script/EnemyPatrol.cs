using UnityEngine;

public class EnemyPatrol : MonoBehaviour
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

        UpdateFacingDirection();
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

            UpdateFacingDirection();
        }
        else if (Vector2.Distance(rb.position, pointB.position) < 0.1f)
        {
            target = pointA.position;

            UpdateFacingDirection();
        }
    }

    private void UpdateFacingDirection()
    {
        if (target == (Vector2)pointB.position)
        {
            transform.localScale = new Vector3(
                Mathf.Abs(transform.localScale.x),
                transform.localScale.y,
                transform.localScale.z
            );
        }
        else
        {
            transform.localScale = new Vector3(
                -Mathf.Abs(transform.localScale.x),
                transform.localScale.y,
                transform.localScale.z
            );
        }
    }
}