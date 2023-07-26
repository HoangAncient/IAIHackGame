using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5;
    public GameObject barrier;
    public float spawnRate = 2;
    private float timer = 0;
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
            Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
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
        transform.position = transform.position + (Vector3.left * speed) * Time.deltaTime;
        if (transform.position.x < -15)
        {
            Destroy(gameObject);
        }
    }
}
