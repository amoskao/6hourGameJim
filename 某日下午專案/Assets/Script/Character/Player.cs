using UnityEngine;

public class Player : Character
{
    protected override void ChildUpdate()
    {
        if (Input.GetKey(KeyCode.UpArrow))
            rigidbody2D.AddForce(Speed * Vector2.up * Time.deltaTime);
        else if (Input.GetKey(KeyCode.DownArrow))
            rigidbody2D.AddForce(Speed * Vector2.down * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftArrow))
            rigidbody2D.AddForce(Speed * Vector2.left * Time.deltaTime);
        else if (Input.GetKey(KeyCode.RightArrow))
            rigidbody2D.AddForce(Speed * Vector2.right * Time.deltaTime);

        if (Input.GetKey(KeyCode.Space) && fireTimer <= 0)
        {
            Fire();
        }
    }

    protected override void ChildHit()
    {
        FindObjectOfType<GameManager>().AddScore(-5);
    }
}
