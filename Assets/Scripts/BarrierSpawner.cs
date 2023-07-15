using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spr : MonoBehaviour
{
    public GameObject barrier;
    public static spr Instance;
   // public float spawnRate = 2;
  //  private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;    
        UpdateGame();
    }

    // Update is called once per frame
    public void UpdateGame()
    {
        /*if (timer < spawnRate)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            Instantiate(barrier, transform.position, transform.rotation);
            timer = 0;
        }*/
        //continue game
            Instantiate(barrier, transform.position, transform.rotation);
        
    }
    public void play()
    {
        Debug.Log("haha");
    }
}
