# Import the Client from ambf_comm package
from ambf_client import Client
import time
import json
import socket

# Address and port for sending
UDP_IP = "10.203.185.23"
UDP_PORT = 48000

# Create a instance of the client
_client = Client("streamer")

# Connect the client which in turn creates callable objects from ROS topics
# and initiates a shared pool of threads for bidrectional communication 
_client.connect()

# You can print the names of objects found
_client.get_obj_names()

# Lets say from the list of printed names, we want to get the 
# handle to an object names "Torus"

handle_list = []


# Create Socket
sock = socket.socket(socket.AF_INET, # Internet
                     socket.SOCK_DGRAM) # UDP


for name in _client.get_obj_names():
    if name != "World":
        handle_list.append(_client.get_obj_handle(name))


while True:
    data = {}
    for handle in handle_list:
        name = handle.get_name()
        #print(name)
        if (name == "/ambf/env/psm1/toolyawlink"):
            print(cur_pos)
        cur_pos = handle.get_pos()
        cur_rot = handle.get_rot()
        position = {
            "x": cur_pos.x,
            "y": cur_pos.y,
            "z": cur_pos.z
        }
        orientation = {
            "x": cur_rot.x,
            "y": cur_rot.y,
            "z": cur_rot.z,
            "w": cur_rot.w
        }

        pose = {
            "position": position,
            "orientation": orientation
        }

        data["name"] = name
        data["pose"] = pose
        
        output_string = json.dumps(data)
        MESSAGE = bytes(output_string, 'UTF-8')

        sock.sendto(MESSAGE, (UDP_IP, UDP_PORT))
        
        data = {}
        
    time.sleep(.1)  # This was added to avoid overwhelming the Unity Client
        

# Lastly to cleanup
_client.clean_up()
