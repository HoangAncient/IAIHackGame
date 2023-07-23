using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Web : MonoBehaviour
{
    public GameObject LoginPanel;
    public GameObject RegisterPanel;

    void Start()
    {
        RegisterPanel.SetActive(false);
        LoginPanel.SetActive(true);
        // StartCoroutine(GetDate());
        // StartCoroutine(GetUsers());
        // StartCoroutine(Login("user3", "123"));
        // StartCoroutine(RegisterUser("user3", "789"));
        

    }

    // IEnumerator GetDate()
    // {
    //     string uri = "http://localhost:8080/UnityBackendTutorial/GetDate.php";
    //     using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
    //     {
    //         // Request and wait for the desired page.
    //         yield return webRequest.SendWebRequest();

    //         string[] pages = uri.Split('/');
    //         int page = pages.Length - 1;

    //         switch (webRequest.result)
    //         {
    //             case UnityWebRequest.Result.ConnectionError:
    //             case UnityWebRequest.Result.DataProcessingError:
    //                 Debug.LogError(pages[page] + ": Error: " + webRequest.error);
    //                 break;
    //             case UnityWebRequest.Result.ProtocolError:
    //                 Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
    //                 break;
    //             case UnityWebRequest.Result.Success:
    //                 Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
    //                 break;
    //         }
    //     }
    // }

    // IEnumerator GetUsers()
    // {
    //     string uri = "http://localhost:8080/UnityBackendTutorial/GetUsers.php";
    //     using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
    //     {
    //         // Request and wait for the desired page.
    //         yield return webRequest.SendWebRequest();

    //         string[] pages = uri.Split('/');
    //         int page = pages.Length - 1;

    //         switch (webRequest.result)
    //         {
    //             case UnityWebRequest.Result.ConnectionError:
    //             case UnityWebRequest.Result.DataProcessingError:
    //                 Debug.LogError(pages[page] + ": Error: " + webRequest.error);
    //                 break;
    //             case UnityWebRequest.Result.ProtocolError:
    //                 Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
    //                 break;
    //             case UnityWebRequest.Result.Success:
    //                 Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
    //                 break;
    //         }
    //     }
    // }

    // IEnumerator Login(string username, string password)
    // {
    //     using (UnityWebRequest www = UnityWebRequest.Post("http://localhost:8080/UnityBackendTutorial/Login.php", "{ \"field1\": 1, \"field2\": 2 }", "application/json"))
    //     {
    //         yield return www.SendWebRequest();

    //         if (www.result != UnityWebRequest.Result.Success)
    //         {
    //             Debug.Log(www.error);
    //         }
    //         else
    //         {
    //             Debug.Log("Form upload complete!");
    //         }
    //     }
    // }

    public IEnumerator Login(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost:8080/unitygame/Login_Register/Login.php", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Debug.Log("Form upload complete!");
            Debug.Log(www.downloadHandler.text);
        }
    }

    public void LoginToRegister() {
        LoginPanel.SetActive(false);
        RegisterPanel.SetActive(true);
    }

    public void RegisterToLogin() {
        LoginPanel.SetActive(true);
        RegisterPanel.SetActive(false);
    }


    public IEnumerator RegisterUser(string lastname, string firstname, string birthday, string username, string password, string job)
    {
        WWWForm form = new WWWForm();

        form.AddField("registerLastName", lastname);
        form.AddField("registerFirstName", firstname);
        form.AddField("registerBirthday", birthday);
        form.AddField("registerUser", username);
        form.AddField("registerPass", password);
        form.AddField("registerJob", job);


        UnityWebRequest www = UnityWebRequest.Post("http://localhost:8080/unitygame/Login_Register/RegisterUser.php", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Debug.Log("Form upload complete!");
            Debug.Log(www.downloadHandler.text);
        }
    }
}
