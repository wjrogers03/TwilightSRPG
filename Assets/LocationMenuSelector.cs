using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationMenuSelector : MonoBehaviour
{

    [SerializeField] public float x_offset;
    [SerializeField] public int movementSpeed = 400;
    [SerializeField] public bool can_move = true;
    [SerializeField] public bool is_moving = true;
    public float destination_buffer = 0.0001f;
    private Vector3 destination_location = new();
    [HideInInspector]
    public GameObject current_button;


    public void assign_destination(Vector3 destination)
    {
        // apply shifting
        Vector3 realTarget = new Vector3(destination.x+x_offset, destination.y, -18.0f);
        this.destination_location = realTarget;
    }

    public void jump_to_destination()
    {
        Debug.Log("Jumping immediately to"+ this.destination_location);
        this.transform.localPosition = this.destination_location;
    }

    public void jump_to_position(Vector3 position)
    {
        this.transform.localPosition = position;
    }

    public void onInput()
    {
        this.can_move = true;
        this.is_moving = true;
    }

    public void onArrival()
    {
        this.can_move = false;
        this.is_moving = false;
    }


    private void Awake()
    {
        // switch this off so it doesn't eat resources when the menu isn't launched.
        this.gameObject.SetActive(false);
    }
    // No start actions

    // Update is called once per frame
    void Update()
    {
        if (can_move)
        {
            // change sprite to face the direction
            // calculate step size
            var step = movementSpeed * Time.deltaTime; //calculate distance to move, doesn't need to be here, just leaving it for testing movementSpeed var.
            // move the menu pointer
            this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, this.destination_location, step);
            if (Vector3.Distance(this.transform.localPosition, destination_location) < destination_buffer)
            {
                // we've arrived, set the destination
                onArrival();
            }
        }
    }
}
