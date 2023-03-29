using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;
    public GameObject closedRoom;

    public List<GameObject> rooms;

    public float waitTime;
    private bool spawnedBoss;
    public GameObject[] bossroom;

    private void Update()
    {
        if(waitTime <= 0 && spawnedBoss == false)
        {
            if (rooms[rooms.Count - 1].name == "closedRoom")
            {
                string clone = "(Clone)";
                string bossroomname = "Boss-" + rooms[rooms.Count - 2].name.Replace(clone, "");

                Debug.Log(bossroomname);
                foreach (GameObject room in bossroom)
                {
                    if(room.name == bossroomname)
                    {
                        Debug.Log(room.name);
                        Instantiate(room, rooms[rooms.Count - 2].transform.position, Quaternion.identity);
                        Destroy(rooms[rooms.Count - 2]);
                        spawnedBoss = true;
                    }
                }
            }
            else
            {
                string clone = "(Clone)";
                string bossroomname = "Boss-" + rooms[rooms.Count - 1].name.Replace(clone, "");
                Debug.Log(bossroomname);
                foreach (GameObject room in bossroom)
                {
                    if (room.name == bossroomname)
                    {
                        Debug.Log(room.name);
                        Instantiate(room, rooms[rooms.Count - 1].transform.position, Quaternion.identity);
                        Destroy(rooms[rooms.Count - 1]);
                        spawnedBoss = true;
                    }
                }
            }
        } 
        else if (waitTime >= 0)
        {
            waitTime -= Time.deltaTime;
        }

    }
}
