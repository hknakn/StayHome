using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{

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

    void OnEnable()
    {
        InitColor();
        level = 0;
        currentAmount = 0;
        bar_fill.fillAmount = currentAmount;
        UpdateLevel(level);
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

    public void UpdateProgress(float amount, float duration = 0.1f)
    {
        if (routine != null)
        {
            StopCoroutine(routine);
        }

        float target = currentAmount + amount;
        routine = StartCoroutine(FillRoutine(target, duration));
    }

    private IEnumerator FillRoutine(float target, float duration)
    {
        float time = 0;
        float tempAmount = currentAmount;
        float diff = target - tempAmount;
        currentAmount = target;

        while (time < duration)
        {
            time += Time.deltaTime;
            float percent = time / duration;
            bar_fill.fillAmount = tempAmount + diff * percent;
            yield return null;
        }

        if (currentAmount >= 1)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        UpdateLevel(level + 1);
        UpdateProgress(-1f, 0.2f);
    }

    private void UpdateLevel(int level)
    {
        this.level = level;
        Text_1.text = this.level.ToString();
        Text_2.text = (this.level + 1).ToString();
    }
}