using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetResultsDirectory(string directory)
    {
        Debug.Log(directory);
        SkeletonDriver.Instance.ParseLowerCSV(directory + "/prediction.csv");
        SkeletonDriver.Instance.ParseUpperCSV(directory + "/test_values.csv");
        SkeletonDriver.Instance.isPaused = false;
    }

    public void PauseRecording()
    {
        SkeletonDriver.Instance.isPaused = true;
    }
    public void PlayRecording()
    {
        SkeletonDriver.Instance.isPaused = false;
    }
    


}


