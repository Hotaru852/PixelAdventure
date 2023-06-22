using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private AudioSource FinishSound;
    [SerializeField] private PlayerController Player;
    [SerializeField] private int AvailableCollectibles;
    [SerializeField] private Animator SceneAnimator;

    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Player.CollectedCollectibles == AvailableCollectibles) 
        {
            FinishSound.Play();
            Invoke("Transition", 1f);
        }
    }

    private void Transition()
    {
        SceneAnimator.SetTrigger("Change");
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
