using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;

public class SidePanelAdjust : MonoBehaviour
{ 
    public GameObject canvas;
    public TMP_FontAsset titleFont;
    public List<Sprite> squareImages;
    public List<Piece> panelPieces;

    [HideInInspector] public TMP_Text whiteCountText;
    [HideInInspector] public TMP_Text blackCountText;

    private RectTransform imageGridTransform;
    private GridLayoutGroup gridLayout;

    void Start()
    { 
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        RectTransform rect = GetComponent<RectTransform>();

        float screenHeight = Screen.height;
        float canvasWidth = canvasRect.rect.width;

        float boardSize = screenHeight;
        float panelWidth = canvasWidth - boardSize * 0.8f;

        rect.sizeDelta = new Vector2(panelWidth, screenHeight);
        rect.anchorMin = new Vector2(1, 0.5f);
        rect.anchorMax = new Vector2(1, 0.5f);
        rect.pivot = new Vector2(1, 0.5f);
        rect.anchoredPosition = new Vector2(0, 0);

        //CreateSidePanel(rect);
    }

    void CreateSidePanel(RectTransform parent)
    {
        GameObject container = new GameObject("MainContainer", typeof(RectTransform), typeof(VerticalLayoutGroup));
        container.transform.SetParent(parent, false);

        RectTransform containerRect = container.GetComponent<RectTransform>();
        containerRect.anchorMin = new Vector2(0, 0);
        containerRect.anchorMax = new Vector2(1, 1);
        containerRect.offsetMin = new Vector2(10, 10);
        containerRect.offsetMax = new Vector2(-10, -10);

        VerticalLayoutGroup containerLayout = container.GetComponent<VerticalLayoutGroup>();
        containerLayout.spacing = 10;
        containerLayout.childControlHeight = true;
        containerLayout.childControlWidth = true;
        containerLayout.childForceExpandHeight = false;

        
        
        blackCountText = CreateTextPanel(container.transform, "Black: 0");
        CreateSquareInfoPanel(container.transform);
        whiteCountText = CreateTextPanel(container.transform, "White: 0");

        RefreshImageGrid();
    }

    TMP_Text CreateTextPanel(Transform parent, string initialText)
    {
        GameObject textPanel = new GameObject(initialText.Split(':')[0] + "Panel", typeof(RectTransform), typeof(Image), typeof(LayoutElement));
        textPanel.transform.SetParent(parent, false);
        Image bg = textPanel.GetComponent<Image>();
        bg.color = new Color(0.15f, 0.15f, 0.15f, 0.9f);

        LayoutElement layout = textPanel.GetComponent<LayoutElement>();
        layout.preferredHeight = 40;

        GameObject textObj = new GameObject("Text", typeof(RectTransform), typeof(TextMeshProUGUI));
        textObj.transform.SetParent(textPanel.transform, false);

        RectTransform textRect = textObj.GetComponent<RectTransform>();
        textRect.anchorMin = Vector2.zero;
        textRect.anchorMax = Vector2.one;
        textRect.offsetMin = Vector2.zero;
        textRect.offsetMax = Vector2.zero;

        TMP_Text tmpText = textObj.GetComponent<TMP_Text>();
        tmpText.text = initialText;
        tmpText.font = titleFont;
        tmpText.fontSize = 24;
        tmpText.color = Color.white;
        tmpText.alignment = TextAlignmentOptions.Center;

        return tmpText;
    }

