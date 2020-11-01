using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : MonoBehaviour
{
    private int health = 3000;

    public int attack() 
    {
        return 5;
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
        if ((health += amount) > 3000)
        {
            health = 3000;
        }
        else if (health < 3000)
        {
            health += amount;
        }
    }
}
