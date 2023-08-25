using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewManagementHandler : MonoBehaviour
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
}
