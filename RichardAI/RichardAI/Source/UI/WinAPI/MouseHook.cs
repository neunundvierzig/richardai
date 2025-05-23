﻿using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace RichardAI.Source.UI.WinAPI
{
    internal partial class MouseHook : IHookable, IDisposable
    {
        private IntPtr _hookID = IntPtr.Zero;
        private LowLevelMouseProc _proc;
        public event Action<MouseButton> OnMouseDown;
        public MouseHook()
        {
            _proc = HookCallback;
        }

        public void Hook() 
        {
            if (_hookID == IntPtr.Zero)
                _hookID = SetHook(_proc);
        }

        public void Unhook()
        {
            if (_hookID != IntPtr.Zero)
            {
                UnhookWindowsHookEx(_hookID);
                _hookID = IntPtr.Zero;
            }
        }

        public void Dispose() // inherited from IDisposable
        {
            Unhook();
        }

        private IntPtr SetHook(LowLevelMouseProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_MOUSE_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                int msg = wParam.ToInt32();
                if (msg == WM_LBUTTONDOWN)
                    OnMouseDown?.Invoke(MouseButton.Left);
                else if (msg == WM_RBUTTONDOWN)
                    OnMouseDown?.Invoke(MouseButton.Right);
                else if (msg == WM_MBUTTONDOWN)
                    OnMouseDown?.Invoke(MouseButton.Middle);
            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        #region WinAPI // better not to check what inside

        private const int WH_MOUSE_LL = 14;
        private const int WM_LBUTTONDOWN = 0x0201;
        private const int WM_RBUTTONDOWN = 0x0204;
        private const int WM_MBUTTONDOWN = 0x0207;

        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        #endregion
    }
}
