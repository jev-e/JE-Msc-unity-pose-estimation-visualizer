using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SkeletonDriver : MonoBehaviour
{
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
        string path = "C:/Users/Jev/Documents/Liv Project/results/run_01_08_2022_18_25/prediction.csv";
        string[] lines = System.IO.File.ReadAllLines(path);
        int i = 0;
        foreach (string line in lines)
        {
            Debug.Log(i);
            i++;
            string[] columns = line.Split(',');
            foreach (string column in columns)
            {
                
            }
        }
    }
}
