using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class UpgradeManager : MonoBehaviour
{

    public GameObject CharacterTemplate;
    public GameObject activeCharacter;
    public GameObject characterPortrait;
    public List<GameObject> playerTeamIconList;
    public Sprite portraitDPS;
    public Sprite portraitTank;
    public Sprite portraitSupport;
    private static UpgradeManager instance;
    public GameObject crewScrollableList;

    public GameObject weaponPrefab;
    public GameObject weaponSlot;
    public GameObject armorPrefab;
    public GameObject armorSlot;
    public GameObject consumablePrefab;
    public GameObject consumableSlot;

    public static UpgradeManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
        GeneratePlayerCharacters();
    }

    private void GeneratePlayerCharacters()
    {
        foreach (GameObject playerCharacter in PlayerInfo.GetInstance().RecruitedCharacters)
        {
            GameObject character = CharacterTemplate;
            GameObject spawnedCharacter = Instantiate(character, new Vector3(0, 0), Quaternion.identity);
            spawnedCharacter.transform.SetParent(crewScrollableList.transform);

            spawnedCharacter.name = character.name;



            CharacterStats characterstats = spawnedCharacter.GetComponent<CharacterStats>();
            CharacterStats playerCharacterStats = playerCharacter.GetComponent<CharacterStats>();

            characterstats.CopyStats(playerCharacterStats);

            Debug.Log(spawnedCharacter.GetComponent<CharacterStats>().classname.ToString());
            spawnedCharacter.GetComponent<CharacterIcon>().SetIcon();
            playerTeamIconList.Add(spawnedCharacter);

        }


    }

    public void LoadCharacterPortrait()
    {
        Image portrait = characterPortrait.GetComponent<Image>();
        switch (activeCharacter.GetComponent<CharacterStats>().classname)
        {
            case (CharacterStats.Classes.DMG):
                portrait.sprite = portraitDPS;
                break;
            case (CharacterStats.Classes.TANK):
                portrait.sprite = portraitTank;
                break;
            case (CharacterStats.Classes.SUPPORT):
                portrait.sprite = portraitSupport;
                break;
        }
        portrait.color = Color.white;
    }

    public void SaveCharacterStats()
    {
        foreach (GameObject playerCharacter in playerTeamIconList)
        {

            GameObject GHCharacter = PlayerInfo.GetInstance().RecruitedCharacters.Find((x) => x.GetComponent<CharacterStats>().characterID == playerCharacter.GetComponent<CharacterStats>().characterID);
            CharacterStats GHCharacterStats = GHCharacter.GetComponent<CharacterStats>();
            CharacterStats playerCharacterStats = playerCharacter.GetComponent<CharacterStats>();

            if (playerCharacterStats.weaponID == 0 && GHCharacterStats.weaponID != 0)
            {
                InventoryHandler.GetInstance().inventoryItems.Find((x) => x.GetComponent<ItemInfo>().itemId == GHCharacterStats.weaponID).GetComponent<ItemInfo>().equiped = false;
            }
            else if (playerCharacterStats.weaponID != 0)
            {
                InventoryHandler.GetInstance().inventoryItems.Find((x) => x.GetComponent<ItemInfo>().itemId == playerCharacterStats.weaponID).GetComponent<ItemInfo>().equiped = true;
            }


            if (playerCharacterStats.armorID == 0 && GHCharacterStats.armorID != 0)
            {
                InventoryHandler.GetInstance().inventoryItems.Find((x) => x.GetComponent<ItemInfo>().itemId == GHCharacterStats.armorID).GetComponent<ItemInfo>().equiped = false;
            }
            else if (playerCharacterStats.armorID != 0)
            {
                InventoryHandler.GetInstance().inventoryItems.Find((x) => x.GetComponent<ItemInfo>().itemId == playerCharacterStats.armorID).GetComponent<ItemInfo>().equiped = true;
            }


            if (playerCharacterStats.consumableID == 0 && GHCharacterStats.consumableID != 0)
            {
                InventoryHandler.GetInstance().inventoryItems.Find((x) => x.GetComponent<ItemInfo>().itemId == GHCharacterStats.consumableID).GetComponent<ItemInfo>().equiped = false;
            }
            else if (playerCharacterStats.consumableID != 0)
            {
                InventoryHandler.GetInstance().inventoryItems.Find((x) => x.GetComponent<ItemInfo>().itemId == playerCharacterStats.consumableID).GetComponent<ItemInfo>().equiped = true;
            }

            GHCharacter.GetComponent<CharacterStats>().CopyStats(playerCharacter.GetComponent<CharacterStats>());
        }
    } 

    public void LoadCharacterEquipment()
    {
        CharacterStats characterStats = activeCharacter.GetComponent<CharacterStats>();

        if (characterStats.weaponID != 0)
        {
            GameObject weapon = InventoryHandler.GetInstance().inventoryItems.Find((x) => x.GetComponent<ItemInfo>().itemId == characterStats.weaponID);
            GameObject weaponToSpawn = weaponPrefab;
            weaponToSpawn.GetComponent<WeaponInfo>().AssignStats(weapon.GetComponent<WeaponInfo>());
            Instantiate(weaponToSpawn, weaponSlot.transform);

        }

        if (characterStats.armorID != 0)
        {
            GameObject armor = InventoryHandler.GetInstance().inventoryItems.Find((x) => x.GetComponent<ItemInfo>().itemId == characterStats.armorID);
            GameObject armorToSpawn = armorPrefab;
            armorToSpawn.GetComponent<ArmorInfo>().AssignStats(armor.GetComponent<ArmorInfo>());
            Instantiate(armorToSpawn, armorSlot.transform);

        }

        if (characterStats.consumableID != 0)
        {
            GameObject consumable = InventoryHandler.GetInstance().inventoryItems.Find((x) => x.GetComponent<ItemInfo>().itemId == characterStats.consumableID);
            GameObject consumableToSpawn = consumablePrefab;
            consumableToSpawn.GetComponent<ConsumableInfo>().AssignStats(consumable.GetComponent<ConsumableInfo>());
            Instantiate(consumableToSpawn, consumableSlot.transform);

        }
    }

    public void MainHubButton()
    {
        SceneManager.LoadScene(sceneName: "MainHub");
    }

}
