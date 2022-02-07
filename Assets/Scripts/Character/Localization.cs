using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Localization : MonoBehaviour
{
    public GameObject room;
    public GameObject lastRoom;

    public static event Action OnRoomChanged;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Rooms") )
        {
            room = collision.gameObject;
            OnRoomChanged?.Invoke();
        }
    }

}
