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
            RectTransform rt = instantiated.GetComponent<RectTransform>();
            rt.localPosition = new(0, 0);
            instantiated.GetComponent<Image>().color = entry.color;
        }
    }

    void Update()
    {
        for (int i = 0; i < fieldUI.childCount; i++)
        {
            RectTransform rt = fieldUI.GetChild(i).GetComponent<RectTransform>();
            float percentTravelled = Mathf.Clamp01(leaderboardEntries[i].horseTransform.localPosition.x / fieldLength);
            float x = percentTravelled * maxPosX;
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

    public int Compare(object x, object y)
    {
        Transform xTrans = ((LeaderboardEntry)x).horseTransform;
        Transform yTrans = ((LeaderboardEntry)y).horseTransform;
        if (xTrans.position.x == yTrans.position.x) return 0;
        else if (xTrans.position.x > yTrans.position.x) return 1;
        else return -1;
    }
}