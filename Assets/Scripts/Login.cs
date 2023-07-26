using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    public InputField UsernameInput;
    public InputField PasswordInput;
    public Button LoginButton;
    public Button RegisterButton;

    // Start is called before the first frame update
    void Start()
    {
        LoginButton.onClick.AddListener(() => {
            StartCoroutine(Main.Instance.Web.Login(UsernameInput.text, PasswordInput.text));
            SceneManager.LoadScene("Scenes/Gamedemo");
        });

        RegisterButton.onClick.AddListener(() => {
            Main.Instance.Web.LoginToRegister();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
