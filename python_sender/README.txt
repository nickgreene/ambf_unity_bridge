In one terminal window run
roscore


In another terminal run
ambf_simulator --launch_file ~/Downloads/surgical_robotics_challenge-master/launch.yaml -l 0,1,3,4,14,15 -p 120 -t 1 --override_max_comm_freq 120



in a third terminal:
cd ~/surgical_robotics_challenge/scripts/surgical_robotics_challenge/examples
python gui_based_control.py



if you want to use LapVR
cd ~/surgical_robotics_challenge/LapVr
python lapVRControl.py

make sure the power switch is on

open README.md

cd ~/surgical_robotics_challenge/LapVr/socket_based_control
python socket_based_control.py



NOTE: In order get needed poses, must edit adf .yaml files corresponding to ambf scene.
Should edit all 'passive' settings to be false so that the poses are published by ambf as rostopics.

For example:
surgical_robotics_challenge/ADF/full_new_psm1.yaml
surgical_robotics_challenge/ADF/full_new_psm2.yaml

etc...

