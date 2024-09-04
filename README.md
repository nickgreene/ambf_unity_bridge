# AMBF_Unity_Bridge

Lots of development is still required

## Structure
- The `unity_receiver` folder contains a unity project.
- The `python_sender` folder contains a `README` and a `.py` file that should be
  run on an ubuntu machine with `ambf` and `surgical_robotics_challenge`
  - Poses from ambf are read, converted to a json string, and sent via UDP
- The `json` structure is:

```json
{
  "Name": "/your/ambf/topic/name",
  "Pose": {
    "Position": {
      "x": 0,
      "y": 0,
      "z": 0
    },
    "Orientation": {
      "x": 0,
      "y": 0,
      "z": 0,
      "w": 0
    }
  }
}
```

### Notes

- The Unity scene only receives 6D poses and shows rigid meshes. None of the AMBF physics are used.
- The Unity scene is expected to contain all of the individual meshes from the corresponding  AMBF  scene as gameobjects. You have to import everything manually.
- AMBF's configuration files are modified so that the pose of each individual mesh is published as a ROS Topic. A python program reads the poses and sends them via UDP to Unity. There are no joint angles, only poses.
- Sometimes Unity will block incoming UDP packets without an obvious reason. A workaround that seems to work is to first send a UDP message from Unity to the IP of where you will be recieving from. 


### Unfinished:

1. The coordinates from AMBF are not correctly converted to the Unity coordinate system. The 3D scene is not displayed correctly in Unity.
2. The 6D poses are sent and received in a suboptimal way right now so performance is not good.
3. The AMBF topics need to be manually matched with the correct meshes in Unity.
