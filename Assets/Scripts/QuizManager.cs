using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class QuizManager : MonoBehaviour
{
    public static QuizManager Instance;
    public static List<Question> questions;
    //current question data
    public static Question selectedQuestion = new();


    void Start()
    {
        Instance = this;
        if (QuizManager.questions == null)
            QuizManager.questions = new List<Question>();
    }






    // Start is called before the first frame update
    public void StartGame()
    {
        
       
       
        SelectQuestion();

    }
    public void GetData() => StartCoroutine(GetQuestions("http://localhost/unitygame/game1/GetQuestions.php"));

    public void SelectQuestion()
    {
        Debug.Log(QuizManager.questions.Count);
        //get the random number
        int val = UnityEngine.Random.Range(0, questions.Count - 1);
        Debug.Log(val);
        //set the selectedQuetion
        QuizManager.selectedQuestion = QuizManager.questions[val];
        //send the question to quizGameUI

        Debug.Log(QuizManager.selectedQuestion.questionInfo);
        QuizUI.Instance.SetQuestion(QuizManager.selectedQuestion);

        QuizManager.questions.RemoveAt(val);
        Debug.Log(QuizManager.selectedQuestion.questionInfo);
    }

    public bool Answer(string answered)
    {
        //set default to false
        bool correctAns = false;
        //if selected answer is similar to the correctAns
        Debug.Log("popup");


        if (answered == QuizManager.selectedQuestion.correctAns)
        {
            correctAns = true;
            ////call SelectQuestion method again after 1s
            //Invoke("SelectQuestion", 0.4f);
        }
        else
        {

        }
        
        //return the value of correct bool
        return correctAns;
    }
    // Update is called once per frame
    public IEnumerator GetQuestions(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    //Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    string rawresponse = webRequest.downloadHandler.text;
                    string[] questionList = rawresponse.Split('*');


                    for (int i = 0; i < questionList.Length - 1; i++)
                    {

                        string[] questionInfo = questionList[i].Split(',');


                        Question singleQuestion = new();
                        singleQuestion.questionInfo = questionInfo[0];
                        singleQuestion.correctAns = questionInfo[3];
                        singleQuestion.questionType = QuestionType.TEXT;


                        singleQuestion.options.Add(Convert.ToString(questionInfo[1]));
                        singleQuestion.options.Add(Convert.ToString(questionInfo[2]));

                        Debug.Log("question:" + singleQuestion.questionInfo + "choice1: " + singleQuestion.options[0]
                            + "choice2: " + singleQuestion.options[1] + "answer: " + singleQuestion.correctAns);

                        QuizManager.questions.Add(singleQuestion);



                    }
                    break;
            }




        }
    }
}


public class Question
{
    public string questionInfo;         //question text
    public QuestionType questionType;   //type
    public Sprite questionImage;        //image for Image Type
    public AudioClip audioClip;         //audio for audio type
    public UnityEngine.Video.VideoClip videoClip;   //video for video type
    public List<string> options = new();        //options to select
    public string correctAns;           //correct option
}

[System.Serializable]
public enum QuestionType
{
    TEXT,
    IMAGE,
    AUDIO,
    VIDEO
}
