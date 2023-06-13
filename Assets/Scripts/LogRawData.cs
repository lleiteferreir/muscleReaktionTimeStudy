// Autor: LLF

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;
using UnityEditor;
//Takes care of all logging
public class LogRawData : MonoBehaviour
{
	public GameObject startPanel;
    [SerializeField] LightChanger _lightChanger;

    string filename;
    string filename2;
    string filename3;
    string filename4;

    private string condition;
    private long systemTime;
    private string rawDataSample;
    private int userID;
    //Setting all directorys + filename
    void Start()
    {
        systemTime = 0;
		Directory.CreateDirectory(Application.dataPath + "/_RawData/");
		filename = _lightChanger.subjectID + "_RawData" + "_" + System.DateTime.Now.Ticks;
        Directory.CreateDirectory(Application.dataPath + "/_TestArray/");
        filename2 = _lightChanger.subjectID + "_TestArray" + "_" + System.DateTime.Now.Ticks;
        Directory.CreateDirectory(Application.dataPath + "/_FrameBasedData/");
        filename3 = _lightChanger.subjectID + "_FrameBasedData" + "_" + System.DateTime.Now.Ticks;
        Directory.CreateDirectory(Application.dataPath + "/_FrameBasedData/");
        filename4 = _lightChanger.subjectID + "_RawDataAlot" + "_" + System.DateTime.Now.Ticks;
    }
    //Logs data frame based
    private void Update()
    {
        if (_lightChanger.startStudy)
        {
            if (_lightChanger.startTimerTime < 0)
            {
                if (systemTime != System.DateTime.Now.Ticks)
                {
                    string path = (Application.dataPath + "/_FrameBasedData/" + filename3 + ".csv");
                    //Time;Ch1;Ch2;Ch3;Ch4;ID;condition;lightSwitch ch1 = biceps ch2 = tri 
                    File.AppendAllText(path, System.DateTime.Now.Ticks + ";" + rawDataSample.ToString() + _lightChanger.subjectID + ";" + condition + ";" + _lightChanger.lightActiveText + "\n");
                }
                systemTime = System.DateTime.Now.Ticks;
            }
        }
    }
    //Saves the first condition for the first run through
    public void FirstCondition(string muscle, string feedback, int timings, int repetition, int subjectID)
    {
        if (muscle == "upper leg")
        {
            muscle = "upperleg";
        }
        userID = subjectID;
        condition = muscle + "-" + feedback + "-" + timings + "-" + repetition;
    }
    //Creates CSV for the raw data
    public void CreateCSVRawData(String rawData)
	{
        rawDataSample = rawData;
        //starts only if study starts
        if (_lightChanger.startStudy)
        {
            //starts only if timeing is 0
            if (_lightChanger.startTimerTime < 0)
            {
                //sets filepath
                string path = (Application.dataPath + "/_RawData/" + filename + ".csv");
                //Adds data to file
                //Time;Ch1;Ch2;Ch3;Ch4;ID;condition;lightSwitch
                File.AppendAllText(path, System.DateTime.Now.Ticks + ";" + rawDataSample.ToString() + _lightChanger.subjectID + ";" + condition + ";" + _lightChanger.lightActiveText + "\n");
                systemTime = System.DateTime.Now.Ticks;
            }
        }
        
	}
    //Logs data to file
    public void CreateCSVTestArrary(string muscle, string feedback, int timings, int repetition, int index, string light, int duration, int restDuration, string muscleGroup1, string muscleGroup2, string muscleGroup3, string muscleGroup4)
    {
        //Starts only if the study starts
        if (_lightChanger.startStudy)
        {
            //Changes wording for better logging
            if (muscle == "upper leg")
            {
                muscle = "upperleg";
            }
            condition = muscle + "-" + feedback + "-" + timings + "-" + repetition;
            //Sets filepath
            string path2 = (Application.dataPath + "/_TestArray/" + filename2 + ".csv");
            //Adds data to file
            File.AppendAllText(path2, System.DateTime.Now.Ticks + ";" + _lightChanger.subjectID + ";" + condition + ";" + index + ";" + light + ";" + duration + ";" + muscle + ";" + restDuration + ";" + muscleGroup1 + ";" + muscleGroup2 + ";" + muscleGroup3 + ";" + muscleGroup4 + "\n");
        }
    }
}
