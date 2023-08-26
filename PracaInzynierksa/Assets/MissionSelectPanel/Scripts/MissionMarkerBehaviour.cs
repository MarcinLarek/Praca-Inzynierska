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
        CrewManagementHandler.GetInstance().ResetRecruitsList();
        SceneManager.LoadScene(sceneName: "MapGenerator");
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
