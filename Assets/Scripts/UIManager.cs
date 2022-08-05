using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    private Slider FrameSlider;
    // Start is called before the first frame update
    void Start()
    {
        FrameSlider = GameObject.Find("FrameSlider").GetComponent<Slider>();
        
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
        FrameSlider.maxValue = SkeletonDriver.Instance.lowerFrameStore.Count;
        SkeletonDriver.Instance.framesLoaded = true;
        

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
    
    public void SetFrame()
    {
        SkeletonDriver.Instance.CurrentFrame = (int)FrameSlider.value;
    }




}


