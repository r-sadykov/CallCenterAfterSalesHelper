﻿using System;
using System.Runtime.InteropServices;

namespace DinkToPdf.Utils
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void VoidCallback(IntPtr converter);
}
