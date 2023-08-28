using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrphanBeGoner : MonoBehaviour
{
    //TYLKO JEDNA OPCJA POWINNA BYC ZAZNACZONA
    public bool character;
    public bool item;
    public bool mission;

    private void Awake()
    {
        FindYourParent();
    }

    private void FindYourParent()
    {
        GameObject gameHandler = GameObject.Find("GameHandler");

        if (character)
        {
            gameHandler = GameObject.Find("GameHandler/Characters");
        }
        else if (item)
        {
            gameHandler = GameObject.Find("GameHandler/Items");
        }
        else if (mission)
        {
            gameHandler = GameObject.Find("GameHandler/Missions");
        }

        this.transform.SetParent(gameHandler.transform);
    }
}
