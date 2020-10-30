using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private int health = 4500;
    System.Random rand = new System.Random();

    public int attack(string type) 
    {
        if (type == "Warrior") 
        {
            return rand.Next(45, 55);
        }

        return rand.Next(5, 20);
    }

    public int getHealth() 
    {
        return health;
    }

    public void damageHealth(int amount) 
    {
        health -= amount;

        if (health < 0) 
        {
            health = 0;
        }
    }
}
