using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchInventory : MonoBehaviour
{
    public GameObject blackBackground;
    public GameObject inventory;

    public void ToggleVisibility()
    {
        if(inventory.activeSelf)
        {
            inventory.SetActive(false);
            blackBackground.SetActive(false);
        }
        else
        {
            inventory.SetActive(true);
            blackBackground.SetActive(true);
        }
    }
}
