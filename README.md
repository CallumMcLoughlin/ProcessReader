# ProcessReader
### Description
This is a simple program to allow reading memory of another program through the use of platform invoking on Windows to call native C functions.

### Usage
A sample with comments has been included as Program.cs which is not necessary but provides a nice starting point to using these classes.

Quick setup:

* Instantiate a new ProcessOpener class and open the process by string 
```cs
OpenProcess("Notepad");
```

* Get the process handle
```cs
GetProcessHandle(ProcessHere);
```

* Optionally get the base address of the process 
```cs
GetProcessBaseAddress(myProcess);
```

* Get the process handle by instantiating a new ProcessReader
```cs
new ProcessReader(pHandle);
```

* Then to read the process memory call
```cs
ReadMemory<T>(MemoryAddress);
```
