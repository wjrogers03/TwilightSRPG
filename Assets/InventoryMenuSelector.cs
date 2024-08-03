using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenuSelector : MonoBehaviour
{
    [SerializeField] public float x_offset;
    [SerializeField] public float y_offset;
    [SerializeField] public int movementSpeed = 400;
    [SerializeField] public bool can_move = true;
    [SerializeField] public bool is_moving = true;
    public float destination_buffer = 0.0001f;
    private Vector3 destination_location = new();
    public GameObject destination_object;
    [SerializeField] public bool useTargeting = true;
    private int frames_on_target = 0;
    private int frames_before_stopping = 4*60;
    
    public void assign_destination(Vector3 destination)
    {
        // apply shifting
        Vector3 realTarget = new Vector3(destination.x + x_offset, destination.y + y_offset, -18.0f);
        this.destination_location = realTarget;
    }

    public void assign_target(GameObject target)
    {
        this.destination_object = target;
    }

    public void jump_to_destination()
    {
        Debug.Log("Jumping immediately to" + this.destination_location);
        this.transform.position = this.destination_location;
    }

    public void jump_to_position(Vector3 position)
    {
        this.transform.position = position;
    }

    public void onInput()
    {
        this.frames_on_target = 0;
        this.can_move = true;
        this.is_moving = true;
    }

    public void onArrival()
    {
        this.frames_on_target += 1;
        if (this.frames_on_target >= frames_before_stopping)
        {
            this.can_move = false;
            this.is_moving = false;
        }
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
            if (useTargeting)
            {
                assign_destination(destination_object.transform.position);
                Debug.Log("Moving Cursor to target: " + destination_object.transform.position);
            }
            //this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, this.destination_location, step);
            this.transform.position = Vector3.MoveTowards(this.transform.position, this.destination_location, step);
            // removed destination checking, it's inefficient but I think for this it'll be ok. 
            // it's during the menu, and this type of game isn't that taxing that we need to worry about every little inefficiency. (famous last words)
            if (Vector3.Distance(this.transform.position, destination_location) < destination_buffer)
            {
                // modified onArrival() method to account for delay by counting frames.
                onArrival();
            }
        }
    }
}
