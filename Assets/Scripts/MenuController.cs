using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Animator SceneAnim;

    public void StartGame()
    {
        Invoke("Transition", 1f);
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        EditorApplication.ExecuteMenuItem("Edit/Play");
    }

    private void Transition()
    {
        SceneAnim.SetTrigger("Change");
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
