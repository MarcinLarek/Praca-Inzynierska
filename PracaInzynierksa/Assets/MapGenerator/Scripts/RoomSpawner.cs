using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{

    public int openingDirection;
    // 1 --> need bottom door
    // 2 --> need top door
    // 3 --> need left door
    // 4 --> need right door


    private RoomTemplates templates;
    private int rand;
    public bool spawned = false;

    public float waitTime = 4f; // Czas po których spawnpoint weŸmie i siê usunie

    void Start()
    {
        Destroy(gameObject, waitTime); // Usuwamy spawnpoint po tym czasie ¿eby nie lagowa³o
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>(); //Pobieram te wszystkie arraye z prefabami pomieszczen.
        Invoke("Spawn", 0.1f); // wykonujemy Funkcje Spawn co 0.1 sekundy
    }


    void Spawn()
    {
        // Sprawdzamy jakie kierunek ma nasz spawnpoint.
        // NP jest jest to dolny spawnpoint to bierzemy losowe pomieszczenie ze spawnpointem gornym itd.
        // Nastepnie respiimy pomieszczenie
        if (spawned == false)
        {
            if (openingDirection == 2)
            {
                // Need to spawn a room with a BOTTOM door.
                rand = Random.Range(0, templates.bottomRooms.Length);
                Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
            }
            else if (openingDirection == 1)
            {
                // Need to spawn a room with a TOP door.
                rand = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
            }
            else if (openingDirection == 4)
            {
                // Need to spawn a room with a LEFT door.
                rand = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
            }
            else if (openingDirection == 3)
            {
                // Need to spawn a room with a RIGHT door.
                rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
            }
            spawned = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Wykonuje sie na kolizji 2 obiektow. If spradza czy te obiekty to spawnpointy
        if (other.CompareTag("SpawnPoint"))
        {
            // Otworzy na kosmos pojawiaja sie jesli 2 spawnponty sie ze soba stykna i odrazu usuna przez co nie maja czasu na Respawn pomieszczenia
            // Przez to musimy wdro¿yæ rozwi¹zanie ala £êcina
            // Mo¿e nie najlepiej, ale jako tako
            // Pierw sprawdzamy czy oba koliduj¹ce spawnpointy niczego nie zrespi³y
            if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false) 
            {
                templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
                // Musimy na nowo pobraæ te wszystkie tablice z pokojami
                // Zapytacie pewnie dlaczego skoro ju¿ mamy to pobrane wczeœnieœ
                // Jeœli tego nie zrobimy pokoje generuja sie na sobie i wywala Null Exception
                // A dlaczego tak sie dzieje?
                // \_(-_-)_/
                Instantiate(templates.closedRoom, transform.position, Quaternion.identity);// A tutaj po prostu respimy blokade. Wypelnione pomieszczenie bez wejsc/wyjsc
                Destroy(gameObject); // Usuwammy kolizje
            }
            spawned = true; // Ustawiamy spawned na true zeby nie respilo sie kolejne pomieszcze po niewykryciu kolizji w tym miejscu
        }
    }

}
