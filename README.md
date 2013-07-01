#RetroShare SSH Client#

A lightweight SSH Client for RetroShare (nogui) Server.

##IRC Bridge##
This was a proof of concept done on a free weekend. 
__I don't plan to continue working on this!__

If you want to use it you'll need SmartIrc4net - https://github.com/meebey/SmartIrc4net
_I have no idea if the licences are compatible .. so no libs or binary :P_

##What is RetroShare?##

RetroShare is a secure decentralised communication platform.
http://retroshare.sourceforge.net/

##What are RSRPC and RetroShareSSHClient?##

__RSRPC__ is a .NET library which offers an interface to communicate with a RetroShare SSH server via RPC calls.
It manages the SSH connection and the send/receive process including (de)serialisation of data.

__RetroShareSSHClient__ is a gui client using RSRPC lib. Its goal is to provide a lightweight gui to control and maintain a RetroShare server.

__Both (RSRPC and RetroShareSSHClient) are using:__
* SSH.NET - http://sshnet.codeplex.com/
* protobuf-net - http://code.google.com/p/protobuf-net/

For more information on the RetroShare RPC system check out the git repository of its developer: 
https://github.com/drbob/pyrs#readme

##Requirements##
.NET Framework 4.0