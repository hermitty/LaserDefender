using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour {

    public void LoadGameOver()
    {
        StartCoroutine(SlowDown());
    }
    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game");

    }
    public void StartMenu()
    {
        SceneManager.LoadScene("Menu");

    }
    public void QuitGame()
    {
        Application.Quit();
    }
    IEnumerator SlowDown()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Game Over");

    }

}
