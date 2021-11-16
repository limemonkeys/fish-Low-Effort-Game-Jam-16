using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Drag : MonoBehaviour
{
    bool startDrag;
    public GameObject ReelInner;
    public GameObject ReelOuter;
    private Vector2 reelBounds;
    private Vector3 centerPosition;
    private float reelInnerRadius;
    private float reelOuterRadius;
    private Vector3 previousPosition;
    private float coolDown;
    public float ReelPoints;
    public float fishStrength;

    public Rigidbody bobber;
    private int fishCount;

    public float fillAmount = 0;
    public float timeThreshold = 0;
    public GameObject FillObj;

    public GameObject QTE;
    public GameObject Wait4Fish;
    public GameObject Flicker;
    public GameObject ReelItIn;

    public GameObject Fish1;
    public GameObject Fish2;
    public GameObject BadFish1;
    public GameObject BadFish2;
    public GameObject Jumpscare;

    public GameObject Rod;
    public GameObject Bobber;

    // Start is called before the first frame update
    void Start()
    {
        reelInnerRadius = ReelInner.GetComponent<RectTransform>().rect.width / 2;
        reelOuterRadius = ReelOuter.GetComponent<RectTransform>().rect.width / 2;
        centerPosition = ReelInner.GetComponent<RectTransform>().position;
        coolDown = 0.0f;
        fishCount = 0;
    }
    void CatchFish()
    {
        QTE.SetActive(false);
        Flicker.SetActive(false);
        ReelItIn.SetActive(false);
        Wait4Fish.SetActive(true);

        

        ReelPoints = 0.0f;
    	bobber.position = new Vector3(-3.82f,1.13f,-2.980f);
    	bobber.velocity = new Vector3(0.0f,0.0f,0.0f);
    	//coolDown = 3.0f;
    	fishCount++;

        if (fishCount < 5)
        {
            Bobber.GetComponent<AudioSource>().Play();
        }

        switch (fishCount)
        {
            case 1:
                Fish1.SetActive(true);
                break;
            case 2:
                Fish2.SetActive(true);
                break;
            case 3:
                BadFish1.SetActive(true);
                break;
            case 4:
                BadFish2.SetActive(true);
                break;
            case 5:
                Rod.GetComponent<AudioSource>().Stop();
                Jumpscare.SetActive(true);
                // Jumpscare Audio Here
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (coolDown > 0.0f)
        {
            coolDown -= Time.deltaTime;
            return;
        }

    	if (ReelPoints >= 1.0f || (fishCount == 4 && ReelPoints >= 0.70f))
    	{
    		CatchFish();
    	}
        previousPosition = this.GetComponent<RectTransform>().position;
        if (startDrag)
        {
        	if (ReelPoints == 0.0f)
        	{
        		bobber.AddForce(new Vector3(-0.5f, 0.5f, 0.0f),ForceMode.Impulse);
        	}
            Vector3 newLocation = Input.mousePosition;

            float distance = Vector3.Distance(newLocation, centerPosition);

            // Check to make sure handle is between inner and outer part of the reel
            if (distance > reelInnerRadius && distance < reelOuterRadius) 
            {
                Vector3 fromOriginToObject = newLocation - centerPosition; 
                fromOriginToObject *= ((reelInnerRadius + reelOuterRadius)/2) / distance; 
                newLocation = centerPosition + fromOriginToObject; 

                transform.position = newLocation;


                float distancePrevCurr = Vector3.Distance(newLocation, previousPosition);

                // Make sure there are no "cheesed" methods of reeling in
                if (distancePrevCurr > 0f && distancePrevCurr < 40f)
                {
                    // Lower the float, higher the strength
                    ReelPoints += fishStrength * distancePrevCurr;
                }

            }

        }

        timeThreshold += Time.deltaTime;
        if (timeThreshold > 0.1) {
            timeThreshold = 0;
            ReelPoints -= .02f;
        }

        if (ReelPoints < 0) {
            ReelPoints = 0;
        }

        FillObj.GetComponent<Image>().fillAmount = ReelPoints;
    }

    public void StartDragUI()
    {
        startDrag = true;
    }

    public void StopDragUI()
    {
        startDrag = false;
    }

}
