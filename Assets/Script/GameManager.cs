
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class GameManager : MonoBehaviour
{

    #region Variables
    public static GameManager gm;
    public TMP_Text correctCountText;
    public TMP_Text incorrectCountText;
    public int incorrectCount;
    public int correctCount;

    #endregion

    #region MonoBehaviour Methods
    private void Awake()
    {
        gm = this;
    }
    #endregion

    #region Public Methods
    public void Restart()
    {
        SwipeManager.questionCount = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void MainMenu()
    {
        SwipeManager.questionCount = 0;
        SceneManager.LoadScene(0);
    }
    public void UpdateQuestionCount()
    {
        correctCountText.text = correctCount.ToString();
        incorrectCountText.text = incorrectCount.ToString();
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void OpenPanel(RectTransform panel)
    {
        panel.DOScale(new Vector3(1, 1, 1), 1f);
    }
    public void ClosePanel(RectTransform panel)
    {
        panel.DOScale(new Vector3(0, 0, 0), 1f);
    }
    #endregion
}
