using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : Singleton<ScoreManager>
{
    private TextMeshProUGUI scoreCounterText;
    private int scoreValue;

    private void Awake()
    {
        scoreCounterText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        scoreValue = 0;
    }

    private void Update()
    {
        scoreCounterText.text = scoreValue.ToString();
    }

    private IEnumerator PulseText(int pointWorth)
    {
        float maxScaleValue = 1.2f;

        for (float i = 1f; i <= maxScaleValue; i += 0.05f)
        {
            scoreCounterText.rectTransform.localScale = Vector3.one * i;
            yield return new WaitForEndOfFrame();
        }

        scoreCounterText.rectTransform.localScale = Vector3.one * maxScaleValue;

        scoreValue += pointWorth;

        for (float i = maxScaleValue; i >= 1f; i -= 0.05f)
        {
            scoreCounterText.rectTransform.localScale = Vector3.one * i;
            yield return new WaitForEndOfFrame();
        }

        scoreCounterText.rectTransform.localScale = Vector3.one;
    }

    public void HandleScoreIncrease(int pointWorth)
    {
        StartCoroutine(PulseText(pointWorth));
    }
}
