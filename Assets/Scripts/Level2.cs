using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;


public class Level2 : MonoBehaviour
{
    public GameObject menuBtn;
    public GameObject textObject;
    public GameObject boss;
    public GameObject druid;
    public GameObject mage;
    public GameObject priest;
    public GameObject rogue;
    public GameObject warrior;

    private int totalBossDamage;
    private int totalPartyDamage;
    private int curDmg;

    string Line1;
    string Line2;
    string Line3;                                               

    public bool gameLoop;

    string filePath = "Assets/Files/totalDamage.csv";
    string filePath2 = "Assets/Files/damageLog.csv";

    string finalLog;                                            
    int timestep;                                               

    System.Random rand = new System.Random();

    void Start()
    {
        menuBtn.SetActive(false);                               

        totalBossDamage = 0;
        totalPartyDamage = 0;

        timestep = 0;

        gameLoop = true;

        if (!File.Exists(filePath2))                            
        {
            string[] tempText = {""};
            File.WriteAllLines(filePath2, tempText);
        }


        //InvokeRepeating("loop", 0.0f, 0.3f);
    }

    void Update()
    {
        if (gameLoop)
        {
            curDmg = addDamage(boss, boss.GetComponent<Boss>().attack("Rogue"));                
            rogue.GetComponent<Rogue>().damageHealth(curDmg);
            checkDeath(rogue.GetComponent<Rogue>().getHealth());

            curDmg = addDamage(boss, boss.GetComponent<Boss>().attack("Mage"));
            mage.GetComponent<Mage>().damageHealth(curDmg);
            checkDeath(mage.GetComponent<Mage>().getHealth());

            curDmg = addDamage(boss, boss.GetComponent<Boss>().attack("Druid"));
            druid.GetComponent<Druid>().damageHealth(curDmg);
            checkDeath(druid.GetComponent<Druid>().getHealth());

            curDmg = addDamage(boss, boss.GetComponent<Boss>().attack("Priest"));
            priest.GetComponent<Priest>().damageHealth(curDmg);
            checkDeath(priest.GetComponent<Priest>().getHealth());

            curDmg = addDamage(boss, boss.GetComponent<Boss>().attack("Warrior"));                  
            warrior.GetComponent<Warrior>().damageHealth(curDmg);
            checkDeath(warrior.GetComponent<Warrior>().getHealth());
            tankHeal(warrior.GetComponent<Warrior>().getHealth()); //Level 2 specific

            curDmg = addDamage(warrior, warrior.GetComponent<Warrior>().attack());
            curDmg = addDamage(rogue, rogue.GetComponent<Rogue>().attack());
            curDmg = addDamage(mage, mage.GetComponent<Mage>().attack());
            curDmg = addDamage(druid, druid.GetComponent<Druid>().attack());

            healParty(priest.GetComponent<Priest>().smallHeal(true));

            warrior.GetComponent<Warrior>().addHealth(priest.GetComponent<Priest>().BigHeal(true));
            priest.GetComponent<Priest>().regenerate();

            string p1 = boss.GetComponent<Boss>().getHealth().ToString();
            string p2 = warrior.GetComponent<Warrior>().getHealth().ToString();
            string p3 = rogue.GetComponent<Rogue>().getHealth().ToString();
            string p4 = mage.GetComponent<Mage>().getHealth().ToString();
            string p5 = druid.GetComponent<Druid>().getHealth().ToString();
            string p6 = priest.GetComponent<Priest>().getHealth().ToString();

            finalLog += timestep.ToString() + "," + p1 + "," + p2 + "," + p3 + "," + p4 + "," + p5 + "," + p6 + '\n';
            timestep += 1;

            display();
        }
    }


