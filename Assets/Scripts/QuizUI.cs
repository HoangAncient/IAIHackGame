using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizUI : MonoBehaviour
{
    public int TimesCallButton = 0;

    public int timeBeforeUpdate = 0;

    public int timeAfterUpdate = 0;
    public static QuizUI Instance;
    public Image questionImg;                     //image component to show image
    public UnityEngine.Video.VideoPlayer questionVideo;   //to show video
    public AudioSource questionAudio;             //audio source for audio clip
    public TMP_Text questionInfoText;                 //text to show question
    public List<Button> options;                  //options button reference
    public Color CorrectCol, WrongCol, normalCol; //color of buttons
    public TMP_Text formfillInstruction;
    public InputField formfillChoice;
    public Button Submit;

    public float audioLength;          //store audio length
    public Question question;          //store current question data
    public bool answered = false;
    public static bool render = false;//bool to keep track if answered or not
    // Start is called before the first frame update
    void Awake()
    {
        
        Instance = this;
        for (int i = 0; i < 4; i++)
        {
            // Debug.Log("Loasf");
            Button localBtn = options[i];
            localBtn.enabled = true;
            options[i].image.color = new Color(93, 93, 69);

            if (!answered)
            {
                localBtn.onClick.AddListener(() => OnClick(localBtn));
                Debug.Log(i);
            }

            // localBtn.onClick.AddListener(() => OnClick(localBtn));
            // Debug.Log("Loasf2");
            // localBtn.enabled = false;
            // localBtn.enabled = true;
        }


        if (!render)
        {
            questionInfoText.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
        }
        //add the listner to all the buttons


    }





    public void SetQuestion(Question question)
    {


        questionInfoText.transform.parent.gameObject.transform.parent.gameObject.SetActive(true);
        Debug.Log("Question.option.count = " + question.options.Count);
       
        switch (question.renderType)
        {
            case QuestionRenderType.MultipleType2:
                Debug.Log("MC2");
                options[0].transform.gameObject.SetActive(true);
                options[1].transform.gameObject.SetActive(true);
                options[2].transform.gameObject.SetActive(false);
                options[3].transform.gameObject.SetActive(false);
                options[0].enabled = true;
                options[1].enabled = true;
                options[2].enabled = false;
                options[3].enabled = false;
                
                formfillInstruction.transform.parent.gameObject.SetActive(false);
                break;
            case QuestionRenderType.MultipleType3:
                Debug.Log("MC3");
                options[0].transform.gameObject.SetActive(true);
                options[1].transform.gameObject.SetActive(true);
                options[2].transform.gameObject.SetActive(true);
                options[3].transform.gameObject.SetActive(false);
                options[3].enabled = false;
                options[0].enabled = true;
                options[1].enabled = true;
                options[2].enabled = true;
                
                
                formfillInstruction.transform.parent.gameObject.SetActive(false);
                break;
            case QuestionRenderType.MultipleType4:
                Debug.Log("MC4");
                options[0].enabled = true;
                options[1].enabled = true;
                options[2].enabled = true;
                options[3].enabled = true;
                options[0].transform.gameObject.SetActive(true);
                options[1].transform.gameObject.SetActive(true);
                options[2].transform.gameObject.SetActive(true);
                options[3].transform.gameObject.SetActive(true);                
                formfillInstruction.transform.parent.gameObject.SetActive(false);
                break;
            case QuestionRenderType.Formfill:
                Debug.Log("formfill");
                options[0].transform.gameObject.SetActive(false);
                options[1].transform.gameObject.SetActive(false);
                options[2].transform.gameObject.SetActive(false);
                options[3].transform.gameObject.SetActive(false);
                options[0].enabled = false;
                options[1].enabled = false;
                options[2].enabled = false;
                options[3].enabled = false;
                
                formfillInstruction.transform.parent.gameObject.SetActive(true);
                formfillInstruction.transform.gameObject.SetActive(true);
                formfillChoice.transform.gameObject.SetActive(true);
                formfillInstruction.text = "Hãy điền câu trả lời vào ô bên cạnh:";
                
                formfillChoice.onSubmit.AddListener(e =>
                {
                    if (formfillChoice.isFocused)
                    {
                        TimesCallButton += 1;
                        Debug.Log("Times Onclick = " + TimesCallButton);
                        Debug.Log("Enter");
                        //if answered is false

                        //set answered true
                        // answered = true;
                        //get the bool value
                        bool val = QuizManager.Instance.Answer(formfillChoice.text.ToLower());

                        //if its true
                        if (val)
                        {


                            render = false;

                        }
                        else
                        {
                            //else set it to wrong color


                            render = false;

                        }
                        answered = true;


                        CollisionScript.animator.SetTrigger("Attack");
                        Time.timeScale = 1f;
                        // answered = false;
                        Invoke("DeleteScene", 0.6f);

                        spr.Instance.Create();
                    }

                });

                break;
        }
        
        //set the question
        this.question = question;
        //check for questionType
        switch (question.questionType)
        {
            
            case QuestionType.TEXT:
                questionImg.transform.parent.gameObject.SetActive(false);   //deactivate image holder
                break;
            case QuestionType.IMAGE:
                questionImg.transform.parent.gameObject.SetActive(true);    //activate image holder
                questionVideo.transform.gameObject.SetActive(false);        //deactivate questionVideo
                questionImg.transform.gameObject.SetActive(true);           //activate questionImg
                questionAudio.transform.gameObject.SetActive(false);        //deactivate questionAudio

                questionImg.sprite = question.questionImage;                //set the image sprite
                break;
            case QuestionType.AUDIO:
                questionVideo.transform.parent.gameObject.SetActive(true);  //activate image holder
                questionVideo.transform.gameObject.SetActive(false);        //deactivate questionVideo
                questionImg.transform.gameObject.SetActive(false);          //deactivate questionImg
                questionAudio.transform.gameObject.SetActive(true);         //activate questionAudio

                audioLength = question.audioClip.length;                    //set audio clip
                StartCoroutine(PlayAudio());                                //start Coroutine
                break;
            case QuestionType.VIDEO:
                questionVideo.transform.parent.gameObject.SetActive(true);  //activate image holder
                questionVideo.transform.gameObject.SetActive(true);         //activate questionVideo
                questionImg.transform.gameObject.SetActive(false);          //deactivate questionImg
                questionAudio.transform.gameObject.SetActive(false);        //deactivate questionAudio

                questionVideo.clip = question.videoClip;                    //set video clip
                questionVideo.Play();                                       //play video
                break;
        }

        questionInfoText.text = question.questionInfo;                      //set the question text

        //suffle the list of options
        List<string> ansOptions = ShuffleList.ShuffleListItems<string>(question.options);

        //assign options to respective option buttons
        for (int i = 0; i < question.options.Count; i++)
        {
            Debug.Log(ansOptions[i]);
            //set the child text
            options[i].GetComponentInChildren<TMP_Text>().text = ansOptions[i];
            options[i].name = ansOptions[i];    //set the name of button
            options[i].image.color = new Color(93, 93, 69);

        }

        answered = false;

    }

    IEnumerator PlayAudio()
    {
        //if questionType is audio
        if (question.questionType == QuestionType.AUDIO)
        {
            //PlayOneShot
            questionAudio.PlayOneShot(question.audioClip);
            //wait for few seconds
            yield return new WaitForSeconds(audioLength + 0.5f);
            //play again
            StartCoroutine(PlayAudio());
        }
        else //if questionType is not audio
        {
            //stop the Coroutine
            StopCoroutine(PlayAudio());
            //return null
            yield return null;
        }
    }

    void DeleteScene()
    {
        questionInfoText.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
    }

    void OnClick(Button btn)
    {
        // btn.enabled = false;

        TimesCallButton += 1;
        Debug.Log("Times Onclick = " + TimesCallButton);
        //if answered is false

        //set answered true
        // answered = true;
        //get the bool
        Debug.Log(btn.name);
        bool val = QuizManager.Instance.Answer(btn.name);

        //if its true
        if (val)
        {
            //set color to correct
            btn.image.color = CorrectCol;
      
            render = false;
            // PointCalculator.currentPoint += 10;

        }
        else
        {
            //else set it to wrong color
            btn.image.color = WrongCol;
           
            render = false;

        }
        answered = true;
        // answered = false;
        // Invoke("DeleteScene", 0.6f);
        // timeBeforeUpdate += 1;
        // Debug.Log("Number of times BEFORE Update = " + timeBeforeUpdate);
        // spr.Instance.UpdateGame();
        // // btn.enabled = false;
        // timeAfterUpdate += 1;
        // Debug.Log("Number of times AFTER Update = " + timeAfterUpdate);
        // Debug.Log("CURRENT POINT = " + PointCalculator.currentPoint);

        CollisionScript.animator.SetTrigger("Attack");
        Time.timeScale = 1f;
        // answered = false;
        Invoke("DeleteScene", 0.6f);
   
        spr.Instance.Create();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
