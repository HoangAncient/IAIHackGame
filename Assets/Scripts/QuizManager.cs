using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

// namespace QuizNamespace {
public class QuizManager : MonoBehaviour
{
    public static QuizManager Instance;
    public static List<Question> questions;
    //current question data
    public static Question selectedQuestion = new();

    public static int times = 0;


    void Awake()
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
        //get the random number
        int val = UnityEngine.Random.Range(0, questions.Count - 1);
        //set the selectedQuetion
        QuizManager.selectedQuestion = QuizManager.questions[val];
        //send the question to quizGameUI

        QuizUI.Instance.SetQuestion(QuizManager.selectedQuestion);

        QuizManager.questions.RemoveAt(val);
        // Debug.Log(QuizManager.selectedQuestion.questionInfo);
    }

    public bool Answer(string answered)
    {
        //set default to false
        bool correctAns = false;
        //if selected answer is similar to the correctAns
        times += 1;

        if (answered == QuizManager.selectedQuestion.correctAns)
        {
            correctAns = true;
            ////call SelectQuestion method again after 1s
            //Invoke("SelectQuestion", 0.4f);
            PointCalculator.currentPoint += 10;
            // Debug.Log("CURRENT POINT = " + PointCalculator.currentPoint);
            PointCalculator.currentStreak += 1;
        }
        else
        {
            if(!PointCalculator.StreakProtection){
                PointCalculator.currentStreak = 0;
            }
        }
        Debug.Log("CURRENT POINT = " + PointCalculator.currentPoint);
        Debug.Log("CURRENT Streak = " + PointCalculator.currentStreak);
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

                        string[] questionDetail = questionList[i].Split('@');
                        Question singleQuestion = new();
                        switch (questionDetail[0])
                        {
                            case "MC2":
                                
                                singleQuestion.questionInfo = questionDetail[1];
                                singleQuestion.correctAns = questionDetail[2];
                                singleQuestion.questionType = QuestionType.TEXT;
                                singleQuestion.renderType = QuestionRenderType.MultipleType2;
                                singleQuestion.options.Add(Convert.ToString(questionDetail[3]));
                                singleQuestion.options.Add(Convert.ToString(questionDetail[4]));
                                break;
                            case "MC3":
                                
                                singleQuestion.questionInfo = questionDetail[1];
                                singleQuestion.correctAns = questionDetail[2];
                                singleQuestion.questionType = QuestionType.TEXT;
                             
                                singleQuestion.renderType = QuestionRenderType.MultipleType3;
                                singleQuestion.options.Add(Convert.ToString(questionDetail[3]));
                                singleQuestion.options.Add(Convert.ToString(questionDetail[4]));
                                singleQuestion.options.Add(Convert.ToString(questionDetail[5]));
                                
                                break;
                            case "MC4":
                               
                                singleQuestion.questionInfo = questionDetail[1];
                                singleQuestion.correctAns = questionDetail[2];
                                singleQuestion.questionType = QuestionType.TEXT;
                                                  
                                singleQuestion.renderType = QuestionRenderType.MultipleType4;
                                singleQuestion.options.Add(Convert.ToString(questionDetail[3]));
                                singleQuestion.options.Add(Convert.ToString(questionDetail[4]));
                                singleQuestion.options.Add(Convert.ToString(questionDetail[5]));
                                singleQuestion.options.Add(Convert.ToString(questionDetail[6]));
                                break;
                            case "formfill":
                                
                                singleQuestion.questionInfo = questionDetail[1];
                                singleQuestion.correctAns = questionDetail[2];
                                singleQuestion.questionType = QuestionType.TEXT;
                                                        
                                singleQuestion.renderType = QuestionRenderType.Formfill;
                                break;
                        }




                        QuizManager.questions.Add(singleQuestion);



                    }
                    break;
            }




        }
    }
}
// }


public class Question
{
    public string questionInfo;         //question text
    public QuestionType questionType;   //type
    public Sprite questionImage;        //image for Image Type
    public AudioClip audioClip;         //audio for audio type
    public UnityEngine.Video.VideoClip videoClip;   //video for video type
    public List<string> options = new();        //options to select
    public string correctAns;           //correct option
    public QuestionRenderType renderType; //render type
}

[System.Serializable]
public enum QuestionType
{
    TEXT,
    IMAGE,
    AUDIO,
    VIDEO
}

public enum QuestionRenderType
{
    MultipleType2,
    MultipleType3,
    MultipleType4,
    Formfill
}