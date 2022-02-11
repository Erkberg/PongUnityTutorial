using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameUI gameUI;
    public int scorePlayer1, scorePlayer2;
    public System.Action onReset;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void OnScoreZoneReached(int id)
    {
        onReset?.Invoke();

        if (id == 1)
            scorePlayer1++;

        if (id == 2)
            scorePlayer2++;

        gameUI.UpdateScores(scorePlayer1, scorePlayer2);
        gameUI.HighlightScore(id);
    }
}
