using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Transactions;
using TMPro;
using Unity.Profiling;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class WorldMenuManager : MonoBehaviour
{
    // this should manage the menus and nothing else.
    // sources information from the GDM
    [Header("Additional Displays")]
    [SerializeField] GameObject world_map_reference;
    



    #region Lifecycle Functions
    public void Awake()
    {
        world_map_reference.SetActive(false);
    }

    #endregion
}

