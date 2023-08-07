using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconClickHandlerUpgrader : MonoBehaviour
{
    private void OnMouseDown()
    {
        UpgradeManager.GetInstance().activeCharacter = this.gameObject;
        UpgradeManager.GetInstance().LoadCharacterPortrait();
    }
}
