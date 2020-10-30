using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class Scores : MonoBehaviour
{
    public GameObject textObject;
    string filePath = "Assets/Files/totalDamage.csv";

    void Start()
    {
        if (!File.Exists(filePath)) 
        {
            string[] createText = { "Level 1,0,0", "Level 2,0,0", "Level 3,0,0" };
            File.WriteAllLines(filePath, createText);
        }

        string[] fileLines = File.ReadAllLines(filePath);

        foreach (string line in fileLines) 
        {
            string[] words = line.Split(',');

            textObject.GetComponent<Text>().text += words[0] + "\n";
            textObject.GetComponent<Text>().text += "The Max Total Damage done by the party to the boss is " + words[1] + "\n";
            textObject.GetComponent<Text>().text += "The Max Total Damage done by the boss to the party is " + words[2] + "\n";
        }
    }

    public void returntoMenu() 
    {
        SceneManager.LoadScene("Main Menu");
    }
}
