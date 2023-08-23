using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ContextMenu : MonoBehaviour
{
    private bool showMenu = false;
    private Vector3 menuPosition;
    private GameObject clickedItem;
    public Image image;
    public int typ;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            menuPosition = Input.mousePosition;
            

            // Raycast co sprawdza czy item zosta³ trafiony tej.
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = menuPosition;
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            foreach (RaycastResult result in results)
            {
                if (result.gameObject != null && result.gameObject.name == "InventoryItem(Clone)")
                {
                    clickedItem = result.gameObject;
                    Debug.Log(clickedItem.name);
                    showMenu = true;
                    break;
                }
            }
        }
    }

    void OnGUI()
    {
        if (showMenu)
        {
            Rect menuRect = new Rect(menuPosition.x, Screen.height - menuPosition.y, 100, 200);
            GUILayout.BeginArea(menuRect);
            GUILayout.Box("Przedmiot");

            if (GUILayout.Button("U¿yj"))
            {
                if (clickedItem != null)
                {
                    Debug.Log("U¿yto elementu: " + clickedItem.name);
                    switch (typ)
                    {
                        case 1: //u¿ycie medykamentu
                            Destroy(clickedItem);
                            break;
                        case 2: //wyekwipowanie broni
                            break;
                        case 3: //inne
                            break;

                    }
                    //tutaj czarna magia bo trzeba zaimplementowaæ do sceny walki.
                }
            }
            if (GUILayout.Button("Wyrzuæ"))
            {
                Debug.Log("Wyrzucono element: " + clickedItem.name);
                Destroy(clickedItem);
            }

            GUILayout.EndArea();

            if (Event.current.type == EventType.MouseDown && !menuRect.Contains(Event.current.mousePosition))
            {
                showMenu = false;
                clickedItem = null;
            }
        }
    }
}
