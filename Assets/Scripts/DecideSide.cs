// Autor: LLF

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//class for picking one body part left/right
public class DecideSide : MonoBehaviour
{
    public bool rightSide = true;
    public bool leftSide = false;
    public Toggle toogleboxRightSide;
    public Toggle toogleboxLeftSide;
    //starts toogle
    private void Start()
    {
        toogleboxRightSide.onValueChanged.AddListener(delegate
        {
            ToggleValueChangedOccured(toogleboxRightSide, toogleboxLeftSide);
        });
        toogleboxLeftSide.onValueChanged.AddListener(delegate
        {
            ToggleValueChangedOccured(toogleboxRightSide, toogleboxLeftSide);
        });
    }
    //Toggles between both sides
    void ToggleValueChangedOccured(Toggle toggleRightSide, Toggle toggleLeftSide)
    {
        if(rightSide)
        {
            if (toggleLeftSide.isOn)
            {
                rightSide = false;
                leftSide = true;
                toggleRightSide.isOn = false;
                toggleLeftSide.interactable= false;
                toggleRightSide.interactable = true;
            }
            else
            {
                toggleLeftSide.isOn = false;
            }
        }
        if (leftSide)
        {
            if (toggleRightSide.isOn)
            {
                rightSide = true;
                leftSide = false;
                toggleLeftSide.isOn = false;
                toggleRightSide.interactable = false;
                toggleLeftSide.interactable = true;
            }
            else
            {
                toggleRightSide.isOn = false;
            }
        }
        
        
    }
}
