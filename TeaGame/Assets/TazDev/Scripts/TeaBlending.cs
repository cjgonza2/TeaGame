using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaBlending : MonoBehaviour
{
    #region Objects
    public GameObject mildBase;
    public GameObject sweetBase;
    public GameObject bitterBase;

    public GameObject sleepInfuse;
    public GameObject healthInfuse;
    public GameObject energyInfuse;
    #endregion

    #region Bools

    //Bases check
    public bool mild = false;
    public bool sweet = false;
    public bool bitter = false;

    //Infusions check
    public bool sleep = false;
    public bool health = false;
    public bool energy = false;

    //Flavors
    public bool mildSleep = false;
    public bool mildHealth = false;
    public bool mildEnergy = false;

    public bool sweetSleep = false;
    public bool sweetHealth = false;
    public bool sweetEnergy = false;

    public bool bitterSleep = false;
    public bool bitterHealth = false;
    public bool bitterEnergy = false;



    #endregion

    #region Values
    public int teaBase = 0;
    public int teaIng = 0;

    #endregion




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(teaBase == 1 && teaIng == 1)
        {
            if (mild)
            {
                if (sleep)
                {
                    mildSleep = true;
                }
                if (health)
                {
                    mildHealth = true;
                }
                if (energy)
                {
                    mildEnergy = true;
                }
            }

            if (bitter)
            {
                if (sleep)
                {
                    bitterSleep = true;
                }
                if (health)
                {
                    bitterHealth = true;
                }
                if (energy)
                {
                    bitterEnergy = true;
                }
            }

            if (sweet)
            {
                if (sleep)
                {
                    sweetSleep = true;
                }
                if (health)
                {
                    sweetHealth = true;
                }
                if (energy)
                {
                    sweetEnergy = true;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("mild") == true && teaBase == 0)
        {
            mild = true;
            teaBase++;
        }
        if (collision.gameObject.tag.Equals("bitter") == true && teaBase == 0)
        {
            bitter = true;
            teaBase++;
        }
        if (collision.gameObject.tag.Equals("sweet") == true && teaBase == 0)
        {
            sweet = true;
            teaBase++;
        }
        if (collision.gameObject.tag.Equals("sleep") == true && teaIng == 0)
        {
            sleep = true;
            teaIng++;
        }
        if (collision.gameObject.tag.Equals("health") == true && teaIng == 0)
        {
            health = true;
            teaIng++;
        }
        if (collision.gameObject.tag.Equals("energy") == true && teaIng == 0)
        {
            energy = true;
            teaIng++;
        }
    }
}
