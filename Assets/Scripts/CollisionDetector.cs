using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    [SerializeField] private Collider2D Collider;
    [SerializeField] private GameObject Player;
 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.otherCollider == Collider)
        {
            Player.GetComponent<AudioSource>().Play();
            Player.GetComponent<Animator>().SetTrigger("Dead");
            Player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }
}
