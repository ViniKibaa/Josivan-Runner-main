using UnityEngine;

public class EnemyAIBoss : MonoBehaviour
{
    [Header("Patrulha")]
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float patrolSpeed = 2f;

    [Header("Perseguição")]
    [SerializeField] private Transform player;
    [SerializeField] private float chaseSpeed = 4f;
    [SerializeField] private float detectionRange = 2f;

    private Rigidbody2D rb;
    private Vector2 target;
    private Vector3 initialPosition;

    private enum State
    {
        Patrol,
        Chase
    }

    private State currentState;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        initialPosition = transform.position;

        currentState = State.Patrol;
        target = pointB.position;
    }

    void FixedUpdate()
    {
        float distanceToPlayer = Vector2.Distance(
            transform.position,
            player.position
        );

        float maxPatrolDistance = Vector2.Distance(
            pointA.position,
            pointB.position
        ) / 2f + 1f;

        float distanceFromCenter = Vector2.Distance(
            transform.position,
            (pointA.position + pointB.position) / 2f
        );

        if (distanceFromCenter > maxPatrolDistance)
        {
            currentState = State.Patrol;
        }
        else if (distanceToPlayer <= detectionRange)
        {
            currentState = State.Chase;
        }
        else
        {
            currentState = State.Patrol;
        }

        switch (currentState)
        {
            case State.Patrol:
                Patrol();
                break;

            case State.Chase:
                Chase();
                break;
        }
    }

    void Patrol()
    {
        if (Vector2.Distance(rb.position, target) < 0.1f)
        {
            if (target == (Vector2)pointA.position)
                target = pointB.position;
            else
                target = pointA.position;
        }

        Vector2 newPos = Vector2.MoveTowards(
            rb.position,
            target,
            patrolSpeed * Time.fixedDeltaTime
        );

        rb.MovePosition(newPos);

        UpdateFacing(target.x);
    }

    void Chase()
    {
        Vector2 newPos = Vector2.MoveTowards(
            rb.position,
            player.position,
            chaseSpeed * Time.fixedDeltaTime
        );

        rb.MovePosition(newPos);

        UpdateFacing(player.position.x);
    }

    void UpdateFacing(float targetX)
    {
        if (targetX > transform.position.x)
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

    public void ResetBoss()
    {
        transform.position = initialPosition;

        currentState = State.Patrol;

        target = pointB.position;

        rb.linearVelocity = Vector2.zero;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}