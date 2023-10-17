using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    public Image characterIcon;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI actionPointsText;
    public TextMeshProUGUI weaponNameText;
    public TextMeshProUGUI damageNumberText;
    public TextMeshProUGUI armorNameText;
    public TextMeshProUGUI stoppingNumberText;
    public TextMeshProUGUI characterName;

    public Button consumableButton;
    public TextMeshProUGUI consumableButtonText;

    private BattleHandler battleHandler;

    public Sprite DPSIcon;
    public Sprite SUPPORTIcon;
    public Sprite TANKIcon;

    private static BattleHud instance;
    public static BattleHud GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
        battleHandler = BattleHandler.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        actionPointsText.text = $"{battleHandler.activeCharacter.GetComponent<CharacterStats>().actionPoints}/{battleHandler.activeCharacter.GetComponent<CharacterStats>().maxActionPoints}";
    }

    public void UpdateUi()
    {
        battleHandler = BattleHandler.GetInstance();
        GameObject activeCharacter = battleHandler.activeCharacter;
        CharacterStats ACStats = activeCharacter.GetComponent<CharacterStats>();
        WeaponInfo ACWeapon = activeCharacter.GetComponent<WeaponInfo>();
        ArmorInfo ACArmor = activeCharacter.GetComponent<ArmorInfo>();
        ConsumableInfo ACConsumable = activeCharacter.GetComponent<ConsumableInfo>();

        if(ACConsumable.itemName != "")
        {
            consumableButton.enabled = true;
            consumableButtonText.text = ACConsumable.itemName;
        }
        else
        {
            consumableButton.enabled = false;
            consumableButtonText.text = "No Item";
        }

        if (ACStats.isplayerteam)
        {
            healthText.text = $"{ACStats.health}/{ACStats.maxHealth}";
            actionPointsText.text = $"{ACStats.actionPoints}/{ACStats.maxActionPoints}";
            weaponNameText.text = $"{ACWeapon.itemName}";
            if (ACWeapon.damageBonus > 0)
            {
                damageNumberText.text = $"{ACWeapon.damageDices}d{ACWeapon.damageRange}+{ACWeapon.damageBonus}";
            }
            else
            {
                damageNumberText.text = $"{ACWeapon.damageDices}d{ACWeapon.damageRange}";
            }
            armorNameText.text = ACArmor.itemName;
            stoppingNumberText.text = $"Armor:{ACArmor.stopingPower} Endurance:{ACStats.endurance} Total: {ACStats.endurance + ACArmor.stopingPower}";
            characterName.text = ACStats.charactername;
            switch (ACStats.classname)
            {
                case CharacterStats.Classes.DMG:
                    characterIcon.sprite = DPSIcon;
                    break;
                case CharacterStats.Classes.SUPPORT:
                    characterIcon.sprite = SUPPORTIcon;
                    break;
                case CharacterStats.Classes.TANK:
                    characterIcon.sprite = TANKIcon;
                    break;
                default:
                    break;
            }
        }
    }
}
