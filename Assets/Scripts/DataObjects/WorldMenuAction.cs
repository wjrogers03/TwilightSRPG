using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute]
public class WorldMenuAction : ScriptableObject
{
    [Header("Info")]
    [SerializeField] public string display_name;
    [SerializeField] public string tool_tip;
    [SerializeField] public string callback_name;
}
