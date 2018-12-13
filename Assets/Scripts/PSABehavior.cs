using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PseudoAnalytics;

public class PSABehavior : MonoBehaviour
{
    void Start()
    {
        CSVData[] cSVDatas = new CSVData[1000];
        for (int i = 0; i < 1000; i++)
            cSVDatas[i] = new CSVData(ClockWork.GetPSATime, PSAConstants.MAIN_SCENE_MODULE, ClockWork.AppendMap(PSAConstants.MAIN_SCENE_MODULE));
        CSVParser.Instance.SaveDataToCSV(cSVDatas);
        for (int i = 0; i < 1000; i++)
            cSVDatas[i] = new CSVData(ClockWork.GetPSATime, PSAConstants.SHOPPING_SCENE_MODULE, ClockWork.AppendMap(PSAConstants.SHOPPING_SCENE_MODULE));
        CSVParser.Instance.SaveDataToCSV(cSVDatas);
        for (int i = 0; i < 1000; i++)
            cSVDatas[i] = new CSVData(ClockWork.GetPSATime, PSAConstants.SHOWCASE_SCENE_MODULE, ClockWork.AppendMap(PSAConstants.SHOWCASE_SCENE_MODULE));
        CSVParser.Instance.SaveDataToCSV(cSVDatas);

        //Debug.Log(CSVParser.Instance.IsDataEmpty);
        //foreach (var item in CSVParser.Instance.GetAllDataFromCSV())
        //    Debug.Log(item);
    }
}
