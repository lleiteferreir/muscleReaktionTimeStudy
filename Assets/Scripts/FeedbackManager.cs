// Autor: LLF

using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.EventSystems;
//Sends signals to arduino 
public class FeedbackManager : MonoBehaviour
{
    public SerialController tactileSerialController;
    public SerialController emsSerialController;
    [SerializeField] LightChanger _lightChanger;

    public bool isTactileFeedbackOn = false;
    public bool isEmsFeedbackOn = false;

    private string ClickedButtonName;
    private bool isInTestMode = false;
    //Activate feedback
    public void UpdateFeedback()
    {
        if(isTactileFeedbackOn)
        {
            SetTactileFeedback(1, isTactileFeedbackOn);
        }
        if(isEmsFeedbackOn) {
            SetEMSFeedback(1, isEmsFeedbackOn);
        }        
    }
    //checks which button is clicked and sends signal to arduino
    public void TestFeedback()
    {
        if (!isInTestMode)
        {
            isInTestMode = true;
            ClickedButtonName = EventSystem.current.currentSelectedGameObject.name;
            Debug.Log("Button clicked: " + ClickedButtonName);
            StartCoroutine(nameof(TestFeedbackCo));
        }
    }
    //start tests
    private IEnumerator TestFeedbackCo()
    {
        isInTestMode = true;
        SetTactileFeedback(1, true);
        yield return new WaitForSecondsRealtime(3);
        SetTactileFeedback(0, false);
        SetEMSFeedback(1, true);
        yield return new WaitForSecondsRealtime(3);
        SetEMSFeedback(0, false);
        isInTestMode = false;
    }
    IEnumerator startCoroutine()
    {
        //yield on a new YieldInstruction that waits for 1 seconds.
        yield return new WaitForSeconds(1);
    }
 
    private float calculatePercentage(double currentValue, double maxValue)
    {
        return (float)(currentValue / maxValue);
    }
    //Starts EMS feedback
    private void SetEMSFeedback(float percentage, bool activate)
    {
        //check if object is existend
        if (emsSerialController is null)
        {
            Debug.Log("ems serial controller is null");
            return;
        }

        // Trigger EMS
        if (isInTestMode)
        {
            //Depending on which button is clicked
            switch (ClickedButtonName)
            {
                case "EMSBiceps":
                    emsSerialController.SendSerialMessage((percentage > 0 ? 1 : 0) + "\n");
                    break;
                case "EMSTriceps":
                    emsSerialController.SendSerialMessage((percentage > 0 ? 2 : 0) + "\n");
                    break;
                case "EMSUpperLeg":
                    emsSerialController.SendSerialMessage((percentage > 0 ? 4 : 0) + "\n");
                    break;
                case "EMSLowerLeg":
                    emsSerialController.SendSerialMessage((percentage > 0 ? 3 : 0) + "\n");
                    break;
            }
        }
        else
        {
            //Checks which muslce is in use and trigger EMS
            switch (_lightChanger.muscle)
            {
                case "biceps":
                    emsSerialController.SendSerialMessage((percentage > 0 ? 1 : 0) + "\n");
                    Debug.Log("EMS ausgelöst, percentage: " + percentage + " rechnung: " + (percentage > 0 ? 1 : 0));
                    break;
                case "triceps":
                    emsSerialController.SendSerialMessage((percentage > 0 ? 2 : 0) + "\n");
                    Debug.Log("EMS ausgelöst, percentage: " + percentage + " rechnung: " + (percentage > 0 ? 2 : 0));
                    break;
                case "upper leg":
                    emsSerialController.SendSerialMessage((percentage > 0 ? 4 : 0) + "\n");
                    Debug.Log("EMS ausgelöst, percentage: " + percentage + " rechnung: " + (percentage > 0 ? 3 : 0));
                    break;
                case "calf":
                    emsSerialController.SendSerialMessage((percentage > 0 ? 3 : 0) + "\n");
                    Debug.Log("EMS ausgelöst, percentage: " + percentage + " rechnung: " + (percentage > 0 ? 4 : 0));
                    break;
                default:

                    break;
            }
        }        
    }

    private void SetTactileFeedback(float percentage, bool activate)
    {
        if(!activate)
        {
            SetTactileFeedback(0, true);
            return;
        }
        //check if object is existend
        if (tactileSerialController is null)
        {
            Debug.Log("tactile serial controller is null");
            return;
        }
        
        
        // Trigger Vib
        if (isInTestMode)
        {
            //Depending on which button is clicked
            switch (ClickedButtonName)
            {
                case "VibBiceps":
                    tactileSerialController.SendSerialMessage((percentage > 0 ? 5 : 0) + "\n");
                    break;
                case "VibTriceps":
                    tactileSerialController.SendSerialMessage((percentage > 0 ? 6 : 0) + "\n");
                    break;
                case "VibUpperLeg":
                    tactileSerialController.SendSerialMessage((percentage > 0 ? 8 : 0) + "\n");
                    break;
                case "VibLowerLeg":
                    tactileSerialController.SendSerialMessage((percentage > 0 ? 7 : 0) + "\n");
                    break;
            }          
        }
        else
        {
            //Checks which muslce is in use and trigger Vibrationmotor
            switch (_lightChanger.muscle)
            {
                case "biceps":
                    tactileSerialController.SendSerialMessage((percentage > 0 ? 5 : 0) + "\n");
                    Debug.Log("Vibration ausgelöst, percentage: " + percentage + " rechnung: " + (percentage > 0 ? 5 : 0));
                    break;
                case "triceps":
                    tactileSerialController.SendSerialMessage((percentage > 0 ? 6 : 0) + "\n");
                    Debug.Log("Vibration ausgelöst, percentage: " + percentage + " rechnung: " + (percentage > 0 ? 6 : 0));
                    break;
                case "upper leg":
                    tactileSerialController.SendSerialMessage((percentage > 0 ? 8 : 0) + "\n");
                    Debug.Log("Vibration ausgelöst, percentage: " + percentage + " rechnung: " + (percentage > 0 ? 8 : 0));
                    break;
                case "calf":
                    tactileSerialController.SendSerialMessage((percentage > 0 ? 7 : 0) + "\n");
                    Debug.Log("Vibration ausgelöst, percentage: " + percentage + " rechnung: " + (percentage > 0 ? 7 : 0));
                    break;
                default:

                    break;
            }
        } 
    }
}