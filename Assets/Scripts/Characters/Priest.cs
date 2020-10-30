using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Priest : MonoBehaviour
{
    private int health = 1000;
    private int mana = 1000;

    public int smallHeal() 
    {
        mana -= 5;
        return 15;
    }

    public int BigHeal() 
    {
        mana -= 8;
        return 20;
    }

    public void regenerate() 
    {
        if (mana < 1000)
        {
            if (mana > 998)
            {
                mana = 1000;
            }
            else 
            { 
                mana += 2; 
            }
        }
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
