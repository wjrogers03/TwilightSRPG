using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

public class PinSelector : MonoBehaviour
{
    [SerializeField] public float y_offset;
    [SerializeField] public int movementSpeed = 2000;
    [SerializeField] public GameObject current_pin;
    [SerializeField] public GameObject player_marker;
    [SerializeField] public bool can_move = true;
    [SerializeField] public bool is_moving = true;
    public float destination_buffer = 0.0001f;
    private Vector3 destination_location = new();
    [SerializeField]
    public Camera main_camera;
    private LocationPinObect lpo;
    // Start is called before the first frame update
    void Start()
    {
        bool location_set = false;

        foreach (LocationPinObect pin in GameDataManager.instance.world_map_pins.Values)
        {
            if (pin.player_occupied)
            {
                this.current_pin = pin.gameObject;
                //Debug.Log("moving to pin: " + this.current_pin.name);
                this.destination_location = pin.transform.localPosition;
                jump_to_position(pin.transform.position);
                move_to_next_pin(this.current_pin);
                //main_camera.GetComponent<WorldMapCameraController>().move_camera(this.transform.position);
                location_set = true;
                break;

            }
        }
        if (!location_set)
        {
            Debug.Log("Fallback position updating");
            foreach (LocationPinObect pin in GameDataManager.instance.world_map_pins.Values)
            {
                if (pin.GetComponent<LocationPinObect>().associated_location.SceneName == "DungeonMap_COTD")
                {
                    jump_to_position(pin.transform.position);
                    break;
                }
            }
        }

    }

    // Update is called once per frame

    public void jump_to_position(Vector3 position)
    {
        //Debug.Log("Jumping to position: " + position);
        //Vector3 new_position = new Vector3(position.x, position.y + y_offset, 10.0f);
        this.transform.position = position;
    }

    public void onArrival()
    {
        this.can_move = false;
        this.is_moving = false;
    }

    public void move_to_next_pin(GameObject reference_pin)
    {
        Vector3 position = reference_pin.transform.localPosition;
        Vector3 new_position = new Vector3(position.x, position.y+y_offset, 10.0f);
        this.destination_location = new_position;


        this.lpo = reference_pin.GetComponent<LocationPinObect>();
        this.can_move = true;
        this.is_moving = true;
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
}
