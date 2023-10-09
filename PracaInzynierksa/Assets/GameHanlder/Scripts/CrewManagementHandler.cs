using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewManagementHandler : MonoBehaviour, IDataPersistence
{
    public List<GameObject> generatedRecruits;
    public GameObject playerCharacterPreFab;
    public bool recruitsAreGenerated;
    private static CrewManagementHandler instance;

    public static CrewManagementHandler GetInstance()
    {
        return instance;
    }
    private void Awake()
    {
        instance = this; // Singleton
    }

    public void ResetRecruitsList()
    {
        recruitsAreGenerated = false;
        foreach(GameObject recruit in generatedRecruits)
        {
            Destroy(recruit);
        }
        generatedRecruits.Clear();
    }

    public void LoadData(GameData data)
    {
        this.recruitsAreGenerated = data.recruitsAreGenerated;
        //List
        //.generatedRecruits = data.generatedRecruits;
    }

    public void SaveData(ref GameData data)
    {
        data.recruitsAreGenerated = this.recruitsAreGenerated;
        //List
        //data.generatedRecruits = this.generatedRecruits;
    }
}
