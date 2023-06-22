using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    private Animator anim;
    private BoxCollider2D bc;

    [SerializeField] private AudioSource CollectSound;

    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CollectSound.Play();
            anim.SetTrigger("Collected");
            bc.enabled = false;
        }
    }

    private void Disappear()
    {
        Destroy(gameObject);
    }
}
