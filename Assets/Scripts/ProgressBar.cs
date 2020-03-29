using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public GameObject player;
    public Vector3 endLine;
    public Text Text_1;
    public Text Text_2;
    public Image bar_fill;
    public Image bar_outline;
    public Image circle_1;
    public Image circle_2;
    public Color color;
    public Color background_color;
    public int level = 0;
    private float currentAmount = 0;
    private Coroutine routine;
    float maxDistance;

    private void Start()
    {
        maxDistance = getDistance();
    }

    private void Update()
    {
        bar_fill.fillAmount = 1 - (getDistance() / maxDistance);
    }

    float getDistance()
    {
        return Vector3.Distance(player.transform.position, endLine);
    }

    void OnEnable()
    {
        InitColor();
        level = 0;
        currentAmount = 0;
        bar_fill.fillAmount = currentAmount;
    }

    void InitColor()
    {
        circle_1.color = color;
        circle_2.color = color;

        bar_fill.color = color;
        bar_outline.color = color;

        Text_1.color = background_color;
        Text_2.color = color;
    }
}