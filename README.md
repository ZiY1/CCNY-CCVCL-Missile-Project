# Missile Project

## Prerequisite
- Unity Version: 2020.3.10f1

## Description
A project that launches a missile/rocket.
To test Perception, go to Asset > Perception Test > Scene and open it.

## Problem
- One possible problem is the physics of Unity. Usually as a missile sores through the air, they start to come down at an angle. This would be due to the missile's rotation and speed. However, if we randomize the rotation of the missile, would that not affect its flight path? Need to look into this

## What needs to be done
- Find a way to output the data that verifies camera's position is similar to its other angles
- A better way to handle the physics of the missile or a better way to implement Unity's rigidbody
- Learn to use Machine Learning program

## ChangeLog
### Sept 1
- Added annotations for Camera's FOV
### Aug 28
- Added BoundingBox2D
- Made varies small changes such as renaming some variables to make it easier to understand
### Aug 25
- Updated the Rocket Test folder to Team 1's newest version.
- Fixed an error in script StartPerception.cs
### Aug 12
- Data Capture now includes the objects world position, world rotation, and world quaternion values
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
