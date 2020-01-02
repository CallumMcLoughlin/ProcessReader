using System;
using System.Diagnostics;

namespace ProcessReader
{
    class Program
    {
        //Example program
        static void Main(string[] args)
        {
            ProcessOpener procOpener = new ProcessOpener(); //New process opener instance
            Process myProcess = procOpener.OpenProcess("Notepad"); //Grab process by string
            if (myProcess == null) //OpenProcess will return null if no process found
            {
                throw new Exception("Process not found"); //Throw
            }

            IntPtr pHandle = procOpener.GetProcessHandle(myProcess); //Get IntPtr (Handle) to process
            IntPtr pBaseAddress = procOpener.GetProcessBaseAddress(myProcess); //Get base address IntPtr

            ProcessReader procReader = new ProcessReader(pHandle); //Instantiate new process reader for program by using the handle
            float myFloat = procReader.ReadMemory<float>(pBaseAddress); //Read type, float in this case from memory location

            Console.WriteLine(myFloat);
            Console.ReadKey();
        }
    }
}
