using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rigidbody2D;
    public GameObject bullet, bulletPosition;
    [Space(10)]
    public int Health;
    public float Speed, FireRate;
    [Space(10)]
    public SpriteRenderer spriteRenderer;
    [ColorUsage(true)]
    public Color HitColor, MyColor;
    public float HitColorChangeSpeed;

    protected float fireTimer;
    private float ColorChangeTimer;

    // Start is called before the first frame update
    void Start()
    {
        ChildStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (fireTimer > 0)
            fireTimer -= Time.deltaTime;

        if (ColorChangeTimer > 0)
            ColorChangeTimer -= Time.deltaTime;
        else
            spriteRenderer.color = MyColor;

        ChildUpdate();
    }

    protected virtual void ChildUpdate() { }
    protected virtual void ChildStart() { }
    protected virtual void ChildHit() { }

    protected void Fire()
    {
        animator.SetTrigger("Attack");
        Instantiate(bullet, bulletPosition.transform.position, bulletPosition.transform.rotation);
        fireTimer = FireRate;
    }

    public void Hit()
    {
        Health--;
        ColorChangeTimer = HitColorChangeSpeed;
        spriteRenderer.color = HitColor;
        ChildHit();
    }
}
