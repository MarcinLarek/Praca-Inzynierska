using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour, IDataPersistence
{

    private static PlayerInfo instance;

    public static PlayerInfo GetInstance()
    {
        return instance;
    }
    //Prefab GameObjectu przechowywujacego informacje miedzy scenami o pojedynczej postaci
    public GameObject playerCharacterPreFab;
    //Lista wszystkich zrekrutowanych postaci
    public List<GameObject> CharactersInActiveTeam;
    //Lista postaci ktore bierzemy na misje
    public List<GameObject> RecruitedCharacters;
    //Waluta i statysytki zmieniajace sie po ulepszeniu statku
    public int playerMoney;
    public int recruitslimit = 2;
    public int crewlimit = 5;
    public int teamlimit = 3;
    public bool betterrecruits = false;

    private void Awake()
    {
        instance = this; // Singleton
    }

    public void LoadData(GameData data)
    {
        this.playerMoney = data.playerMoney;
        this.recruitslimit = data.recruitslimit;
        this.crewlimit = data.crewlimit;
        this.teamlimit = data.teamlimit;
        this.betterrecruits = data.betterrecruits;
        //Lists
        //this.CharactersInActiveTeam = data.CharactersInActiveTeam;
        //this.RecruitedCharacters = data.RecruitedCharacters;

    }

    public void SaveData(ref GameData data)
    {
        data.playerMoney = this.playerMoney;
        data.recruitslimit = this.recruitslimit;
        data.crewlimit = this.crewlimit;
        data.teamlimit = this.teamlimit;
        data.betterrecruits = this.betterrecruits;
        //Lists
        //data.CharactersInActiveTeam = this.CharactersInActiveTeam;
        //data.RecruitedCharacters = this.RecruitedCharacters;
    }
}
