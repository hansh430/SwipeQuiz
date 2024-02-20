using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerController : MonoBehaviour
{
    #region Variables/Dependencies
    public static TimerController instance;
    public GameObject timerBarMain;
    public Image timerBar;
    public float totalTime = 100f;
    private float reduceAmount = 10f;
    #endregion

    #region MonoBehaviour Methods

    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
         totalTime -= reduceAmount * Time.deltaTime;
         timerBar.fillAmount = totalTime / 100f;
        if (timerBar.fillAmount == 0)
        {
            ResetTimerWithQuestion();
        }
        else if(SwipeManager.questionCount==10)
        {
            timerBarMain.SetActive(false);
            GameManager.gm.UpdateQuestionCount();
        }
    }
    #endregion

    #region Public Methods
    public void ResetTimerWithQuestion()
    {
        totalTime = 100f;
        timerBar.fillAmount = totalTime;
        SwipeManager.instance.AutoNextQuestion();
    }
    public void ResetTimer()
    {
        totalTime = 100f;
        timerBar.fillAmount = totalTime;
    }

    #endregion

}
