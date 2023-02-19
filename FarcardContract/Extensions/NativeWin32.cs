using System;
using System.Runtime.InteropServices;

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

        public static T GetDelegate<T>(IntPtr hModule, string nameAction, bool throws = false) where T : Delegate
        {
            T result = default(T);
            try
            {
                var pointer = NativeWin32.GetProcAddress(hModule, nameAction);
                if (pointer != IntPtr.Zero)
                {
                    result = Marshal.GetDelegateForFunctionPointer(pointer, typeof(T)) as T;
                    if (result == null&&throws)
                        throw new NullReferenceException($"Не удалось получить делегат {nameAction} по адресу {pointer.ToInt64()}");
                }
                else if (throws)
                    throw new NullReferenceException($"Не найден обязательный метод {nameAction}");

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
