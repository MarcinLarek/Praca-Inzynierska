using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconClickHandler : MonoBehaviour
{
    private void OnMouseDown()
    {
        CrewManager.GetInstance().activeCharacter = this.gameObject;
    }

}
