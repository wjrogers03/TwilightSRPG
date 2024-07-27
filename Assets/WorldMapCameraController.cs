using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapCameraController : MonoBehaviour
{
    [SerializeField] GameObject player_marker;
    [SerializeField] public bool follow_player;
    // Start is called before the first frame update
    

    private void Update()
    {
        //Debug.Log(follow_player);
        if (follow_player)
        {
            move_camera(player_marker.gameObject.transform.position);
        }
    }

    public void move_camera(Vector3 target_position)
    {
        this.transform.position = target_position;
    }


    public void FollowPlayer()
    {
        this.follow_player = true;
    }

    public void StopFollowPlayer()
    {
        this.follow_player = false;
    }
}
