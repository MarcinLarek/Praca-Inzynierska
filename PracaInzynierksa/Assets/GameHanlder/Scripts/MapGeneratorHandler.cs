using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using static MissionSelectionHandler;

public class MapGeneratorHandler : MonoBehaviour
{
    private static MapGeneratorHandler instance;
    public static MapGeneratorHandler GetInstance()
    {
        return instance;
    }

    public bool mapGenerated;
    //Uzywane przy generowaniu nowej mapy
    //public List<GameObject> spawnRooms;

    //
    public GameObject[] bottomRoomsScavengerBase;
    public GameObject[] topRoomsScavengerBase;
    public GameObject[] leftRoomsScavengerBase;
    public GameObject[] rightRoomsScavengerBase;
    public GameObject spawnRoomScavengerBase;
    //
    public GameObject[] bottomRoomsTunels;
    public GameObject[] topRoomsTunels;
    public GameObject[] leftRoomsTunels;
    public GameObject[] rightRoomsTunels;
    public GameObject spawnRoomTunels;
    //
    public GameObject[] bottomRoomsShipWreck;
    public GameObject[] topRoomsShipWreck;
    public GameObject[] leftRoomsShipWreck;
    public GameObject[] rightRoomsShipWreck;
    public GameObject spawnRoomShipWreck;

    //Uzywane przy wczytywaniu istniejacej mapy
    private RoomTemplates templates;
    public List<GameObject> rooms;
    public List<GameObject> encountersList;

    //Uzywane tu i tu. W momencie generowania nowej mapy jest tutaj zwyczajny prefab, przy wchodzeni ze sceny walki
    //Przechowywujemy tutaj pozycje gracza na mapie.
    //newPlayerPrefab uzwany TYLKO do respienia nowego znacznika gracza
    //player uzywany do przechowywania informacji i przenoszenia znacznika miedzy scenami
    public GameObject player;
    public GameObject newPlayerPrefab;

    private void Awake()
    {
        instance = this; // Singleton
    }

    //Funkcja decydujaca w jaki sposob wygenerowac scene.
    //Jesli mapGenerated jest na true to znaczy ze wracamy z BattleScreen do wczesniej juz wygenerwoanej i eksplorowanej mapy.
    //Jesli mapGenerated jest na false to znaczy ze przychodzimy z MissionSelection i musimy wygenerowac nowa mape.
    public void PrepareScene()
    {
        if (mapGenerated)
        {
            LoadSceneState();
        }
        else
        {
            GenerateNewScene();
        }

    }

    private GameObject LoadMissionSettings()
    {
        RoomTemplates templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        switch (MissionSelectionHandler.GetInstance().ActiveMission.GetComponent<MissionInfo>().dungeonType)
        {
            case DungeonType.MineTunels:
                templates.bottomRooms = bottomRoomsTunels;
                templates.topRooms = topRoomsTunels;
                templates.leftRooms = leftRoomsTunels;
                templates.rightRooms = rightRoomsTunels;
                return spawnRoomTunels;
            case DungeonType.ScavengerBase:
                templates.bottomRooms = bottomRoomsScavengerBase;
                templates.topRooms = topRoomsScavengerBase;
                templates.leftRooms = leftRoomsScavengerBase;
                templates.rightRooms = rightRoomsScavengerBase;
                return spawnRoomScavengerBase;
            case DungeonType.ShipWreck:
                templates.bottomRooms = bottomRoomsShipWreck;
                templates.topRooms = topRoomsShipWreck;
                templates.leftRooms = leftRoomsShipWreck;
                templates.rightRooms = rightRoomsShipWreck;
                return spawnRoomShipWreck;
            default:
                return spawnRoomShipWreck;
        }
    }

    private void GenerateNewScene()
    {
        //Respimy losowy pokoj poczatkowy na koordynatach 0,0. On automatycznie wygeneruje reszte pomieszczen.
        //Nastepnie respimy gracza conajmniej 3pkt x/y od miejsca respa pokoju poczatkowego.
        //Jesli zrespimy w tym samym miejscu, gracz zostanie od razu usuniety.

        GameObject spawnRoom = LoadMissionSettings();
        Vector2 roomposition = new Vector2(0, 0);
        Instantiate(spawnRoom, roomposition, spawnRoom.transform.rotation);
        Vector2 playerposition = new Vector2(-3, 0);
        GameObject spawnedPlayer = Instantiate(newPlayerPrefab, playerposition, newPlayerPrefab.transform.rotation);
        spawnedPlayer.name = newPlayerPrefab.name; //Tutaj usuwamy (clone) z nazwy
        player = spawnedPlayer;
    }

    private void LoadSceneState()
    {
        //Wpisujemy nasze dane przechowywane w GameHandlerze do RoomTemplates ktore jest normalnie wykorzystywany przez scene
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        templates.rooms = rooms;
        templates.encountersList = encountersList;

        //Teraz aktywujemy pokolei wszystko co wczesniej zapisalismy przy opuszczaniu sceny.
        foreach (GameObject room in rooms)
        {
            if(room != null)
            {
                room.SetActive(true);
            }
        }
        foreach (GameObject encounter in encountersList)
        {
            if (encounter!= null)
            {
                encounter.SetActive(true);
            }
        }

        player.SetActive(true);
    }

    //Odpalane po skonczeniu eksploracji, czyli pokonaniu bossa/straceniu zalogi
    //Usuwa wszystkie GameObjecty potrzebne do generowania mapy i ustawia zmienna mapGeneated na false
    //aby nastepen wejscie do generatora respilo nam nowa mape zamiast wczytywac stara.
    public void RemoveGeneratedScene()
    {
        foreach(GameObject room in rooms)
        {
            Destroy(room);
        }
        foreach(GameObject encounter in encountersList)
        {
            Destroy(encounter);
        }

        rooms.Clear();
        encountersList.Clear();
        mapGenerated = false;
    }
}
