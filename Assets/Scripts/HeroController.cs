using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class HeroController : MonoBehaviour
{
    public float maxSpeed;
    bool facingRight = true;
    public Rigidbody2D rb;
    public Animator anim;
    public GameObject coin;
    public EndGame endGame;
    public Tilemap tilemap;
    public Tilemap tilemapQb;
    #region bool
    public bool _grounded;
    public bool _dead;
    #endregion


    private void Update()
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

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Death"))
        {
            anim.SetTrigger("Dead");
            StartCoroutine("Death");
        }
        if (col.CompareTag("End Game Trigger"))
        {
            anim.SetTrigger("Grounded");
            this.enabled = false;
            anim.SetFloat("Speed", Mathf.Abs(0));
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 hitPosition = Vector3.zero;
        if (tilemap != null && collision.gameObject.CompareTag("QuestionBlocks"))
        {
            foreach (ContactPoint2D hit in collision.contacts)
            {
                if (Vector3.Dot(hit.normal, Vector3.up) == -1)
                {
                    Debug.Log(Vector3.Dot(hit.normal, Vector3.up));
                    hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                    hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
                    tilemap.SetTile(tilemap.WorldToCell(hitPosition), null);
                    tilemap.SetColliderType(tilemap.WorldToCell(hitPosition), Tile.ColliderType.Grid);
                    tilemapQb.SetColliderType(tilemap.WorldToCell(hitPosition), Tile.ColliderType.None);
                    GameObject cointemp = Instantiate(coin, (hitPosition + new Vector3(0, 1, 0)), Quaternion.identity);
                    cointemp.AddComponent<Rigidbody2D>().AddForce(new Vector2(0, 6), ForceMode2D.Impulse);
                    Destroy(cointemp, 0.35f);
                }
            }
        }
    }

    public IEnumerator Death()
    {
        if (_dead == false)
        {
            _dead = true;
            this.enabled = false;
            rb.AddForce(new Vector2(0, 30), ForceMode2D.Impulse);
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene("Game");
        }
    }
}
