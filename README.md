 JE-Msc-unity-pose-estimation-visualizer
Unity project viewing results from lower body pose prediction model

Author: Jacob Evans 

Part of Msc Advanced Computer Science Project </br>

<pre>
-Assets
      -Scripts
        -SkeletonDriver.cs <- Reads in CSV, finds joints in scene, controls joints using frame list
        -UIManager.cs <- Controls UI elements in scene. Passes CSV directory string to Driver, toggles bool for pause / play, passes slider int to driver for frame selection
     -Scenes
        -ResultsVisualizer.unity <- Main scene for playing recordings
      
</pre>
