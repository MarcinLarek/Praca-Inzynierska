using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextMenuHandler : MonoBehaviour
{
    // Dodaj funkcje, kt�re zostan� wywo�ane po wybraniu poszczeg�lnych opcji z menu
    public void ModifyObjectOption()
    {
        // Tutaj umie�� kod modyfikuj�cy obiekt
    }

    public void DeleteObjectOption()
    {
        // Tutaj umie�� kod usuwaj�cy obiekt
        Destroy(gameObject);
    }
}