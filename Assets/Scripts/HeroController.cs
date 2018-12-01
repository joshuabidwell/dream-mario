using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HeroController : MonoBehaviour
{
    public float maxSpeed;
    bool facingRight = true;
    public Rigidbody2D rb;
    public Animator anim;
    public Tilemap tilemap;
    public bool _grounded;

    void Start ()
    {

	}

    private void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(move));
        rb.velocity = new Vector2(move * maxSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (move > 0 && !facingRight)
        {
            Flip();
        }
        else if (move < 0 && facingRight)
        {
            Flip();
        }
        if (rb.velocity.y == 0)
        {
            anim.SetTrigger("Grounded");
            _grounded = true;
        }
        else if (rb.velocity.y > 1)
        {
            anim.ResetTrigger("Grounded");
            _grounded = false;
        }

    }

    private void Jump()
    {
        if (_grounded == true)
        {
            rb.AddForce(new Vector2(0, 20), ForceMode2D.Impulse);
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

    }
}
