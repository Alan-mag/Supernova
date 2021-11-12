using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelSelectHandler : MonoBehaviour
{
    public GameObject[] levelButtons;

    private int latestLevelCompleted = 1;
    private Dictionary<int, int[]> levelDictionary = new Dictionary<int, int[]>()
    {
        { 1, new []{ 2 } },
        { 2, new []{ 3, 4, 5 } },
        { 3, new []{ 6, 7 } },
        { 4, new []{ 6 } },
        { 5, new []{ 6 } },
        { 6, new []{ 8 } },
        { 7, new []{ 8 } },
    };

    void Awake()
    {
        SetLatestLevelCompleted(latestLevelCompleted);
    }

    void SetLatestLevelCompleted(int completedLevel)
    {
        if (levelDictionary.ContainsKey(completedLevel))
        {
            int[] validNextLevels = levelDictionary[completedLevel];
            Debug.Log(validNextLevels);
            foreach(GameObject btn in levelButtons)
            {
                int btnToInt = Int32.Parse(btn.name);
                if (Array.IndexOf(validNextLevels, btnToInt) < 0)
                {
                    // btn.SetActive(false);
                    btn.GetComponent<Image>().color = Color.grey;
                    btn.GetComponent<Button>().interactable = false;
                } 
                else
                {
                    btn.SetActive(true);
                }
            }
        }
    }
}
