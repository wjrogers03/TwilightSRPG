using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UIElements;

public class PlayerMarker : MonoBehaviour
{
    [SerializeField]
    int movementSpeed = 70;
    public bool can_move = false;
    public bool is_moving = false;
    [SerializeField]
    public float destination_buffer = 0.0001f;
    private Vector3 destination_location = new();
    [SerializeField]
    public Camera main_camera;
    private LocationPinObect lpo;
    public void update_position(Vector3 position)
    {
        // there's some weird stuff going on with local vectorPosition. I'd rather use world space but it really doesn't want to work with
        // reference positions, even when i'm passing a transform.position as opposed to a transform.localPosition.
        Vector3 new_position = new Vector3(position.x, position.y, 10.0f);
        this.transform.localPosition = new_position;
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0.1f);
    }


    void Update()
    {
        if (can_move)
        {
            // change sprite to face the direction
            // calculate step size
            var step = movementSpeed * Time.deltaTime; //calculate distance to move, doesn't need to be here, just leaving it for testing movementSpeed var.
            // move the playermarker
            this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, this.destination_location, step);
            if (Vector3.Distance(this.transform.localPosition, destination_location) < destination_buffer)
            {
                // we've arrived, set the destination
                onArrival();
            }
        }
        
    }

    public void onArrival()
    {
        main_camera.GetComponent<WorldMapCameraController>().StopFollowPlayer();
        lpo.OnMouseDown();
        this.can_move = false;
        this.is_moving = false;
    }
    public void move_to_next_pin(Vector3 position, LocationPinObect reference_pin)
    {
        Vector3 new_position = new Vector3(position.x, position.y, 10.0f);
        this.destination_location = new_position;


        this.lpo = reference_pin;
        this.can_move = true;
        this.is_moving = true;
    }

    public void Start()
    {
        // get location guid from GDM.playerData
        bool location_set = false;

        // in awake, the pin checks to see if the location is occupied, so when the code reaches here it should already be set.
        // we now have to iterate through all the pins to find the occupied one, and move the marker there
        // get pins from gdm
        // find the pin that is occupied
        // move the player marker & camera there.
        Debug.Log("world_map_pin order: 3");
        // lets do a double loop, becuase I loooooooove that.

        foreach (LocationPinObect pin in GameDataManager.instance.world_map_pins.Values)
        {
            pin.startup_occupation_check();
            pin.update_pin_status();
        }



        foreach (LocationPinObect pin in GameDataManager.instance.world_map_pins.Values)
        {
            if (pin.player_occupied)
            {
                update_position(pin.transform.localPosition);
                main_camera.GetComponent<WorldMapCameraController>().move_camera(this.transform.position);
                location_set = true;
                break;

            }
        }
        if (!location_set)
        {
            foreach (LocationPinObect pin in GameDataManager.instance.world_map_pins.Values)
            {
                if (pin.GetComponent<LocationPinObect>().associated_location.SceneName == "DungeonMap_COTD")
                {
                    update_position(pin.transform.localPosition);
                    pin.player_occupied = true;
                    main_camera.GetComponent<WorldMapCameraController>().move_camera(this.transform.position);
                    break;
                }
            }
        }
    }


}
