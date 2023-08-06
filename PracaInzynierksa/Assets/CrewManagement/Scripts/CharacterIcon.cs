using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterIcon : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite IconDPS;
    public Sprite IconTank;
    public Sprite IconSupport;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetIcon()
    {
        switch (this.GetComponent<CharacterStats>().classname)
        {
            case (CharacterStats.Classes.DMG):
                spriteRenderer.sprite = IconDPS;
                break;
            case (CharacterStats.Classes.TANK):
                spriteRenderer.sprite = IconTank;
                break;
            case (CharacterStats.Classes.SUPPORT):
                spriteRenderer.sprite = IconSupport;
                break;
        }
        ToggleTeamColor();
    }

    private void OnMouseDown()
    {
        CrewManager.GetInstance().activeCharacter = this.gameObject;
    }

    public void ToggleTeamColor()
    {
        if (this.GetComponent<CharacterStats>().inactiveteam)
        {
            spriteRenderer.color = Color.green;
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
    }
}
