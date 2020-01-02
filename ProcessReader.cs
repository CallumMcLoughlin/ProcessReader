using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ProcessReader
{
    class ProcessReader
    {
        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int dwSize, out IntPtr lpNumberOfBytesRead);

        IntPtr pHandle;
        /// <summary>
        /// Assign process to ProcessReader
        /// </summary>
        /// <param name="hProcess"></param>
        public ProcessReader(IntPtr hProcess)
        {
            pHandle = hProcess;
        }

        /// <summary>
        /// Read memory from address.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="address">Address to read memory from</param>
        /// <param name="bufferLength">How much memory to read, if left blank will get size by Marshal.SizeOf type</param>
        /// <returns>
        /// Typeof(T) object at memory address
        /// </returns>
        public T ReadMemory<T>(IntPtr address, int bufferLength = 0) where T : unmanaged
        {
            IntPtr bytesRead = IntPtr.Zero;
            if (bufferLength == 0)
                bufferLength = Marshal.SizeOf(typeof(T));
            byte[] result = new byte[bufferLength];
            ReadProcessMemory(pHandle, address, result, bufferLength, out bytesRead);

            if (bytesRead == IntPtr.Zero || bytesRead.ToInt32() < result.Length)
                return default(T);
            return Unsafe.As<byte, T>(ref result[0]);
        }

        /// <summary>
        /// Add offset to base pointer then read pointer from memory address at that location.
        /// </summary>
        /// <param name="basePointer"></param>
        /// <param name="offset"></param>
        /// <returns>
        /// IntPtr from new memory address
        /// </returns>
        public IntPtr GetOffsetPointer(IntPtr basePointer, int offset)
        {
            IntPtr offsetAddress = IntPtr.Add(basePointer, offset);
            return new IntPtr(ReadMemory<int>(offsetAddress));
        }
        
        /// <summary>
        /// Get nested pointer from base pointer and series of memory address offsets.
        /// </summary>
        /// <param name="basePointer"></param>
        /// <param name="offsets"></param>
        /// <returns>
        /// IntPtr from nested pointer.
        /// </returns>
        public IntPtr GetOffsetPointer(IntPtr basePointer, int[] offsets)
        {
            IntPtr currentPointer = basePointer;
            for (int i = 0; i < offsets.Length - 1; i++)
            {
                currentPointer = GetOffsetPointer(currentPointer, offsets[i]);
            }
            return IntPtr.Add(currentPointer, offsets[offsets.Length - 1]);
        }
    }
}
