using UnityEngine;

public class TopBottomPanelAdjust : MonoBehaviour
{
    public GameObject canvas;
    public GameObject ParentPanel;

    public enum CornerAnchor
    {
        TopLeft,
        BottomLeft
    }

    public CornerAnchor anchorCorner = CornerAnchor.TopLeft;


    [Tooltip("Height of the panel as a percentage of canvas height.")]
    [Range(0.05f, 0.5f)]
    public float heightPercent = 0.2f;

    void Start()
    {
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        RectTransform rect = GetComponent<RectTransform>();

        float canvasWidth = canvasRect.rect.width;
        float canvasHeight = canvasRect.rect.height;

        float panelWidth = ParentPanel.GetComponent<RectTransform>().sizeDelta.y * 0.8f;
        float panelHeight = canvasHeight * heightPercent;

        rect.sizeDelta = new Vector2(panelWidth, panelHeight);

        switch (anchorCorner)
        {
            case CornerAnchor.TopLeft:
                rect.anchorMin = new Vector2(0, 1);
                rect.anchorMax = new Vector2(0, 1);
                rect.pivot = new Vector2(0, 1);
                break;

            case CornerAnchor.BottomLeft:
                rect.anchorMin = new Vector2(0, 0);
                rect.anchorMax = new Vector2(0, 0);
                rect.pivot = new Vector2(0, 0);
                break;
        }

        rect.anchoredPosition = Vector2.zero;
    }
}
