using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class SkeletonDriver : MonoBehaviour
{

    public struct frame
    {

        public Vector3 head;
        public Vector3 r_controller;
        public Vector3 l_controller;
        public Vector3 waistPredicted;
        public Vector3 waistActual;
        public Vector3 rFootPredicted;
        public Vector3 rFootActual;
        public Vector3 lFootPredicted;
        public Vector3 lFootActual;

    }

    public List<frame> lowerFrameStore { get; set; }

    public List<frame> upperFrameStore { get; set; }

    public string directoryPath { get; set; }

    public static SkeletonDriver Instance;

    public GameObject head { get; set; }
    public GameObject l_controller { get; set; }
    public GameObject r_controller { get; set; }
    public GameObject p_waist { get; set; }
    public GameObject a_waist { get; set; }
    public GameObject a_l_foot { get; set; }
    public GameObject p_l_foot { get; set; }
    public GameObject p_r_foot { get; set; }
    public GameObject a_r_foot { get; set; }


    public bool isPaused = true;

    public bool framesLoaded = false;

    public float totalTimeSeconds = 10f;
    public float framesPerSecond = 1000f;
    public float currentTimeFrames = 0.0f;



    [SerializeField]
    private int frameNum;

    public int CurrentFrame { get; set; }

    void Awake()
    {
        isPaused = true;
        framesLoaded = false;
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        CurrentFrame = 0;
        GetJoints();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            Debug.Log(CurrentFrame);

            currentTimeFrames += Time.deltaTime * framesPerSecond;
            CurrentFrame = (int)currentTimeFrames;

            UpdateJointPosition();

        }
      
    }




    public void UpdateJointPosition()
    {

        //if (isPaused)
        //{
        //    Debug.Log("Paused");

        //} 
        // else
        //{
        //StartCoroutine(SlowFrames());
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

        }
        else
        {
            //CurrentFrame++;
        }
        //}

    }

    //public IEnumerator SlowFrames()
    //{
    //    isPaused = true;
    //    yield return new WaitForSeconds(0.1f);
     //   isPaused = false;
    //}

    public void ParseLowerCSV(string run_path)
    {
        Debug.Log("parsing");
        string path = "C:/Users/jev/Documents/LIV Project/results/" + run_path;
        string[] lines = System.IO.File.ReadAllLines(path);
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
        //return frameList;

    }

    public void ParseUpperCSV(string run_path)
    {
        string path = "C:/Users/jev/Documents/LIV Project/results/" + run_path;
        string[] lines = System.IO.File.ReadAllLines(path);
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
        //return frameList;

    }

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

