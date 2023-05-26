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
    public GameObject[] bossroom; // Tablica z dost�pnymi pomieszczeniamy ko�cowymi (pomieszczenia z bossem, ale w sumie nie musi go tam by�. Zalezy od prefabu)

    private void Update()
    {
        if(waitTime <= 0 && spawnedBoss == false) // Czekamy sztywno podany czas a� mapa sko�czy si� generowa�. Sprawdzamy te� czy pomieszczenie z Bossem si� ju� czasem nie zrespi�o
        {

            // Poniewa� bierzemy najnowszy wygenrowany pok�j z naszej listy wygenerwoanych pokoi (Najnowszy bedzie najdalej), musimy sprawdzi� czy nie jest to pusty
            // pok�j, kt�ry s�u�y do zamykania dziur na kosmos. Je�li tak jest to bierzemy przedostatni (zmieniamy rooms.count -1 na rooms.count -2)
            // G��wny opis tego co si� dzieje jest w elsie. Ten pierwszy warunek jest analogiczny z t� r�nic� �e bierzemy inny pok�j z listy
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
