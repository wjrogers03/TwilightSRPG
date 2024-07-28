using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneFader : MonoBehaviour
{

    [SerializeField]
    public float targetAlpha = 0f;
    [SerializeField]
    public float currentAlpha = 1f;
    [SerializeField]
    public float alphaStep = 0.005f; // 0.005 is a pleasent speed.
    // Start is called before the first frame update
    void Start()
    {
        Color color = this.gameObject.GetComponent<SpriteRenderer>().color;
        color.a = currentAlpha;
        this.gameObject.GetComponent<SpriteRenderer>().color = color;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentAlpha != targetAlpha)
        {
            Color color = this.gameObject.GetComponent<SpriteRenderer>().color;
            if (targetAlpha > currentAlpha)
            {
                float _newAlpha = currentAlpha + alphaStep;
                if (_newAlpha > 1f)
                {
                    _newAlpha = 1f;
                }
                color.a = _newAlpha;
            }
            else
            {
                float _newAlpha = currentAlpha - alphaStep;
                if (_newAlpha < 0f)
                {
                    _newAlpha = 0f;
                }
                color.a = _newAlpha;
            }
            this.gameObject.GetComponent<SpriteRenderer>().color = color;
            this.currentAlpha = color.a;
        }
    }
}
