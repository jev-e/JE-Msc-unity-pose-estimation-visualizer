using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManager : MonoBehaviour
{
    private bool StatsToggle = false;
    private Slider FrameSlider;
    private TextMeshProUGUI StatsText;
    // Start is called before the first frame update
    void Start()
    {
        FrameSlider = GameObject.Find("FrameSlider").GetComponent<Slider>();
        StatsText = GameObject.Find("StatsText").GetComponent<TextMeshProUGUI>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(StatsToggle)
        {
            SetStats();
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        
    }

    // Accepts the full path (E.g C:\Path\to\results\directory\results\run), must have both prediciton and test_values csv in directory
    public void GetResultsDirectory(string directory)
    {
        if (directory == "")
        {
            Debug.Log("Entry cant be empty");
        }
        else
        {
        SkeletonDriver.Instance.ParseLowerCSV(directory + "/prediction.csv");
        SkeletonDriver.Instance.ParseUpperCSV(directory + "/test_values.csv");
        FrameSlider.maxValue = SkeletonDriver.Instance.lowerFrameStore.Count - 1;
        SkeletonDriver.Instance.framesLoaded = true;
        SkeletonDriver.Instance.isPaused = false;
        }
    }

    // Pause recording (if playing)
    public void PauseRecording()
    {
        SkeletonDriver.Instance.isPaused = true;
    }

    // Play recording (if paused)
    public void PlayRecording()
    {
        SkeletonDriver.Instance.isPaused = false;
    }
    
    // Set frame based on slider value
    public void SetFrame()
    {
        SkeletonDriver.Instance.CurrentFrame = (int)FrameSlider.value;
        SkeletonDriver.Instance.currentTimeFrames = (float)FrameSlider.value;
        SkeletonDriver.Instance.UpdateJointPosition();
    }

    // Display stats based on bool
    public void ToggleStats()
    {
        StatsToggle = !StatsToggle;
        StatsText.enabled = StatsToggle;             
 
    }

    // Sets stats text
    public void SetStats()
    {
        StatsText.text = "Current Frame: " + SkeletonDriver.Instance.CurrentFrame + "\n" +
                         "Current Joint Positions (X,Y,Z) \n" +
                         "Head: " + SkeletonDriver.Instance.upperFrameStore[SkeletonDriver.Instance.CurrentFrame].head + "\n" +
                         "L_Controller: " + SkeletonDriver.Instance.upperFrameStore[SkeletonDriver.Instance.CurrentFrame].l_controller + "\n" +
                         "R_Controller: " + SkeletonDriver.Instance.upperFrameStore[SkeletonDriver.Instance.CurrentFrame].r_controller + "\n" +
                         "Waist: " + SkeletonDriver.Instance.lowerFrameStore[SkeletonDriver.Instance.CurrentFrame].waistActual + "\n" +
                         "P_Waist: " + SkeletonDriver.Instance.lowerFrameStore[SkeletonDriver.Instance.CurrentFrame].waistPredicted + "\n" +
                         "L_Foot: " + SkeletonDriver.Instance.lowerFrameStore[SkeletonDriver.Instance.CurrentFrame].lFootActual + "\n" +
                         "P_L_Foot: " + SkeletonDriver.Instance.lowerFrameStore[SkeletonDriver.Instance.CurrentFrame].lFootPredicted + "\n" +
                         "R_Foot: " + SkeletonDriver.Instance.lowerFrameStore[SkeletonDriver.Instance.CurrentFrame].rFootActual + "\n" +
                         "P_R_Foot: " + SkeletonDriver.Instance.lowerFrameStore[SkeletonDriver.Instance.CurrentFrame].rFootPredicted + "\n"; 

    }






}


