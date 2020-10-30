using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : MonoBehaviour
{
    private int health = 1000;
    System.Random rand = new System.Random();

    public int attack() 
    {
        return rand.Next(1, 30);
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

    public void addHealth(int amount)
    {
        if ((health += amount) > 1000)
        {
            health = 1000;
        }
        else if (health < 1000)
        {
            health += amount;
        }
    }
}
