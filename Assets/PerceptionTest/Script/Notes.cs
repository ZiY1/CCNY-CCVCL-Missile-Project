using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{
    [TextArea] public string text = "Found out the \"wrong\" way of triggering the perception and simulations. We can simply turn both the percpetion script and simulation off. We can turn them both on and hook them up to a button taht also launches the missile. This way, when the button is pressed, perception will turn on, simulation will turn on, and the missile will launch. \nOn another note, I figured out how to add our own parameters. Will need to play around with it, but I should be able to get teh missiles position and thus its altitude.";
}
