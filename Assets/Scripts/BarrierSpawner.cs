using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spr : MonoBehaviour
{
    public GameObject barrier;
    public static spr Instance;
    public float spawnRate = 5;
    private float timer = 0;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        Instantiate(barrier, transform.position, transform.rotation);
    }
    
    void Update()
    {
        if (gameObject.tag == "Item")
        {
            if (timer < spawnRate)
            {
                timer = timer + Time.deltaTime;
            }
            else
            {
                Instantiate(barrier, transform.position, transform.rotation);
                timer = 0;
            }
        }
    }

    // Update is called once per frame
    public void Create()
    {
        Instantiate(barrier, transform.position, transform.rotation);
       
        //continue game
    }
    public void play()
    {
        Debug.Log("haha");
    }
}
