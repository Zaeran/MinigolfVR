/* HTC Vive Trackpad input detection
 * (c) Nathan Beattie
 * 
 * 
 * Instructions:
 * - Drag this script onto your Controller(Left) and Controller(Right) objects. 
 * - Access variables via GetDataForInputType method
 */


using UnityEngine;
using System.Collections;

public class ViveTrackpadDataManager : MonoBehaviour {

    public enum InputType { HorizontalScroll, VerticalScroll, RadialScroll, TouchedQuadrant };

    //If you'd like control over scrolling sensitivity, modify these values

    private const int DegreesToRotationSelection = 5;
    private const float DistanceToScrollDetection = 0.2f;

    float touchAngle;
    float prevAngle;
    Vector2 prevTouch;

    int VerticalScroll;
    int HorizontalScroll;
    int RadialScroll;
    int QuadrantTouched;
    Vector2 CurrentTouch;

    void Start()
    {
        if (!GetComponent<SteamVR_TrackedObject>())
        {
            Debug.LogError("This script must be placed on a HTC Vive controller object");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SteamVR_Controller.Input((int)GetComponent<SteamVR_TrackedObject>().index).GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Vector2 touchPoint = SteamVR_Controller.Input((int)GetComponent<SteamVR_TrackedObject>().index).GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);
            touchAngle = Mathf.Atan2(touchPoint.y, touchPoint.x) * Mathf.Rad2Deg + 180; // from 0 to 360 degrees

            if (prevTouch.x < 1.5f)
            {
                HorizontalScroll = returnHorizontalScroll(touchPoint.x);
                VerticalScroll = returnVerticalScroll(touchPoint.y);
                RadialScroll = returnTrackpadRotation();
                QuadrantTouched = returnQuadrantRotation();
            }
            else
            {
                prevTouch = touchPoint;
            }
        }
        else
        {
            HorizontalScroll = 0;
            VerticalScroll = 0;
            RadialScroll = 0;
            QuadrantTouched = 0;
            prevTouch = new Vector2(2, 2);
        }
    }

    public int GetDataForInputType(InputType input)
    {
        switch (input)
        {
            case InputType.HorizontalScroll:
                return HorizontalScroll;
            case InputType.VerticalScroll:
                return VerticalScroll;
            case InputType.RadialScroll:
                return RadialScroll;
            case InputType.TouchedQuadrant:
                return QuadrantTouched;
            default:
                return 0;
        }
    }

    public Vector2 GetTouchPoint()
    {
        return CurrentTouch;
    }


    int returnVerticalScroll(float touchPointY)
    {
        int returnValue = 0;
        float valueDifference = touchPointY - prevTouch.y;
        if (valueDifference < 0)
        {
            valueDifference *= -1;
        }
        if (valueDifference > DistanceToScrollDetection)
        {
            if (touchPointY > prevTouch.y)
            {
                returnValue = 1;
            }
            else
            {
                returnValue = -1;
            }
            prevTouch = new Vector2(prevTouch.x, touchPointY);
        }
        return returnValue;
    }
    int returnHorizontalScroll(float touchPointX)
    {
        int returnValue = 0;
        float valueDifference = touchPointX - prevTouch.x;
        if (valueDifference < 0)
        {
            valueDifference *= -1;
        }
        if (valueDifference > DistanceToScrollDetection)
        {
            if (touchPointX > prevTouch.x)
            {
                returnValue = 1;
            }
            else
            {
                returnValue = -1;
            }
            prevTouch = new Vector2(touchPointX, prevTouch.y);
        }
        return returnValue;
    }

    int returnTrackpadRotation()
    {
        //wheel-based manipulation method
        int returnValue = 0;
        if (Mathf.Abs(touchAngle - prevAngle) > DegreesToRotationSelection) //minimum angle moved
        {
            if (touchAngle < 354 && touchAngle > 6)
            {
                if (touchAngle < prevAngle)
                {
                    returnValue = 1;
                }
                else
                {
                    returnValue = -1;
                }
            }
            prevAngle = touchAngle;
        }
        return returnValue;
    }

    int returnQuadrantRotation()
    {
        return 4 - Mathf.FloorToInt(((touchAngle + 45) / (360/12)) % 12);
    }
}
