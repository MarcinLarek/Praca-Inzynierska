using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveOnLoad : MonoBehaviour
{
    public bool dontDestroy = true;
    private void Awake()
    {
        if (dontDestroy)
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
