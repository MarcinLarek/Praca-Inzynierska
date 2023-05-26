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

    public List<GameObject> rooms; // Lista z wygenrowanymi pomieszczeniami

    public float waitTime;
    private bool spawnedBoss;
    public GameObject[] bossroom; // Tablica z dostêpnymi pomieszczeniamy koñcowymi (pomieszczenia z bossem, ale w sumie nie musi go tam byæ. Zalezy od prefabu)

    private void Update()
    {
        if(waitTime <= 0 && spawnedBoss == false) // Czekamy sztywno podany czas a¿ mapa skoñczy siê generowaæ. Sprawdzamy te¿ czy pomieszczenie z Bossem siê ju¿ czasem nie zrespi³o
        {

            // Poniewa¿ bierzemy najnowszy wygenrowany pokój z naszej listy wygenerwoanych pokoi (Najnowszy bedzie najdalej), musimy sprawdziæ czy nie jest to pusty
            // pokój, który s³u¿y do zamykania dziur na kosmos. Jeœli tak jest to bierzemy przedostatni (zmieniamy rooms.count -1 na rooms.count -2)
            // G³ówny opis tego co siê dzieje jest w elsie. Ten pierwszy warunek jest analogiczny z t¹ ró¿nic¹ ¿e bierzemy inny pokój z listy
            if (rooms[rooms.Count - 1].name == "closedRoom(Clone)")
            {
                string clone = "(Clone)";
                string bossroomname = "Boss-" + rooms[rooms.Count - 2].name.Replace(clone, ""); 

                foreach (GameObject room in bossroom)
                {
                    if(room.name == bossroomname)
                    {
                        Instantiate(room, rooms[rooms.Count - 2].transform.position, Quaternion.identity);
                        Destroy(rooms[rooms.Count - 2]);
                        spawnedBoss = true;
                    }
                }
            }
            else
            {
                string clone = "(Clone)";
                string bossroomname = "Boss-" + rooms[rooms.Count - 1].name.Replace(clone, ""); // Bierzemy nazwe tego pomieszczenia.
                //Musimy usunac (Clone) z koncowki bo kazde gameobject respi sie z tym dopiskiem.

                foreach (GameObject room in bossroom)// Szukamy w pomieszczeniach z bossami odpowiednika naszego ostatniego wygenrowanego pomieszczenia
                {
                    if (room.name == bossroomname)
                    {
                        Instantiate(room, rooms[rooms.Count - 1].transform.position, Quaternion.identity);
                        Destroy(rooms[rooms.Count - 1]);
                        spawnedBoss = true;
                        //Respimy pomieszczenie z bossem i usuwamy to oryginalne. Zmieniamy status spawnedBoss na true zeby nam sie 2 nie respily.
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
