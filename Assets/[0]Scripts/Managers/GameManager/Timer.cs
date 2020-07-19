using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    private float _minutes;
    private float _seconds;

    private float _timer;

    private bool _isExpired;


    public Timer()
    {
        _timer = 180.0f;
        _seconds = 0.0f;
        _minutes = (int) (_timer / 60.0f);

        _isExpired = false;
    }

    public Timer(float value)
    {
        _timer = value;
        _seconds = 0.0f;
        _minutes = (int) (_timer / 60.0f);

        _isExpired = false;
    }

    public void GoTimer()
    {
        if (_timer - Time.timeSinceLevelLoad > 0.0f)
        {
            _seconds = (int) ((_timer - Time.timeSinceLevelLoad) % 60);
            _minutes = (int) ((_timer - Time.timeSinceLevelLoad) / 60.0f);
        }
        else
        {
            if (!_isExpired) EventManager.TriggerEvent<OnTimerExpiredEvent>();
            _isExpired = true;
        }
    }

    public void SetTimer(float value)
    {
        _timer = value;
    }

    public void Reset()
    {
        SetTimer(180.0f);
    }

    public bool IsExpired()
    {
        return _isExpired;
    }

    public string GetFormattedTimer()
    {
        return String.Format("{0:00} : {1:00}", _minutes, _seconds);
    }
}