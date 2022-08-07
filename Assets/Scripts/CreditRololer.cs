using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditRololer : MonoBehaviour
{
    [SerializeField] float rollspeedModifier = 1f;
    [SerializeField] float waitTime = 5f;
    [SerializeField] float rollTime = 30f;
    float timer;
    float timer2;

    // Start is called before the first frame update
    void Start()
    {
        ResetTimers();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            transform.Translate(-Vector3.forward * Time.deltaTime * rollspeedModifier);
            //transform.Rotate(0, 0, Time.deltaTime * rollspeedModifier);
        }
        if (timer2 > 0)
        {
            timer2 -= Time.deltaTime;
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    void ResetTimers()
    {
        timer = waitTime;
        timer2 = rollTime;
    }
}
