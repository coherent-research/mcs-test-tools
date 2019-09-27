# Tools
## McsRequestClient
This tool will take a CSV file containg data collection requests and send them to a server running the MCS Request Service.

The CSV file must be in the same format as an MTS batch file. The syntax for the command is

```
requestClient -i <INPUT_FILE> [-p <PRIORITY>]
```
where INPUT_FILE is the path to the CSV file and PRIORITY is the priority given
to all requests (default = 1).

The tool requires 2 other settings which are defined in the *appSettings.json* file. These are:

- McsRequestUrl: this is the URL where the request will be sent (i.e. the running the MCS Request Service)
- McsResultUrl: this is the URL where the results will be sent. This URL will be included in each requests as the ResultUrl. 
Note that this URL must be contactable from the server hosting the MCS Request Service.
## McsResultHost
This tool implements the MCS Result Service and listens for results from the MCS Request Service. When the results are received they are simply displayed on the console.

The sytnax for the command is:

```
resultHost -p <LISTENING_PORT>
``` 
where LISTENING_PORT is the TCP port used to listening for incoming resuts.


# System requirement
To run either of these tools the .NET Core runtime version 2.2.x must be installed. This can be downloaded from

https://dotnet.microsoft.com/download/dotnet-core

> Note that these tools haven't been tested with .Net Core v3.0

# Build

These tools are built with Visual Studio 2019. 

The easiest way to build the application is to use "Publish" on the project context menu which will build a version with all the required binaries in a directory. 
Once this is done the files can be copied to another location and run from the command line.

# Quick Check
To quickly check that services are accessible the service-status method can be used from a browser.

e.g. to check that the MCS Request service is accessible from a particular location use a browser and enter the URL:
```
http://SERVER/service-status
```

If the service is accessible it will return a message with the status "OK" and the version number. This works for either service.


