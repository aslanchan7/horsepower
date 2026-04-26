using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    public static Leaderboard Instance;
    public List<LeaderboardEntry> leaderboardEntries = new();
    private float fieldLength;
    [SerializeField] Transform finishLine;
    private float maxPosX;
    [SerializeField] float startX = 350f;
    [SerializeField] float endX = 880f;
    [SerializeField] GameObject horseIndicatorPrefab;
    [SerializeField] RectTransform fieldUI;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        // Field length is finish line - initial position of horse
        fieldLength = finishLine.position.x - leaderboardEntries[0].horseTransform.position.x;

        maxPosX = fieldUI.sizeDelta.x - horseIndicatorPrefab.GetComponent<RectTransform>().sizeDelta.x;

        foreach (var entry in leaderboardEntries)
        {
            GameObject instantiated = Instantiate(horseIndicatorPrefab, fieldUI);
            instantiated.GetComponent<Image>().sprite = entry.sprite;
            RectTransform rt = instantiated.GetComponent<RectTransform>();
            rt.localPosition = new(startX, 0);
        }
    }

    void Update()
    {
        for (int i = 0; i < fieldUI.childCount; i++)
        {
            RectTransform rt = fieldUI.GetChild(i).GetComponent<RectTransform>();
            float percentTravelled = Mathf.Clamp01(leaderboardEntries[i].horseTransform.localPosition.x / fieldLength);
            float x = percentTravelled * (endX - startX) + startX;
            rt.localPosition = new(x, rt.localPosition.y, rt.localPosition.z);
        }
    }
}

[Serializable]
public class LeaderboardEntry : IComparer
{
    public Transform horseTransform;
    public int id;
    public Color color;
    public Sprite sprite;

    public int Compare(object x, object y)
    {
        Transform xTrans = ((LeaderboardEntry)x).horseTransform;
        Transform yTrans = ((LeaderboardEntry)y).horseTransform;
        if (xTrans.position.x == yTrans.position.x) return 0;
        else if (xTrans.position.x > yTrans.position.x) return 1;
        else return -1;
    }
}