    void CreateSquareInfoPanel(Transform parent)
    {
        GameObject subPanelObj = new GameObject("SquareInfoPanel", typeof(RectTransform), typeof(Image), typeof(VerticalLayoutGroup));
        subPanelObj.transform.SetParent(parent, false);

        RectTransform subRect = subPanelObj.GetComponent<RectTransform>();
        Image bgImage = subPanelObj.GetComponent<Image>();
        bgImage.color = new Color(0.2f, 0.2f, 0.2f, 0.8f);

        VerticalLayoutGroup vLayout = subPanelObj.GetComponent<VerticalLayoutGroup>();
        vLayout.padding = new RectOffset(10, 10, 10, 10);
        vLayout.spacing = 10;
        vLayout.childControlHeight = true;
        vLayout.childControlWidth = true;

        // Title
        GameObject titleObj = new GameObject("Title", typeof(RectTransform), typeof(TextMeshProUGUI));
        titleObj.transform.SetParent(subPanelObj.transform, false);

        TMP_Text titleText = titleObj.GetComponent<TMP_Text>();
        titleText.text = "Square Info";
        titleText.font = titleFont;
        titleText.fontSize = 28;
        titleText.color = Color.white;
        titleText.alignment = TextAlignmentOptions.Center;

        LayoutElement titleLayout = titleObj.AddComponent<LayoutElement>();
        titleLayout.preferredHeight = 40;

        // Grid container
        GameObject gridObj = new GameObject("ImageGrid", typeof(RectTransform), typeof(GridLayoutGroup), typeof(LayoutElement));
        gridObj.transform.SetParent(subPanelObj.transform, false);
        imageGridTransform = gridObj.GetComponent<RectTransform>();
        gridLayout = gridObj.GetComponent<GridLayoutGroup>();

        LayoutElement gridLayoutElem = gridObj.GetComponent<LayoutElement>();
        gridLayoutElem.flexibleHeight = 1;
        gridLayoutElem.flexibleWidth = 1;

        gridLayout.spacing = new Vector2(10, 10);
        gridLayout.childAlignment = TextAnchor.UpperCenter;
        //gridLayout.childForceExpandWidth = false;
        //gridLayout.childForceExpandHeight = false;
    }

