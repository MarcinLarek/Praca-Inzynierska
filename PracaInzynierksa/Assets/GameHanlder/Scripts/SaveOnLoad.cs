using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveOnLoad : MonoBehaviour
{
    private void Awake()
    {
        //Upewniamy sie ze GameHandler bedzie przenoszony pomiedzy scenami.
        DontDestroyOnLoad(this.gameObject);
    }
}
