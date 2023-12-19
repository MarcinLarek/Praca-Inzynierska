using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterStats : MonoBehaviour
  
{
    //To plik od statysytk i informacji postaci. Te statystyki bedzie sie przenosic miedzy scenami

    //Main stats
    public int characterID = 0;
    public string charactername;
    public Classes classname;
    public bool isplayerteam;
    public bool isalive = true;
    public bool inactiveteam; // Uzywane do sprawdzenia czy jest w obecnie wybranej druzynie
    public int maxHealth;
    public int health;
    public int maxActionPoints;
    public int actionPoints;
    public int strength;
    public int endurance;
    public int agility;
    public int luck;
    public int inteligence;
    //other stats
    public int experience;
    public Animator animatorr;

    public int price;
    public int bonusDamage;

    //Items
    public int weaponID;
    public int armorID;
    public int consumableID;


    private void Awake()
    {
        if (characterID == 0)
        {
            characterID = Random.Range(0, 2147483640);
        }
    }
    public enum Classes
    {
        DMG,
        SUPPORT,
        TANK,
    }
    private void Start()
    {
        animatorr = GetComponent<Animator>();
    }
    //Kopiowanie statysytk miedzy postaciami.
    //Przypisanie bezposrednio jednego komponentu do drugiego da nam referencje poniwea¿ C#
    //A jak jest referencja to wszystko pojdzie sie <cenzura> w momencie jak usuniemy oryginal
    //Dlatego przypisujemy wartosc po wartosci.
    //Trzeba to ogolnie zrobic inaczej bo za kazdym razem jak cos dodajemy to trzeba to aktualizowac
    public void CopyStats(CharacterStats fromCharacter)
    {
        this.characterID = fromCharacter.characterID;
        this.charactername = fromCharacter.charactername;
        this.classname = fromCharacter.classname;
        this.isplayerteam = fromCharacter.isplayerteam;
        this.isalive = fromCharacter.isalive;
        this.maxHealth = fromCharacter.maxHealth;
        this.health = fromCharacter.health;
        this.maxActionPoints = fromCharacter.maxActionPoints;
        this.actionPoints = fromCharacter.actionPoints;
        this.strength = fromCharacter.strength;
        this.endurance = fromCharacter.endurance;
        this.agility = fromCharacter.agility;
        this.luck = fromCharacter.luck;
        this.inteligence = fromCharacter.inteligence;
        this.experience = fromCharacter.experience;
        this.price = fromCharacter.price;
        this.inactiveteam = fromCharacter.inactiveteam;
        this.weaponID = fromCharacter.weaponID;
        this.armorID = fromCharacter.armorID;
        this.consumableID = fromCharacter.consumableID;
    }

    public void RecieveDamage(int damage)
    {
        int finaldamage = damage;
        switch (classname)
        {
            case Classes.DMG:
                finaldamage -= endurance;
                break;
            case Classes.SUPPORT:
                finaldamage -= (endurance - 2);
                break;
            case Classes.TANK:
                finaldamage -= (endurance + 3);
                break;
        }
        ArmorInfo armorinfo = this.gameObject.GetComponent<ArmorInfo>();
        if (armorinfo != null)
        {
            finaldamage -= armorinfo.stopingPower;
        }
        if (finaldamage < 0) finaldamage = 0;
        this.health -= finaldamage;
        Debug.Log(charactername + " blocks " + (damage - finaldamage) + " out of " + damage + " total damage");

    }


}
