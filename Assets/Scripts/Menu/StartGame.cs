using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void StartAction()
    {
        Debug.Log("fff");
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}
