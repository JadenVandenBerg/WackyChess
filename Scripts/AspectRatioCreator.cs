using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class AspectRatioCreator : MonoBehaviour
{

    public GameObject board;
    void Start()
    {
        Thread.Sleep(100);
        AspectRatioFitter aspectRatioFitter = GetComponent<AspectRatioFitter>();

        float width = board.GetComponent<RectTransform>().sizeDelta.x;

        aspectRatioFitter.aspectRatio = Screen.width - width;

        GetComponent<RectTransform>().position = new Vector2(width + board.GetComponent<RectTransform>().localPosition.x, GetComponent<RectTransform>().position.y);
    }
}
