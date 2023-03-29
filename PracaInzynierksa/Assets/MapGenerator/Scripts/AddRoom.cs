using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour
{
    private RoomTemplates templates;

    private void Start()
    {
        //Zapisujemy wszystkie wygenerwoane pomieszczenia do listy, ¿eby móæ je póŸniej edytowaæ / wywo³ywaæ.
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        templates.rooms.Add(this.gameObject);
    }
}
