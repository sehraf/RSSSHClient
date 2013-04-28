SET PROTO_PATH="D:\Program Files (x86)\ProtoGen"
SET FILES_PATH="D:\Visual Studio 2010\Projects\RSRPC\RSRPC\definitions"

for %%a in (%FILES_PATH%\*.proto) do %PROTO_PATH%\protogen.exe -i:"%%a" -o:"%%a.cs"

pause