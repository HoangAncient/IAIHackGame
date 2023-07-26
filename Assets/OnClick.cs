using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClick : MonoBehaviour
{
    public string level;
    public void LoadLever(string lv)
    {
        lv = level;
        SceneManager.LoadScene(lv);
    }
}
