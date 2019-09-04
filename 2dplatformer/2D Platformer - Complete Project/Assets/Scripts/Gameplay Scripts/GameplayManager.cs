using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    private GameObject[] enemies;

    private bool levelUp;

    public string loadScene;

    int level;

    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if(enemies.Length < 1 && !levelUp)
        {
            levelUp = true;
            SceneManager.LoadScene(loadScene);
        }
    }
}
