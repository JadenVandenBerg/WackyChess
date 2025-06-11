using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Photon.Pun;
using System;

public class createBoard : MonoBehaviour
{
    public Sprite sprite;
    public GameObject ParentPanel;
    public GameObject sideUI;
    public GameObject backgroundUI;
    public String isOnline;

    [Header("Profile UI")]
    public GameObject TopProfilePanel;
    public GameObject BottomProfilePanel;

    void Start()
    {
        float parentHeight = ParentPanel.GetComponent<RectTransform>().sizeDelta.y;
        float boardSize = parentHeight * 0.8f;
        float verticalOffset = (parentHeight - boardSize) / 2;

        for (int i = 1; i <= 8; i++)
        {
            for (int j = 1; j <= 8; j++)
            {
                GameObject go;
                if (string.IsNullOrEmpty(isOnline))
                {
                    go = new GameObject();
                }
                else
                {
                    go = PhotonNetwork.Instantiate("Empty", Vector2.zero, Quaternion.identity);
                }

                go.name = i.ToString() + j.ToString();
                Image s = go.AddComponent<Image>();
                BoxCollider2D b = go.AddComponent<BoxCollider2D>();
                EventTrigger e = go.AddComponent<EventTrigger>();
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerClick;
                entry.callback.AddListener((eventData) =>
                {
                    HelperFunctions.clicked(eventData);
                });
                e.triggers.Add(entry);

                RectTransform rect = go.GetComponent<RectTransform>();
                rect.SetParent(ParentPanel.transform);
                rect.anchorMax = Vector2.zero;
                rect.anchorMin = Vector2.zero;
                rect.pivot = Vector2.zero;

                rect.sizeDelta = new Vector2(boardSize / 8, boardSize / 8);
                rect.anchoredPosition = new Vector2((i - 1) * rect.sizeDelta.y, (j - 1) * rect.sizeDelta.y + verticalOffset);

                s.sprite = sprite;
                s.color = (i + j) % 2 == 0 ? new Color32(14, 115, 34, 255) : new Color32(131, 199, 145, 255);
            }
        }

        SetupProfilePanels();
    }

    void SetupProfilePanels()
    {
        // Setup top panel (player 1)
        if (TopProfilePanel != null)
        {
            RectTransform topRect = TopProfilePanel.GetComponent<RectTransform>();
            topRect.SetParent(ParentPanel.transform);
            topRect.anchorMin = new Vector2(0, 1);
            topRect.anchorMax = new Vector2(1, 1);
            topRect.pivot = new Vector2(0.5f, 1);
            topRect.sizeDelta = new Vector2(0, ParentPanel.GetComponent<RectTransform>().sizeDelta.y * 0.1f);
            topRect.anchoredPosition = Vector2.zero;
        }

        // Setup bottom panel (player 2)
        if (BottomProfilePanel != null)
        {
            RectTransform botRect = BottomProfilePanel.GetComponent<RectTransform>();
            botRect.SetParent(ParentPanel.transform);
            botRect.anchorMin = new Vector2(0, 0);
            botRect.anchorMax = new Vector2(1, 0);
            botRect.pivot = new Vector2(0.5f, 0);
            botRect.sizeDelta = new Vector2(0, ParentPanel.GetComponent<RectTransform>().sizeDelta.y * 0.1f);
            botRect.anchoredPosition = Vector2.zero;
        }
    }
}
