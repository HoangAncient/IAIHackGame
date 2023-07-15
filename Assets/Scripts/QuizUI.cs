using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizUI : MonoBehaviour
{
    public static QuizUI Instance;
    public Image questionImg;                     //image component to show image
    public UnityEngine.Video.VideoPlayer questionVideo;   //to show video
    public AudioSource questionAudio;             //audio source for audio clip
    public TMP_Text questionInfoText;                 //text to show question
    public List<Button> options;                  //options button reference
    public Color CorrectCol, WrongCol, normalCol; //color of buttons
    
    
    public float audioLength;          //store audio length
    public Question question;          //store current question data
    public bool answered = false;
    public static bool render = false;//bool to keep track if answered or not
    // Start is called before the first frame update
    void Awake()
    {
        
        Instance = this;
        for (int i = 0; i < 2; i++)
        {
            Button localBtn = options[i];
            localBtn.onClick.AddListener(() => OnClick(localBtn));
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
        for (int i = 0; i < 2; i++)
        {
            Debug.Log("Loasf");
            Button localBtn = options[i];
            localBtn.onClick.AddListener(() => OnClick(localBtn));
            Debug.Log("Loasf2");
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
            options[i].image.color = normalCol; //set color of button to normal
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

        //if answered is false

        //set answered true
        answered = true;
        //get the bool value
        Debug.Log("popit");
        bool val = QuizManager.Instance.Answer(btn.name);

        //if its true
        if (val)
        {
            //set color to correct
            btn.image.color = CorrectCol;
            Debug.Log("dung1");
            render = false;

        }
        else
        {
            //else set it to wrong color
            btn.image.color = WrongCol;
            Debug.Log("sai1");
            render = false;

        }
        answered = false;
        Invoke("DeleteScene", 0.6f);
        spr.Instance.UpdateGame();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
