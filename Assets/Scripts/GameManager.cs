using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject QTE;
    public GameObject Wait4Fish;
    public GameObject Flicker;
    public GameObject ReelItIn;
    public float timeThreshold = 0;
    public float flickerThreshold = 0;
    private float ranvar = 0;

    public GameObject Bobber;
    public GameObject ReelRock;

    // Start is called before the first frame update
    void Start()
    {
        QTE.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!QTE.activeSelf)
        {
            ReelRock.GetComponent<AudioSource>().Stop();
            flickerThreshold = 0;
            timeThreshold += Time.deltaTime;
            if (timeThreshold > 6 + ranvar)
            {
                ReelRock.GetComponent<AudioSource>().Play();
                ranvar = Random.Range(0, 10);
                Wait4Fish.SetActive(false);
                QTE.SetActive(true);
                ReelItIn.SetActive(true);
                Flicker.SetActive(true);
                timeThreshold = 0;
            }
        }
        else
        {
            flickerThreshold += Time.deltaTime;
            if (flickerThreshold < 0.25)
            {
                Flicker.SetActive(true);
            }
            if (flickerThreshold > 0.25)
            {
                Flicker.SetActive(false);
                if (flickerThreshold > 0.5)
                {
                    flickerThreshold = 0;
                }
            }
        }
    }
}
