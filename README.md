# MoVR

MoVR is a Motion Capture platform made in Unity utilizing VR ([built with the HTC Vive in mind] for motion tracking and input) to capture animations constructed through Inverse Kinematics based on the tracked equipment.
The animations are limited to be used within Unity (at least for now), as there are no ways of converting or otherwise extracting the animations from the Unity-animations-format to a standard model-animation-format such as obj, fbx etc.

The platform provides an example of how the animations can be recorded, and then re-played on a sepparate model.
The model used is [Unity Technologies' Kyle, available free of charge at the Unity Asset Store](https://assetstore.unity.com/packages/3d/characters/robots/space-robot-kyle-4696).

# Controls
| Input                     | Action               |
|---------------------------|----------------------|
| Movement                  | IK Movement          |
| Grip                      | IK/Animate grip      |            
| Trackpad (buttonpress)    | Start/Stop Recording |
| Trigger                   | Grab Object          |
| Trackpad (touchpad)       | Held object scaling  |