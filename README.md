# Missile Project

## Prerequisite
- Unity Version: 2020.3.10f1

## Description
A project that launches a missile/rocket.
To test Perception, go to Asset > Perception Test > Scene and open it.

## Problem
Seems like Unity's physics is not as consistent as I like. When testing the same angle and power for the missile, it produces different results. Sometime the missile lands on its head, other times it lands on its body. Need to create a custom physics script for the missile or look into Unity's current physics to understand and adjust.

Other issues is that for the camera tracking the missile/rocket, there is a limit to how far the camera will follow the rocket before the rocket disappears. While this can be adjusted in camera settings via changing the field of view and increasing the clipping plane's far variable, the right values have not been found yet.

## What needs to be done
- A better way to handle the physics of the missile or a better way to implement Unity's rigidbody
- Add a filter for the camera
- Learn to use Perception and create multiple images to be fed to a Machine Learning program
- Learn to use Machine Learning program
- Find a way to index the information

## ChangeLog
### Aug 10
- Added a button to start perception capture and randomization
### Aug 4
- Created a simple scene for testing.
### Jul 7
- Added some custom randomizers for Perception. These scripts alter how many foreground objects spawn, what position they spawn in, and the rotation.
### Jun 28
- Tried to follow the Perception Tutorial in their github but found out that there were some essential files missing that were missing. So instead of guessing which files are missing, I recreated it and tested some of the scenes to make sure everything was in order.
- Also, there is a Perception folder where I started their github tutorial.
### Jun 24
- Combined Kelvin's work with mine.
- testing out a new smoke trail with lighting effects for a more realistic rocket engine
(With HDRP and Render Pipelines, lighting effects seems to work differently than in the 3D version. Because of this, the smoke effect for whatever reason comes off as a slight greenish color. No idea why.)
- Added and removed HDRP because it broke a lot of things.
- Fixed some things that I didn't realized Kelvin changed
- Added GetRocket() to SpawnRocket.cs, should always return the rocket
  - Tested this by added SpawnRocket.GetRocket() to CameraManager.cs.
### Jun 11
- Finished the restart button. All it does it restarts the scene.
- Added a rewind button. Pressing and holding the return/enter key rewinds the missile. Maybe useful for filming.
### Jun 10
- Added audio for rocket launching and running
- Added smoke trails
- Made one of the cameras track the rocket
### Jun 1
- A temp missile has been made.
- Code to launch missile
- Buttons to change camera angles
- Inputfields to adjust power and angle of missile
