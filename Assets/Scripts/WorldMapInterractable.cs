using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapInterractable : MonoBehaviour
{
    [SerializeField] WorldMapController controller;
    [SerializeField] GameObject button_container;

    public void OnMouseDown()
    {
        // the map was clicked, despawn any maps.
        controller.DespawnMenu(button_container);
    }
}
