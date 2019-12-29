using UnityEngine;

public class Enemy : Character
{
    private enum AIState { Standby, Wander, Avoid, Dead }

    [Space(10)]
    public GameObject target;
    [Space(10)]
    public Collider2D collider;
    public float BackGroundMoveSpeed;
    [Space(10)]
    public float WanderTime, AvoidRange, AvoidTime;

    private float StateTimer;
    private AIState state;
    private Vector2 direction;
    private bool addScore;

    protected override void ChildStart()
    {
        state = AIState.Standby;
        addScore = false;
    }

    protected override void ChildUpdate()
    {
        TimerUpdate();

        switch (state)
        {
            case AIState.Standby:
                Standby();
                break;
            case AIState.Wander:
                Wandering();
                break;
            case AIState.Avoid:
                Avoiding();
                break;
            case AIState.Dead:
                Dead();
                break;
        }
    }

    private void OnDrawGizmos()
    {
        switch (state)
        {
            case AIState.Standby:
                break;
            case AIState.Wander:
                Gizmos.color = Color.green;
                break;
            case AIState.Avoid:
                Gizmos.color = Color.red;
                break;
            case AIState.Dead:
                Gizmos.color = Color.gray;
                break;
        }
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)direction);
    }

    #region AIState & TimerUpdate
    private void TimerUpdate()
    {
        if (StateTimer > 0)
            StateTimer -= Time.deltaTime;
    }

    private void Standby()
    {
        rigidbody2D.MovePosition(rigidbody2D.position + (Vector2.left * BackGroundMoveSpeed * Time.deltaTime));
    }

    private void Dead()
    {
        #region StateUpdate
        animator.SetTrigger("Dead");
        collider.isTrigger = true;
        rigidbody2D.MovePosition(rigidbody2D.position + Vector2.left * BackGroundMoveSpeed * Time.deltaTime);
        if(!addScore)
        {
            addScore = true;
            FindObjectOfType<GameManager>().AddScore(10);
        }
        #endregion
    }

    private void Avoiding()
    {
        #region StateUpdate
        if (fireTimer <= 0)
            Fire();

        Vector3 temp = target.transform.position - transform.position;
        if (temp.x > 0)
            rigidbody2D.AddForce(Vector2.down * (Speed * 2) * Time.deltaTime);
        else
            rigidbody2D.AddForce(Vector2.up * (Speed * 2) * Time.deltaTime);

        if (temp.y > 0)
            rigidbody2D.AddForce(Vector2.right * (Speed * 2) * Time.deltaTime);
        else
            rigidbody2D.AddForce(Vector2.left * (Speed * 2) * Time.deltaTime);
        #endregion

        #region StateControll
        if (Health <= 0)
            state = AIState.Dead;

        if (StateTimer <= 0)
        {
            if (Vector3.Distance(target.transform.position, transform.position) < AvoidRange)
            {
                StateTimer = AvoidTime;
            }
            else
            {
                StateTimer = WanderTime;
                direction = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
                state = AIState.Wander;
            }
        }
        #endregion
    }

    private void Wandering()
    {
        #region StateUpdate
        if (fireTimer <= 0)
            Fire();

        rigidbody2D.AddForce(direction * Speed * Time.deltaTime);
        #endregion

        #region StateControll
        if (Health <= 0)
            state = AIState.Dead;

        if (StateTimer <= 0)
        {
            if (Vector3.Distance(target.transform.position, transform.position) < AvoidRange)
            {
                StateTimer = AvoidTime;
                state = AIState.Avoid;
            }
            else
            {
                StateTimer = WanderTime;
                direction = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
            }
        }
        #endregion
    }
    #endregion

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Boundary")
        {
            collider.isTrigger = false;
            direction = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
            StateTimer = WanderTime;
            state = AIState.Wander;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Boundary" || collision.gameObject.tag == "MoveBody")
        {
            direction = ((Vector2)transform.position - collision.GetContact(0).point).normalized;
            StateTimer = WanderTime;
            state = AIState.Wander;
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
