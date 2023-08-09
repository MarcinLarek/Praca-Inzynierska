using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Kamikaze : MonoBehaviour
{
    //Czym jest Kamikaze?
    //
    //Przechowwyanie informacji o naszej grze odbywa sie poprzez GamObject o nazwie GameHandler.
    //Z zalozenia po jego pierwszym stworzeniu bedzie on obecny przez cala gre, transportujac sie miedzy scenami
    //Czesc scen najzwyczajniej nie dziala jesli nie ma w niej GameHandlera
    //Dlatego powstal Kamikaze
    //Kamikaze sprawdza czy w scenie znajduje sie juz GameHanlder (np przetransportowany z innej sceny)
    //Jesli go wykryje to obiekt Kamikaze po prostu sie wysadza i przestaje istniec
    //Jesli go nie wykryje to pierw tworzy nowego GameHandlera z prefabu i dopiero wtedy sie wysadza
    //
    //Finalnie pewnie nie bedzie wcale wykorzystywany, ablo bedzie wykorzystywany podczas ladowania gry/startu nowej gry
    //Obecnie mozna go wrzucac do scen wymagajacych GameHandlera podczas ich debugowania/edytowania 
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
