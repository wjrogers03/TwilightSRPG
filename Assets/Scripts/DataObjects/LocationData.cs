using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenuAttribute]
public class LocationData : ScriptableObject
{
    [Header("Location Info")]
    [SerializeField] public string Name;
    [SerializeField] public string Description;
    [SerializeField] public Texture2D previewart;

    [Header("Scene Linking Options")]
    [SerializeField] public string SceneName;

    [Header("Menu Generation")]
    [SerializeField] public List<string> options = new List<string>();

    //[Header("Location Tracking Info")]
    //[SerializeField] public bool clear;
    //[SerializeField] public bool available;
    //[SerializeField] public bool accessible;

    [Header("GUID Identification String - Must Be Generated Once")]
    [SerializeField] public string id;

    [ContextMenu("Generate GUID for id")]
    public void GenerateGuid()
    {
        this.id = System.Guid.NewGuid().ToString();
    }
}