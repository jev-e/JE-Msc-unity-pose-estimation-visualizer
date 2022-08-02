using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class SkeletonDriver : MonoBehaviour
{

    public struct frame
    {
        public frame()
        {
            Vector3 waistPredicted;
            Vector3 waistActual;
            Vector3 rFootPredicted;
            Vector3 rFootActual;
            Vector3 lFootPredicted;
            Vector3 lFootActual;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ParseCSV();

   
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    void ParseCSV()
    {
        string path = "C:/Users/jev/Documents/LIV Project/results/run_01_08_2022_18_25/prediction.csv";
        string[] lines = System.IO.File.ReadAllLines(path);
        List<frame> frameList = new List<frame>();
        foreach (string line in lines)
        {
            string[] columns = line.Split(',');
            frame rowFrame = new frame;
            rowFrame.waistActual = new Vector3(float.Parse(columns[0]), float.Parse(columns[1]), float.Parse(columns[2]));
            rowFrame.rFootActual = new Vector3(float.Parse(columns[3]), float.Parse(columns[4]), float.Parse(columns[5]));
            rowFrame.lFootActual = new Vector3(float.Parse(columns[6]), float.Parse(columns[7]), float.Parse(columns[8]));
            rowFrame.waistPredicted = new Vector3(float.Parse(columns[9]), float.Parse(columns[10]), float.Parse(columns[11]));
            rowFrame.rFootPredicted = new Vector3(float.Parse(columns[12]), float.Parse(columns[13]), float.Parse(columns[14]));
            rowFrame.lFootPredicted = new Vector3(float.Parse(columns[15]), float.Parse(columns[16]), float.Parse(columns[17]));











        }


    }

    void ParseCSVAttemptOne()
    {
        string path = "C:/Users/jev/Documents/LIV Project/results/run_01_08_2022_18_25/prediction.csv";
        string[] lines = System.IO.File.ReadAllLines(path);
       
        List<float[]> coordsList = new List<float[]>();
        int counter = 0;
        float[] coords = new float[18];
        foreach (string line in lines)
        {
            coordsList.Add(coords);

            string[] columns = line.Split(',');
            counter = 0;
            foreach (string column in columns)
            {

                if (float.TryParse(column, out float value))
                {
 
                    coords[counter] = value;
                    counter++;
                                    
                    //if (counter == 17)
                    //{

                      //  coordsList.Add(coords);

                    //}
                }

                else
                {
                    continue;
                }
  
            }

        }
        int j = 0;
        foreach (float[] arr in coordsList)
        {
            Debug.Log(arr[0]);
            j++;
        }
        Debug.Log(j);
    }
}

