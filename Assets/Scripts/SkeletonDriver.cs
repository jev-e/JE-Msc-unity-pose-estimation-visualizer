using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class SkeletonDriver : MonoBehaviour
{

    //Struct to store the position of each joint at a certain frame
    public struct frame
    {

        public Vector3 head;
        public Vector3 r_controller;
        public Vector3 l_controller;
        public Vector3 waistPredicted;
        public Vector3 waistActual;
        public Vector3 rFootActual;
        public Vector3 rFootPredicted;
        public Vector3 lFootPredicted;
        public Vector3 lFootActual;

    }

    //List of lower joint positions
    public List<frame> lowerFrameStore { get; set; }

    //List of upper joint positions
    public List<frame> upperFrameStore { get; set; }

    //Path of results for playback
    public string directoryPath { get; set; }

    //Class instance
    public static SkeletonDriver Instance;

    //Game object decleration of each joint
    public GameObject head { get; set; }
    public GameObject l_controller { get; set; }
    public GameObject r_controller { get; set; }
    public GameObject p_waist { get; set; }
    public GameObject a_waist { get; set; }
    public GameObject a_l_foot { get; set; }
    public GameObject p_l_foot { get; set; }
    public GameObject p_r_foot { get; set; }
    public GameObject a_r_foot { get; set; }


    //bool for pausing / playing recording
    public bool isPaused = true;

  
    public bool framesLoaded = false;

    
    public float totalTimeSeconds = 10f;
    public float framesPerSecond = 1000f;
    public float currentTimeFrames = 0.0f;

    [SerializeField]
    private int frameNum;

    //current frame being displayed
    public int CurrentFrame { get; set; }

    //on awake set paused to true, and set instance
    void Awake()
    {
        isPaused = true;
        framesLoaded = false;
        Instance = this;
    }
    // Start is called before the first frame update
    // Find all joint game objects in scene, set current frame to 0
    void Start()
    {
        CurrentFrame = 0;
        GetJoints();
    }

    // Update is called once per frame
    // If playback is not paused, calculate next frame based on time elapsed since last call to update, set new frame, then update joint pos
    void Update()
    {
        if (!isPaused)
        {

            currentTimeFrames += Time.deltaTime * framesPerSecond;
            CurrentFrame = (int)currentTimeFrames;

            UpdateJointPosition();

        }
      
    }

    // Set all joint game objects position to the vector based on the currentFrame, indexing the list
    public void UpdateJointPosition()
    {

        head.transform.position = upperFrameStore[CurrentFrame].head;
        r_controller.transform.position = upperFrameStore[CurrentFrame].r_controller;
        l_controller.transform.position = upperFrameStore[CurrentFrame].l_controller;
        p_waist.transform.position = lowerFrameStore[CurrentFrame].waistPredicted;
        a_waist.transform.position = lowerFrameStore[CurrentFrame].waistActual;
        p_l_foot.transform.position = lowerFrameStore[CurrentFrame].lFootPredicted;
        a_l_foot.transform.position = lowerFrameStore[CurrentFrame].lFootActual;
        a_r_foot.transform.position = lowerFrameStore[CurrentFrame].rFootActual;
        p_r_foot.transform.position = lowerFrameStore[CurrentFrame].rFootPredicted;

        if (CurrentFrame == lowerFrameStore.Count - 1)
        {
            isPaused = true;
            this.CurrentFrame = 0;
            currentTimeFrames = 0.0f;
        }
    }


    // parse the predicted and lower body positions from the results CSV file
    public void ParseLowerCSV(string run_path)
    {
        Debug.Log("parsing");
        string[] lines = System.IO.File.ReadAllLines(run_path);
        List<frame> frameList = new List<frame>();
        foreach (string line in lines)
        {

            string[] columns = line.Split(',');
            if (float.TryParse(columns[0], out float val))
            {
                frame rowFrame = new frame();
                rowFrame.waistActual = new Vector3(float.Parse(columns[0]), float.Parse(columns[1]), float.Parse(columns[2]));
                rowFrame.rFootActual = new Vector3(float.Parse(columns[3]), float.Parse(columns[4]), float.Parse(columns[5]));
                rowFrame.lFootActual = new Vector3(float.Parse(columns[6]), float.Parse(columns[7]), float.Parse(columns[8]));
                rowFrame.waistPredicted = new Vector3(float.Parse(columns[9]), float.Parse(columns[10]), float.Parse(columns[11]));
                rowFrame.rFootPredicted = new Vector3(float.Parse(columns[12]), float.Parse(columns[13]), float.Parse(columns[14]));
                rowFrame.lFootPredicted = new Vector3(float.Parse(columns[15]), float.Parse(columns[16]), float.Parse(columns[17]));
                frameList.Add(rowFrame);
            }
            else
            {
                continue;
            }
        }
        
        lowerFrameStore = frameList;

    }

    //Parse the upper body positions of the joints from CSV file
    public void ParseUpperCSV(string run_path)
    {
        string[] lines = System.IO.File.ReadAllLines(run_path);
        List<frame> frameList = new List<frame>();
        foreach (string line in lines)
        {

            string[] columns = line.Split(',');
            if (float.TryParse(columns[0], out float val))
            {
                frame rowFrame = new frame();
                rowFrame.head = new Vector3(float.Parse(columns[0]), float.Parse(columns[1]), float.Parse(columns[2]));
                rowFrame.r_controller = new Vector3(float.Parse(columns[3]), float.Parse(columns[4]), float.Parse(columns[5]));
                rowFrame.l_controller = new Vector3(float.Parse(columns[6]), float.Parse(columns[7]), float.Parse(columns[8]));
                frameList.Add(rowFrame);
            }
            else
            {
                continue;
            }
        }
        upperFrameStore = frameList;

    }

    // Get all joint game objects in the scene
    public void GetJoints()
    {
        head = GameObject.Find("head");
        l_controller = GameObject.Find("l_controller");
        r_controller = GameObject.Find("r_controller");
        p_waist = GameObject.Find("p_waist");
        a_waist = GameObject.Find("waist");
        p_l_foot = GameObject.Find("p_l_foot");
        p_r_foot = GameObject.Find("p_r_foot");
        a_l_foot = GameObject.Find("l_foot");
        a_r_foot = GameObject.Find("r_foot");

    }

}

