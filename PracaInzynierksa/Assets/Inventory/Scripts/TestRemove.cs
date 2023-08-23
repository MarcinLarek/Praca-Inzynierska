using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextMenuHandler : MonoBehaviour
{
    // Dodaj funkcje, które zostan¹ wywo³ane po wybraniu poszczególnych opcji z menu
    public void ModifyObjectOption()
    {
        // Tutaj umieœæ kod modyfikuj¹cy obiekt
    }

    public void DeleteObjectOption()
    {
        // Tutaj umieœæ kod usuwaj¹cy obiekt
        Destroy(gameObject);
    }
}