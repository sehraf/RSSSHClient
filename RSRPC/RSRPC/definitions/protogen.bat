SET PROTO_PATH="C:\Program Files (x86)\protobuf-net\protobuf-net-VS9"
SET FILES_PATH="C:\Users\Michael\Dropbox\VB Projects\Visual Studio 2010\Projects\RetroShareSSHClient\RetroShareSSHClient\definitions"

for %%a in (%FILES_PATH%\*.proto) do %PROTO_PATH%\protogen.exe -i:"%%a" -o:"%%a.cs"

pause