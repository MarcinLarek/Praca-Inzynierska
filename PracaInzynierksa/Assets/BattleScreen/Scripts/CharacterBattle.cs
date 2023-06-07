using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBattle : MonoBehaviour
{
    private CharacterBase characterBase;

    private void Awake()
    {
        characterBase = GetComponent<CharacterBase>();
    }

    //W tej funkcji ustawiamy wyglad spritow w odpowiednich druzynach. Animacje, odwrocenie, wyglad, tekstury itp.
    //Pozniej sie to oczywiscie bardziej rozbuduje.
    public void Setup(bool isPlayerTeam)
    {
        if (isPlayerTeam)
        {
            characterBase.ChangeSpriteColor(Color.blue);
        }
        else
        {
            characterBase.ChangeSpriteColor(Color.red);
        }
    }
}
