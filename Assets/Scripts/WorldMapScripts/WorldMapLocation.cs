using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WorldMapLocation : MonoBehaviour
{
    // convert to scriptable object.
    // Analogous to StageData.cs in the other example.
    [Header("Location Info")]
    [SerializeField] public string Name;
    [SerializeField] public string Description;
    [SerializeField] public Texture2D previewart;
    
    [Header("Scene Linking Options")]
    [SerializeField] public string SceneName;
    
    [Header("Menu Generation")]
    [SerializeField] public List<string> options = new List<string>();
    
    [Header("Location Tracking Info")]
    [SerializeField] public bool clear;
    [SerializeField] public bool available;
}
