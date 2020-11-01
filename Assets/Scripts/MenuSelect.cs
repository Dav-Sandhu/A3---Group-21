using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSelect : MonoBehaviour
{
    public void Scores()
    {
        SceneManager.LoadScene("Scores");
    }

    public void Level_1()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void Level_2()
    {
        SceneManager.LoadScene("Level 2");
    }

    public void Level_3()
    {
        SceneManager.LoadScene("Level 3");
    }
}