    public void RefreshImageGrid()
    {
        //Debug.Log(panelPieces.Count + " " + squareImages.Count);
        if (imageGridTransform == null || gridLayout == null) return;

        foreach (Transform child in imageGridTransform)
        {
            Destroy(child.gameObject);
        }

        if (squareImages == null) return;
        int count = squareImages.Count;
        if (count == 0) return;

        int rows = Mathf.CeilToInt(Mathf.Sqrt(count));
        int cols = Mathf.CeilToInt((float)count / rows);
        gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayout.constraintCount = cols;

        float panelWidth = imageGridTransform.rect.width;
        float panelHeight = imageGridTransform.rect.height;

        float spacingX = gridLayout.spacing.x * (cols - 1);
        float spacingY = gridLayout.spacing.y * (rows - 1);

        float cellWidth = (panelWidth - spacingX) / cols;
        float cellHeight = (panelHeight - spacingY) / rows;

        gridLayout.cellSize = new Vector2(cellWidth, cellHeight);

        for (int i = 0; i < squareImages.Count; i++)
        {
            Sprite baseSprite = squareImages[i];
            GameObject container = new GameObject($"PieceContainer_{i}", typeof(RectTransform));
            Image bgImage = container.AddComponent<Image>();
            bgImage.color = new Color32(0, 0, 0, 0);
            if (HelperFunctions.checkState(panelPieces[i], "Frozen"))
            {
                bgImage.color = new Color32(189, 222, 236, 255);
            }

            container.transform.SetParent(imageGridTransform, false);

            RectTransform containerRect = container.GetComponent<RectTransform>();
            containerRect.anchorMin = Vector2.zero;
            containerRect.anchorMax = Vector2.one;
            containerRect.offsetMin = Vector2.zero;
            containerRect.offsetMax = Vector2.zero;

            // Base Image
            GameObject baseImageObj = new GameObject("BaseImage", typeof(Image));
            baseImageObj.name = panelPieces.Count > i ? panelPieces[i].name : "Pass";
            baseImageObj.transform.SetParent(container.transform, false);

            Image baseImage = baseImageObj.GetComponent<Image>();
            baseImage.sprite = baseSprite;
            baseImage.preserveAspect = true;

            RectTransform baseRect = baseImageObj.GetComponent<RectTransform>();
            baseRect.anchorMin = Vector2.zero;
            baseRect.anchorMax = Vector2.one;
            baseRect.offsetMin = Vector2.zero;
            baseRect.offsetMax = Vector2.zero;

            // Click handler
            EventTrigger e = baseImageObj.AddComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((eventData) =>
            {
                HelperFunctions.clickedSidePanel(eventData, this);
            });
            e.triggers.Add(entry);

            // Overlay Ability Image (bottom-right corner)
            if (panelPieces != null && i < panelPieces.Count && !string.IsNullOrEmpty(panelPieces[i].ability))
            {
                string[] abilityNames = panelPieces[i].ability.Split("-");
                int color = panelPieces[i].color;
                int gridColumns = 3;
                float gridSpacing = 5f;
                float size = 32f;

                int j = 0;
                foreach (string abilityName in abilityNames)
                {
                    j++;

                    //Validate Ability
                    if (abilityName == "Vomit")
                    {
                        if (panelPieces[i].storage != null && panelPieces[i].storage.Count < 1)
                        {
                            continue;
                        }
                        else if (panelPieces[i].storage == null)
                        {
                            continue;
                        }
                    }
                    else if (abilityName == "CastleLeft")
                    {
                        if (!HelperFunctions.checkCanCastle(color, -1))
                        {
                            continue;
                        }
                    }
                    else if (abilityName == "CastleRight")
                    {
                        Debug.Log("1");
                        if (!HelperFunctions.checkCanCastle(color, 1))
                        {
                            continue;
                        }
                    }
                    else if (abilityName == "Unfreeze")
                    {
                        if (!HelperFunctions.checkState(panelPieces[i], "Frozen"))
                        {
                            continue;
                        }
                    }
                    else if (abilityName == "Freeze")
                    {
                        if (!HelperFunctions.isPieceSurroundingColor(panelPieces[i], panelPieces[i].color * -1))
                        {
                            continue;
                        }
                    }
                    else if (abilityName == "Spawn")
                    {
                        if (panelPieces[i].numSpawns <= 0)
                        {
                            continue;
                        }

                        if (!HelperFunctions.areSurroundingSquaresFull(panelPieces[i]))
                        {
                            continue;
                        }
                    }
                    else if (abilityName == "Spit")
                    {
                        if (panelPieces[0].storage != null && panelPieces[0].storage.Count < 1)
                        {
                            continue;
                        }
                    }
                    else if (abilityName == "None" || abilityName == "")
                    {
                        continue;
                    }
                    else if (abilityName == "Dematerialize")
                    {
                        if (HelperFunctions.checkState(panelPieces[i], "Dematerialized"))
                        {
                            continue;
                        }
                    }
                    else if (abilityName == "Materialize")
                    {
                        if (!HelperFunctions.checkState(panelPieces[i], "Dematerialized"))
                        {
                            continue;
                        }
                    }
                    else if (abilityName == "Split")
                    {
                        //You can always split
                    }

                    Debug.Log(abilityName + " -> Passed");

                    Sprite abilitySprite = Resources.Load<Sprite>($"Ability/{abilityName}");

                    if (abilitySprite != null)
                    {
                        GameObject overlayObj = new GameObject(abilityName + "-" + panelPieces[i].name, typeof(Image));
                        overlayObj.transform.SetParent(container.transform, false);

                        Image overlayImg = overlayObj.GetComponent<Image>();
                        overlayImg.sprite = abilitySprite;
                        overlayImg.preserveAspect = true;

                        RectTransform overlayRect = overlayObj.GetComponent<RectTransform>();

                        overlayRect.anchorMin = new Vector2(1, 0);
                        overlayRect.anchorMax = new Vector2(1, 0);
                        overlayRect.pivot = new Vector2(1, 0);

                        int row = (j - 1) / gridColumns;
                        int column = (j - 1) % gridColumns;

                        float xPos = -column * (size + gridSpacing);
                        float yPos = row * (size + gridSpacing);

                        overlayRect.anchoredPosition = new Vector2(xPos, yPos);
                        overlayRect.sizeDelta = new Vector2(size, size);

                        EventTrigger eAbility = overlayObj.AddComponent<EventTrigger>();
                        EventTrigger.Entry entryAbility = new EventTrigger.Entry();
                        entryAbility.eventID = EventTriggerType.PointerClick;
                        entryAbility.callback.AddListener((eventData) =>
                        {
                            HelperFunctions.clickedAbility(eventData, this, abilityName);
                        });
                        eAbility.triggers.Add(entryAbility);
                    }
                    else
                    {
                        Debug.LogWarning($"Ability image not found for: {abilityName}");
                    }
                }

            }
        }
    }

    public void Initialize()
    {
        RectTransform rect = GetComponent<RectTransform>();
        CreateSidePanel(rect);
    }
}
