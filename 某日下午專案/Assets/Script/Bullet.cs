using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rigidbody2D;
    public float BulletSpeed;
    public string Target;

    private bool isHit;
    private float destroyTimer;

    private void Start()
    {
        isHit = false;
        destroyTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody2D.MovePosition(rigidbody2D.position + Vector2.right * BulletSpeed * Time.deltaTime);

        if(isHit)
        {
            destroyTimer -= Time.deltaTime;

            if(destroyTimer < 0)
                Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isHit && collision.gameObject.tag == Target)
        {
            collision.gameObject.GetComponentInParent<Character>().Hit();

            isHit = true;
            destroyTimer = 0.5f;

            animator.SetTrigger("Ex");
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
