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

# Wanted improvements
- Support for additional tracking by using HTC Nodes or additional controllers (which either requires USB-dongles or long USB-cables to connect to SteamVR). Through Unity's IK system there's potential support for a total of 10 tracked nodes: 4 IK-goals, 4 IK-hints, Head and eye-tracking (although the latter is used for looking at a target so it might be extrapolated from head rotation and eye tracking software [available for some VR headsets. namely HTC Vive Pro]).
Some of these tracking points might also be tracked using other tracking software, such as Microsoft's Kinect or newer, machine learnt tracking mechanisms. As such it might be an idea to implement systems to utilize these tracking mechanisms too, either as alternatives for tracking, or as additional trackers utilizing averages for more precise tracking.
- Better movement systems in VR, that also makes sense from an animation standpoint.
- Exporting mechanismis - allowing the platform to be a useful tool for a more general scene of both game-dev and movie specialists.