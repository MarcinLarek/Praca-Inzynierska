using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Kamikaze : MonoBehaviour
{
    public GameObject NewGameHandler;
    private void Awake()
    {
        if (GameObject.Find("GameHandler") == null)
        {
            Vector3 position = new Vector3(0, 0);
            GameObject GameHandler = Instantiate(NewGameHandler, position, Quaternion.identity);
            GameHandler.name = NewGameHandler.name;
            Debug.Log("NIE WYKRTO GAMEHANDLERA");
        }
        Destroy(this.gameObject);
    }
}
