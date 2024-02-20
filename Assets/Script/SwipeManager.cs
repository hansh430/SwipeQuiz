
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Collections;

public class SwipeManager : MonoBehaviour
{
    #region All Variables and dependencies
    public static SwipeManager instance;
    public RectTransform[] questionCard;
    public RectTransform leftTarget;
    public RectTransform rightTarget;
    public RectTransform centerTarget;
    public TMP_Text qno;
    public string answer;
    public float x1;
    public float x2;
    public float moveDirection = 0;
    public static int questionCount = 0;
    public bool isUnanswered;
    public GameObject response;
    #endregion

    #region MonoBehaviour Methods
    void Awake()
    {
        instance = this;
    }


    void Update()
    {
        qno.text = "Q : " + (questionCount + 1);
#if UNITY_EDITOR
        PCSwipeInput();
#endif
#if UNITY_ANDROID
        MobileInput();
#endif
    }
    #endregion


    #region Mobile Input System
    public void MobileInput()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                x1 = touch.position.x;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                x2 = touch.position.x;
                if (x1 - x2 > 150f)
                {
                    answer = "False";
                    moveDirection = 1;
                    SwipeQuestionCards(questionCount);
                }
                if (x2 - x1 > 150f)
                {
                    answer = "True";
                    moveDirection = 2;
                    SwipeQuestionCards(questionCount);
                }
            }
        }
    }
    #endregion

    #region PC Input System
    public void PCSwipeInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            x1 = Input.mousePosition.x;
        }
        if (Input.GetMouseButtonUp(0))
        {
            x2 = Input.mousePosition.x;
            if (x1 > x2)
            {
                answer = "False";
                moveDirection = 1;
                SwipeQuestionCards(questionCount);
            }
            if (x2 > x1)
            {
                answer = "True";
                moveDirection = 2;
                SwipeQuestionCards(questionCount);

            }
        }

    }
    #endregion

    #region Methods for Functionality

    public void SwipeQuestionCards(int qNo)
    {
        if (moveDirection == 1 && qNo < questionCard.Length)
        {
            StartCoroutine(ResponseCheck(qNo));
            questionCard[qNo].DOMove(leftTarget.position, 1.5f).OnComplete(() => OnTargetMoved(qNo));
            questionCount++;
        }
        if (moveDirection == 2 && qNo < questionCard.Length)
        {
            StartCoroutine(ResponseCheck(qNo));
            questionCard[qNo].DOMove(rightTarget.position, 1.5f).OnComplete(() => OnTargetMoved(qNo));
            questionCount++;
        }
    }

    public void ForUnAnsweredQuestion(int qNo)
    {
        StartCoroutine(ForUnansweredQues());
        questionCard[qNo].DOMove(leftTarget.position, 1.5f).OnComplete(() => OnTargetMoved(qNo));
        questionCount++;
    }
    public void OnTargetMoved(int qNo)
    {
        questionCard[qNo].gameObject.SetActive(false);
        TimerController.instance.ResetTimer();
    }

    public void AutoNextQuestion()
    {
        if (questionCount < 10)
        {
            isUnanswered = true;
            moveDirection = 1;
            ForUnAnsweredQuestion(questionCount);
        }
    }

    #endregion

    #region ResponseCheck Coroutines

    public IEnumerator ResponseCheck(int qNo)
    {
        if (questionCard[qNo].gameObject.CompareTag(answer))
        {
            response.transform.GetChild(0).GetComponent<TMP_Text>().text = "Correct";
            GameManager.gm.correctCount++;
            response.transform.GetChild(0).GetComponent<TMP_Text>().color = UnityEngine.Color.green;
            response.gameObject.SetActive(true);
        }
        else
        {
            response.transform.GetChild(0).GetComponent<TMP_Text>().text = "Incorrect";
            GameManager.gm.incorrectCount++;
            response.transform.GetChild(0).GetComponent<TMP_Text>().color = UnityEngine.Color.red;
            response.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(0.5f);
        response.gameObject.SetActive(false);
    }

    public IEnumerator ForUnansweredQues()
    {
        response.transform.GetChild(0).GetComponent<TMP_Text>().text = "Incorrect";
        response.transform.GetChild(0).GetComponent<TMP_Text>().color = UnityEngine.Color.red;
        response.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        response.gameObject.SetActive(false);
    }
    #endregion
}
