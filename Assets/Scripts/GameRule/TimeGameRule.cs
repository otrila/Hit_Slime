using System;
using UnityEngine;

public class TimeGameRule : GameRule
{
    [Header("Time:")]
    [SerializeField] private float _timerGame = 120;

    private void Update() => Timer(() => CheckScore());

    private void Timer(Action timerEndCallback)
    {
        if (!_isPlaying)
            return;

        _timerGame -= Time.deltaTime;

        _mediator?.SetRule($"Time: {_timerGame.ToString("0")}");

        if (_timerGame <= 0)
        {
            timerEndCallback.Invoke();
        }
    }
}
