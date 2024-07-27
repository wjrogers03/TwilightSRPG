using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class main_menu_control : MonoBehaviour
{
    public void change_scene(string scene)
    {
        if (string.IsNullOrEmpty(scene))
        { return; }
        Debug.Log("clicked button");
        SceneManager.LoadScene(scene);
    }
}
