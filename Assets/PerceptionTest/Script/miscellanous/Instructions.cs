using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour
{
    [TextArea] public string instructions = "The way the scene is set up, there are two perception sections. Make sure which ever sections you are testing, that only its objects are enabled and the other section's objects are disabled. " +
        "The first section is the SimpleScene. This has a camera and a moveable cube. Enable all objects in this scene if they are not. Perception in this part is in CameraPlacement > CameraPlacement01 > TestCamera. TestCube has a script where you can change the what directions the cube will move in its inspector." +
        "The second section is TutorialTest. This section follows the instructions given by Perception. The section will spawn multiple objects and randomize their position and rotation for the camera to take pictures of. Enable the objects within it." +
        "Which ever section you choose to enable, once you play the scene and let it run its course, go to the enabled camera. At the botton you will see an option to show a folder. Here is where your information and pictures will be stored. " +
        "See notes for more information.";

}