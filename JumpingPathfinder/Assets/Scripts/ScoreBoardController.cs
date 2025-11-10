using TMPro;
using UnityEngine;

public class ScoreBoardController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI blueScoreText;
    private float blueScore;

    [SerializeField] private TextMeshProUGUI redScoreText;
    private float redScore;

    private float timeRemaining = 120;
    [SerializeField] private TextMeshProUGUI timeText;

    private bool roundRunning = true;

    
    void Start()
    {
        
    }

    void Update()
    {
        RunGame();
    }

    private void RunGame()
    {
        if(roundRunning)
        {
            UpdateTime();
            UpdateScores();
        }
    }

    private void UpdateTime()
    {
            timeRemaining -= Time.deltaTime;
            int minutes = (int)timeRemaining / 60;
            int seconds = (int)timeRemaining % 60;
            if(minutes > 0)
            {
                timeText.text = minutes + ":" + seconds.ToString("D2");
            }
            else
            {
                timeText.text = seconds.ToString();
            }

        
    }
    private void UpdateScores()
    {
        if(!TagGameController.GetColorTagged(PlayerColor.Red))
        {
            redScore += Time.deltaTime * 2;
            redScoreText.text = ((int)redScore).ToString();
        }
        if (!TagGameController.GetColorTagged(PlayerColor.Blue))
        {
            blueScore += Time.deltaTime * 2;
            blueScoreText.text = ((int)blueScore).ToString();
        }
    }
}
