#!/bin/bash
#!Start the client on linux - Assumes that the browser extension is already loaded

# Set discord client ID
export YT_CLIENT_ID=1034297846100394056

# Get the directory where the script is located
SCRIPT_DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" &> /dev/null && pwd )"

# Invoke the ytext binary for linux
$SCRIPT_DIR/bin/Release/net6.0/linux-x64/ytext
