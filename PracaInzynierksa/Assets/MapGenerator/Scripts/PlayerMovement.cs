using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private static PlayerMovement instance;
    public static PlayerMovement GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        instance = this; // Singleton
    }

    public float speed = 5f;
    public bool enableMovement;

    void Update()
    {
        if (enableMovement)
        {
            MovePlayer();
        }
    }

    private void MovePlayer()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector2 pos = transform.position;

        pos.x += h * speed * Time.deltaTime;
        pos.y += v * speed * Time.deltaTime;

        transform.position = pos;
    }

    public void DisableMovement()
    {
        enableMovement = false;
    }

    public void EnableleMovement()
    {
        enableMovement=true;
    }
}
