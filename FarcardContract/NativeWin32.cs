using FarcardContract.Farcard6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FarcardContract
{
    public class NativeWin32
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr LoadLibrary(string lpFileName);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool FreeLibrary(IntPtr hModule);

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("kernel32.dll")]
        public static extern int GetLastError();

        public static T GetDelegate<T>(IntPtr hModule, string nameAction, bool throws = false) where T: Delegate
        {
            T result = default(T);
            try
            {
                var pointer = NativeWin32.GetProcAddress(hModule, nameAction);
                if (pointer == IntPtr.Zero)
                    throw new NullReferenceException($"не найден обязательный метод {nameAction}");
                result = Marshal.GetDelegateForFunctionPointer(pointer, typeof(T)) as T;
                if (result == null)
                    throw new NullReferenceException($"Не удалось получить делегат {nameAction} по адресу {pointer.ToInt64()}");

            }
            catch (Exception ex)
            {
                if (throws)
                {
                    throw ex;
                }
            }

            return result;
        }
    }
}
