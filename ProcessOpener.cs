using System;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace ProcessReader
{
    public class ProcessOpener
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        public ProcessOpener()
        {
        }

        /// <summary>
        /// Open process by name
        /// </summary>
        /// <param name="sProcessName">Name of process to returned</param>
        /// <returns>
        /// Returns first process that fits given string
        /// </returns>
        public Process OpenProcess(string sProcessName)
        {
            Process[] processes = Process.GetProcessesByName(sProcessName);
            Process proc = null;

            if (processes.Length > 0)
                proc = Process.GetProcessesByName(sProcessName)[0];
            return proc;
        }

        /// <summary>
        /// Get process handle from process
        /// </summary>
        /// <param name="process">Process to get pHandle from</param>
        /// <returns>
        /// Process Handle IntPtr
        /// </returns>
        public IntPtr GetProcessHandle(Process process)
        {
            return OpenProcess((int)AccessType.PROCESS_VM_READ, false, process.Id);
        }

        /// <summary>
        /// Get process base address 
        /// </summary>
        /// <param name="process">Process to get base address from</param>
        /// <returns>Pointer to process base address</returns>
        public IntPtr GetProcessBaseAddress(Process process)
        {
            return process.MainModule.BaseAddress;
        }
    }
}
