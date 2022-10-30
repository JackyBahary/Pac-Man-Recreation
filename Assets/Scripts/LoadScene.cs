using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadScene:MonoBehaviour
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI time;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            const string saveScore = "userScore";
            const string saveTimeMinute = "userTimeMinute";
            const string saveTimeSecond = "userTimeSecond";
            const string saveTimeMillisecond = "userTimeMillisecond";
            int userSavedScore = PlayerPrefs.GetInt(saveScore);
            int userSavedMinute = PlayerPrefs.GetInt(saveTimeMinute);
            int userSavedSecond = PlayerPrefs.GetInt(saveTimeSecond);
            int userSavedMillisecond = PlayerPrefs.GetInt(saveTimeMillisecond);
            score.text = "High Score " + userSavedScore;
            time.text = "Time " + userSavedMinute.ToString("00") + ":" + userSavedSecond.ToString("00") + ":" + userSavedMillisecond.ToString("00");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Level1ButtonClicked()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
        PacStudentController.gameTimerActive = true;
    }

    public void ExitButtonClicked()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
