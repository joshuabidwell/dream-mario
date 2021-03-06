﻿using System.Collections;
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
    public Tilemap tilemapCoins;

    [Header("Audio")]
    public AudioSource music;
    public AudioSource jumpSound;
    public AudioSource coinSound;
    public AudioSource deathSound;
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
            anim.ResetTrigger("Grounded"); // TRY anim.SetBool(anim.SetBool("Grounded", _grounded);
            _grounded = false; // TRY setting this on collision with block where block is lower than player pivot, ensure all sprites are pivot at bottom in that case
        }

    }

    private void Jump()
    {
        if (_grounded == true)
        {
            jumpSound.Play();
            rb.AddForce(new Vector2(0, 18), ForceMode2D.Impulse);
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
            rb.velocity = Vector2.zero;
            endGame.End();
        }
        if (col.CompareTag("Coins"))
        {
            coinSound.Play();
            Destroy(col.gameObject);
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
                    coinSound.Play();
                    hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                    hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
                    tilemap.SetTile(tilemap.WorldToCell(hitPosition), null);
                    tilemap.SetColliderType(tilemap.WorldToCell(hitPosition), Tile.ColliderType.Grid);
                    tilemapQb.SetColliderType(tilemap.WorldToCell(hitPosition), Tile.ColliderType.None);
                    GameObject cointemp = Instantiate(coin, (hitPosition + new Vector3(0, 1, 0)), Quaternion.identity);// This coin should be the spinning one in the objects spritesheet
                    cointemp.AddComponent<Rigidbody2D>().AddForce(new Vector2(0, 6), ForceMode2D.Impulse);// Just for optimisation's interest this is where you should just lerp the transform
                    Destroy(cointemp, 0.35f);
                }
            }
        }
        if (collision.gameObject.CompareTag("Goomba"))
        {
            foreach (ContactPoint2D hit in collision.contacts)
            {
                Debug.Log(hit.normal);
                if (Vector3.Dot(hit.normal, Vector3.up) > 0.1f || Vector3.Dot(hit.normal, Vector3.up) < 0)
                {
                    collision.gameObject.GetComponent<AudioSource>().Play();
                   collision.gameObject.GetComponent<Animator>().SetBool("Dead", true);
                    collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    collision.gameObject.GetComponent<Goomba>().enabled = false;
                    Destroy(collision.gameObject, 1);
                    rb.AddForce(new Vector2(0, 6.5f), ForceMode2D.Impulse);
                }
                else
                {
                    anim.SetTrigger("Dead");
                    StartCoroutine("Death");
                }
            }
        }
    }

    public IEnumerator Death()
    {
        if (_dead == false)
        {
            music.Stop();
            deathSound.Play();
            _dead = true;
            Destroy(rb);
            this.enabled = false;
            rb.isKinematic = true;
            yield return new WaitForSeconds(0.2f);
            LeanTween.move(this.gameObject, new Vector2(transform.position.x, (transform.position.y + 8)), 0.4f);
            yield return new WaitForSeconds(0.6f);
            LeanTween.move(this.gameObject, new Vector2(transform.position.x, (transform.position.y - 20)), 1f);
            yield return new WaitForSeconds(2.4f);
            SceneManager.LoadScene("Game");
        }
    }
}
