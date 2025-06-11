using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightAdjust : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        RectTransform rect = GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(Screen.height, Screen.height);
        rect.position = new Vector2(rect.position.x, Screen.height / 2);
    }
}
