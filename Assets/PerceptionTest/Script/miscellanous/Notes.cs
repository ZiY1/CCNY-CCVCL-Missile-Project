using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{
    [TextArea] public string note = "Notes on the data:" +
        "Object Count Labeler counts hows about many objects of one label is seen per capture. " +
        "Rendered Object Info Labeler counts how many pixels of said object can be seen. To differeniate same objects appear multiple times, they label each one with an instance_id. " +
        "Step is an id for data-producing frames in the simulation. In captures, it is the index of capture in the sequence. This field is used to order of captures within a sequence." +
        "Timestamp is timestamp in milliseconds since the sequence started" +
        "Ego is a frame of reference for a collection of sensors attaced to it. For example, for a robot with two cameras attached, the robot would be the ego containing the two sensors." +
        "3d Bonding Box - Translation is the 3d bounding box's center locaiton in meters with respect to the sensor's coordinate system" +
        "3d Bonding Box - Size is the 3d bounding box size in meters" +
        "3d Bonding Box - Rotation is the 3d bounding box orientation as quaternion: w, x, y, z" +
        "Unity Perception Gournd-Truth Generation: https://github.com/Unity-Technologies/com.unity.perception/blob/master/com.unity.perception/Documentation~/GroundTruthLabeling.md" +
        "Unity Perception API: https://docs.unity3d.com/Packages/com.unity.perception@0.8/api/UnityEngine.Perception.GroundTruth.PerceptionCamera.html" +
        "Perception Camera Component: https://github.com/Unity-Technologies/com.unity.perception/blob/master/com.unity.perception/Documentation~/PerceptionCamera.md" +
        "Unity Perception Github: https://github.com/Unity-Technologies/com.unity.perception" +
        "Perception KeypointLabeler: https://github.com/Unity-Technologies/com.unity.perception/blob/master/com.unity.perception/Documentation%7E/GroundTruth/KeypointLabeler.md" +
        "Perception Schema: https://github.com/Unity-Technologies/com.unity.perception/blob/master/com.unity.perception/Documentation~/Schema/Synthetic_Dataset_Schema.md";

    [TextArea] public string sideNote = "Found out that the reason step and timestamp does not increment is becuase when a new iteration starts, those numebrs reset. To get them to increase, on the FixedLengthScenario.cs, increase the frames per iteration." +
        "Ok so some thigns I found out about how time and frames and iterations works in this. With the help of the perception creators, he states taht increasing Frames per Iteration will increase the step and timestamp in the data. However, after playing around with this, i found a glaring flaw in this. In PerceptionCamera.cs, you can set the Unity.deltatime (this is how long a frame last). To get to 60 frames per second, deltatime needs to be 0.0166 seconds. In FixedLengthScenario.cs, there are two variables, Iteration and Frames per Iteration. An Iteration is called every frame after the current iteration is finished. Frames per Iteration is how many pictures will be rendered. By default, it has Iterations set to 100 and FPI to 1, which means taht time will equal to deltatime * (Iter * FPI). Default settings will be 0.0166 sec * (100 iter * 1 FPI) = 1.6seconds. The confusing part of all this, is FPI. if FPI is larger than 1, then Iteration will have to wait until FPI is finished. If it is 2, then iterations will take 2 frames long rather than 1 frame, which will double the total time (0.0166 * (100 * 2) = 0.332). The main issue is taht, currently talking to the devs, I have no idea why fPI would not be 1, as the scene changes based on iteration. If the iteration last for 2 seconds, then every 2 pictures and data taken will be about the same iteration. Why?";

    [TextArea] public string possibleSolution = "Alternative way to turn perception and scenario on and off: " +
        "Found out the \"wrong\" way of triggering the perception and simulations. We can simply turn both the percpetion script and simulation off. We can turn them both on and hook them up to a button taht also launches the missile. This way, when the button is pressed, perception will turn on, simulation will turn on, and the missile will launch. \nOn another note, I figured out how to add our own parameters. Will need to play around with it, but I should be able to get teh missiles position and thus its altitude.";

    [TextArea] public string problems = "Need to find a way to index the information. Also need to figure out how the scenario reacts to Perception's manual capture.";
}
