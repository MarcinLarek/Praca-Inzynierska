using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScreenHandler : MonoBehaviour
{
    public List<GameObject> enemyCharactersList;
    public bool bossFight;
    private static BattleScreenHandler instance;
    public static BattleScreenHandler GetInstance()
    {
        return instance;
    }
    private void Awake()
    {
        instance = this; // Singleton
    }

}
