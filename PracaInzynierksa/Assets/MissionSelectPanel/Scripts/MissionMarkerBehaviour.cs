using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionMarkerBehaviour : MonoBehaviour
{
    private MissionHandler missionHandler = MissionHandler.GetInstance();

    public int spawnedNumber;
    void OnMouseDown()
    {
        EnterMisssionBehaviour();
    }

    private void EnterMisssionBehaviour()
    {
        MissionHandler missionHandler = MissionHandler.GetInstance();

        if (PlayerInfo.GetInstance().CharactersInActiveTeam.Count > 0)
        {
            CrewManagementHandler.GetInstance().ResetRecruitsList();
            InventoryHandler.GetInstance().ClearMerchantInventory();
            missionHandler.PrepareForMission(this);
            SceneManager.LoadScene(sceneName: "MapGenerator");
        }
        else
        {
            Debug.Log("U need at least one person in your team");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Glupie i proste rozwiazanie po to aby usunac tylko 1 z kolidujacych obiektow
        if(spawnedNumber > collision.gameObject.GetComponent<MissionMarkerBehaviour>().spawnedNumber)
        {
            Destroy(this.gameObject);
        }
    }
}
