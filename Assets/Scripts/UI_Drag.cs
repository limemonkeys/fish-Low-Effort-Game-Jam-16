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
    	print("caught");
    	ReelPoints = 0.0f;
    	bobber.position = new Vector3(-3.82f,1.13f,-2.980f);
    	bobber.velocity = new Vector3(0.0f,0.0f,0.0f);
    	coolDown = 3.0f;
    	fishCount++;
    	print(fishCount + " Fishes caught!");
    	if (fishCount == 3) {
    		print("SHARK JUMPSCARE");
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
    	if (ReelPoints >= 1.0f)
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
