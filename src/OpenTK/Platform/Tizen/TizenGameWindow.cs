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
using OpenTK.Graphics;

namespace OpenTK.Platform.Tizen
{
    internal class TizenGameWindow : GameWindow, ITizenWindowAttributes
    {
        public TizenGameWindow()
            : this(GraphicsMode.Default, DisplayDevice.Default, 2, 0)
        {
        }

        public TizenGameWindow(GraphicsMode mode, DisplayDevice device, int major, int minor)
            : this(GraphicsMode.Default, DisplayDevice.Default, "", 2, 0)
        {
        }

        public TizenGameWindow(GraphicsMode mode, DisplayDevice device, string title, int major, int minor)
            : base(device.Width, device.Height, mode, title, GameWindowFlags.Fullscreen, device, major, minor, GraphicsContextFlags.Embedded)
        {
            SDL2.SDL.SetHint("SDL_IOS_ORIENTATIONS", "Portrait LandscapeLeft LandscapeRight PortraitUpsideDown");
            Paused = false;
        }

        public bool Paused { get; set; }

        public bool IsFocusAllowed
        {
            get
            {
                return SDL2.SDL.SDL_Tizen_GetWindowAcceptFocus(WindowInfo.Handle);
            }

            set
            {
                SDL2.SDL.SDL_Tizen_SetWindowAcceptFocus(WindowInfo.Handle, value);
            }
        }

        public float WindowOpacity
        {
            get
            {
                float opacity = 0.0f;
                SDL2.SDL.SDL_GetWindowOpacity(WindowInfo.Handle, out opacity);
                return opacity;
            }

            set
            {
                SDL2.SDL.SDL_SetWindowOpacity(WindowInfo.Handle, value);
            }
        }

        public IntPtr NativeHandle
        {
            get
            {
                return SDL2.SDL.SDL_Tizen_GetNativeWindow(WindowInfo.Handle);
            }
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            if (!Paused)
            {
                base.OnUpdateFrame(e);
            }
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            if (!Paused)
            {
                base.OnRenderFrame(e);
            }
        }

        public new void Run()
        {
            throw new NotSupportedException();
        }

        public new void Run(double updateRate)
        {
            throw new NotSupportedException();
        }

        public new void Run(double updates_per_second, double frames_per_second)
        {
            throw new NotSupportedException();
        }

        internal void RunInternal(double updates_per_second, double frames_per_second)
        {
            base.Run(updates_per_second, frames_per_second);
        }

        public uint AddAuxiliaryHint(string hint, string value)
        {
            return SDL2.SDL.SDL_Tizen_AddAuxiliaryHint(WindowInfo.Handle, hint, value);
        }

    }
}
