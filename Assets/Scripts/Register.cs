using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Register : MonoBehaviour
{
    public InputField LastNameInput;
    public InputField FirstNameInput;
    public InputField BirthdayInput;
    public InputField UsernameInput;
    public InputField PasswordInput;

    public Toggle GVCheckBox;
    public Toggle HSCheckBox;

    public Button RegisterButton;
    public Button RegisterBackToLoginButton;

    // Start is called before the first frame update
    void Start()
    {
        RegisterButton.onClick.AddListener(() => {
            if (string.Compare(UsernameInput.text, "") == 0) {
                Debug.Log("Hãy nhập tên đăng nhập");
                return;
            }

            if (string.Compare(PasswordInput.text, "") == 0) {
                Debug.Log("Hãy nhập mật khẩu");
                return;
            }

            string job = "";
            if (!GVCheckBox.isOn && !HSCheckBox.isOn){
                Debug.Log("Hãy chọn đối tượng");
                return;
            }
            if(GVCheckBox.isOn && !HSCheckBox.isOn) {
                job = "Giáo viên";
            }
            else if (!GVCheckBox.isOn && HSCheckBox.isOn) {
                job = "Học sinh";
            }
            StartCoroutine(Main.Instance.Web.RegisterUser(LastNameInput.text, FirstNameInput.text, BirthdayInput.text, UsernameInput.text, PasswordInput.text, job));
            LastNameInput.text = "";
            FirstNameInput.text = "";
            BirthdayInput.text = "";
            UsernameInput.text = "";
            PasswordInput.text = "";   
        });

        RegisterBackToLoginButton.onClick.AddListener(() => {
            Main.Instance.Web.RegisterToLogin();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
