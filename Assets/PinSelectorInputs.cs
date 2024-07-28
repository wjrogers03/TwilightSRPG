using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PinSelectorInputs : MonoBehaviour
{

    [SerializeField] public GameObject current_pin;
    [SerializeField] GameObject player_marker;
    [SerializeField] bool locked_position;




    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("The pinSelectorInput script was called, stop that!");
    }





    // Update is called once per frame
    void Update()
    {
        
    }
}