    //Level 2 specific
    //DEBUG THE RANDOM INT NOT WORKING
    void tankHeal(int health)
    {
        if(health <= 1500)
        {
            int dice = rand.Next(0, 2);
            Debug.Log("Dice: " + dice);
            if(dice == 0)
            {
                Debug.Log("SMALL EMERGENCY HEAL @" + warrior.GetComponent<Warrior>().getHealth());
                warrior.GetComponent<Warrior>().addHealth(priest.GetComponent<Priest>().smallHeal(false));
                
            }
            else if(dice == 1)
            {
                Debug.Log("BIG EMERGENCY HEAL @" + warrior.GetComponent<Warrior>().getHealth());
                warrior.GetComponent<Warrior>().addHealth(priest.GetComponent<Priest>().BigHeal(false));
               
            }

            
        }
    }

    


    void checkDeath(int health)
    {
        if (health <= 0)
        {
            gameLoop = false;
            display();
            menuBtn.SetActive(true);

            if (!File.Exists(filePath))
            {
                string[] createText = { "Level 1,0,0", "Level 2,0,0", "Level 3,0,0" };
                File.WriteAllLines(filePath, createText);
            }

            string[] fileLines = File.ReadAllLines(filePath);

            foreach (string line in fileLines)
            {
                string[] words = line.Split(',');

                if (words[0] == "Level 2")
                {
                    string val1 = words[1];
                    string val2 = words[2];

                    if (totalPartyDamage > int.Parse(words[1]))
                    {
                        val1 = totalPartyDamage.ToString();
                    }

                    if (totalBossDamage > int.Parse(words[2]))
                    {
                        val2 = totalBossDamage.ToString();
                    }

                    Line2 = "Level 2," + val1 + ',' + val2;
                }
                else if (words[0] == "Level 1")
                {
                    Line1 = line;
                }
                else if (words[0] == "Level 3") 
                {
                    Line3 = line;               
                }
            }

            string strout = Line1 + '\n' + Line2 + '\n' + Line3;
            File.WriteAllText(filePath, strout);
            File.WriteAllText(filePath2, finalLog);
        }
    }
    void display() 
    {
        textObject.GetComponent<Text>().text = "Boss Health: " + boss.GetComponent<Boss>().getHealth() + '\n';
        textObject.GetComponent<Text>().text += "Warrior Health: " + warrior.GetComponent<Warrior>().getHealth() + '\n';
        textObject.GetComponent<Text>().text += "Priest Health: " + priest.GetComponent<Priest>().getHealth() + '\n';
        textObject.GetComponent<Text>().text += "Druid Health: " + druid.GetComponent<Druid>().getHealth() + '\n';
        textObject.GetComponent<Text>().text += "Rogue Health: " + rogue.GetComponent<Rogue>().getHealth() + '\n';
        textObject.GetComponent<Text>().text += "Mage Health: " + mage.GetComponent<Mage>().getHealth() + '\n';

        textObject.GetComponent<Text>().text += "Total Boss Damage Against Party: " + totalBossDamage + '\n';
        textObject.GetComponent<Text>().text += "Total Party Damage Against Boss: " + totalPartyDamage + '\n';
    }

    int addDamage(GameObject o, int dmg)
    {
        if (o == boss)
        {
            totalBossDamage += dmg;
        }
        else
        {
            totalPartyDamage += dmg;
            boss.GetComponent<Boss>().damageHealth(dmg);
            checkDeath(boss.GetComponent<Boss>().getHealth());
        }

        return dmg;
    }

    void healParty(int amount)
    {
        int dice = rand.Next(0, 5);
        
        if (dice == 0 || dice == 1)
        {
            priest.GetComponent<Priest>().addHealth(amount);
        }
        else if (dice == 2)
        {
            rogue.GetComponent<Rogue>().addHealth(amount);
        }
        else if (dice == 3)
        {
            mage.GetComponent<Mage>().addHealth(amount);
        }
        else if (dice == 4) 
        {
            druid.GetComponent<Druid>().addHealth(amount);
        }
    }

    public void returntoMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}