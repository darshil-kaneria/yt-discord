# yt-discord
Unofficial Youtube Music integration for Discord. Contains an extension and a mini C# server which doubles as a Discord SDK client.

## Setup Discord client
The extension desktop client is written in C# and is essentially a server which listens to events from the extension. The client updates user presence on discord with the appropriate status.

To setup the client, make sure you have Discord running. Set `YT_CLIENT_ID=1034297846100394056` as an environment variable. Then run the `ytext` binary in `bin/Release/net6.0/linux-x64/`. The binary is self contained so no additional dependencies are required.

## Setup the extension
Load `ytext.js` into Firefox.

Go to https://music.youtube.com and start listening a track to have it show up on your Discord status.
