//
// The Open Toolkit Library License
//
// Copyright 2018 Samsung Electronics
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
// the Software, and to permit persons to whom the Software is furnished to do
// so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using System.Security;
using System.Runtime.InteropServices;

namespace OpenTK.Platform.SDL2
{
    internal partial class SDL
    {
        [SuppressUnmanagedCodeSecurity]
        [DllImport(lib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_SetMainReady", ExactSpelling = true)]
        public static extern void SetMainReady();

        [SuppressUnmanagedCodeSecurity]
        [DllImport(lib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_tizen_app_init", ExactSpelling = true)]
        public static extern int TizenAppInit(int argc, string[] argv);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(lib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_SetHint", ExactSpelling = true)]
        public static extern IntPtr SetHint(string name, string value);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(lib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_Tizen_GetSupportedAuxiliaryHintCount", ExactSpelling = true)]
        public static extern uint SDL_Tizen_GetSupportedAuxiliaryHintCount(IntPtr window);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(lib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_Tizen_GetSupportedAuxiliaryHint", ExactSpelling = true)]
        public static extern string SDL_Tizen_GetSupportedAuxiliaryHint(IntPtr window, uint index);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(lib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_Tizen_AddAuxiliaryHint", ExactSpelling = true)]
        public static extern uint SDL_Tizen_AddAuxiliaryHint(IntPtr window, string hint, string value);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(lib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_Tizen_RemoveAuxiliaryHint", ExactSpelling = true)]
        public static extern bool SDL_Tizen_RemoveAuxiliaryHint(IntPtr window, uint id);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(lib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_Tizen_SetAuxiliaryHint", ExactSpelling = true)]
        public static extern bool SDL_Tizen_SetAuxiliaryHint(IntPtr window, uint id, string value);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(lib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_Tizen_GetAuxiliaryHintValue", ExactSpelling = true)]
        public static extern string SDL_Tizen_GetAuxiliaryHintValue(IntPtr window, uint id);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(lib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_Tizen_SetWindowAcceptFocus", ExactSpelling = true)]
        public static extern bool SDL_Tizen_SetWindowAcceptFocus(IntPtr window, bool accept);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(lib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_Tizen_GetWindowAcceptFocus", ExactSpelling = true)]
        public static extern bool SDL_Tizen_GetWindowAcceptFocus(IntPtr window);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(lib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_Tizen_GetNativeWindow", ExactSpelling = true)]
        public static extern IntPtr SDL_Tizen_GetNativeWindow(IntPtr window);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(lib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_SetWindowOpacity", ExactSpelling = true)]
        public static extern int SDL_SetWindowOpacity(IntPtr window, float opacity);

        [SuppressUnmanagedCodeSecurity]
        [DllImport(lib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDL_GetWindowOpacity", ExactSpelling = true)]
        public static extern int SDL_GetWindowOpacity(IntPtr window, out float opacity);
    }
}
