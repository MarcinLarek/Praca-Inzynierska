using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject); 
        // Póki co u¿ywane jedynie przy Startowym pomieszczeniu. Jeœli jakikolwiek spawnpoint wejdzie  w kolizje to go usuwamy
    }
}
