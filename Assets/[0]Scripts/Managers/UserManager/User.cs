using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class User
{
    private string _userID;
    private int _score;
    private int _bestScore;

    public string UserID
    {
        get { return _userID; }
        set { _userID = !String.IsNullOrWhiteSpace(value) && !String.IsNullOrEmpty(value) ? value : "Code name: 47"; }
    }
    public int Score
    {
        get { return _score; }
        set { if(value >= 0) _score = value;}
    }
    
    public int BestScore
    {
        get { return _bestScore; }
        set { if(value >= 0) _bestScore = value;}
    }
    

    public User()
    {
        _userID = Guid.NewGuid().ToString("N").Substring(0, 6);
        _score = 0;
    }

    public User(string id)
    {
        _userID = id;
        _score = 0;
    }

    public void Save()
    {
        SetUpBestScore();
        PlayerPrefs.SetString("_userID", _userID);
        PlayerPrefs.SetInt("_bestScore", _bestScore);
    }

    public void Load()
    {
        UserID = PlayerPrefs.GetString("_userID") != null ? PlayerPrefs.GetString("_userID") : UserID;
        _bestScore = PlayerPrefs.GetInt("_bestScore");
    }


    public int AddScore(int scores = 1)
    {
        _score += scores;
        return _score;
    }

    public int SetUpBestScore()
    {
        _bestScore = _bestScore < _score ? _score : _bestScore;
        return _bestScore;
    }

    public void ResetScore()
    {
        _score = 0;
    }
}
