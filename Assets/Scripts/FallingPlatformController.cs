using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FallingPlatformController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Invoke("Trigger", 0.5f);
        }
        else if (collision.gameObject.layer == 6)
        {
            Destroy(gameObject);
        }
    }

    private void Trigger()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        anim.SetTrigger("Off");
    }
}
