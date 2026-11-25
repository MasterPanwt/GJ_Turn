using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameBotton : MonoBehaviour
{
    [Tooltip("填入第一个轮回场景")]
    public string firstTurn ;
    public void StartGame()
    {
        SceneLoader.Instance.TransitionToScene(firstTurn);

    }

}
