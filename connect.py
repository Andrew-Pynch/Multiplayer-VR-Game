import socket
import sys

try: 
    s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    print("Socket successfully created")
except socket.error as err:
    print(f"Socket creation failed with error {err}")

port = 26950
host_ip = "127.0.0.1"


s.connect((host_ip, port))

print(f"Successfully connected on {host_ip}{port}")
