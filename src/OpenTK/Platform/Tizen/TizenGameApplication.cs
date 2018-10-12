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

using Tizen.Applications;

using OpenTK.Graphics;
using OpenTK.Platform.SDL2;

namespace OpenTK.Platform.Tizen
{
    /// <summary>
    /// Represents an application that have a GameWindow of OpenTK.
    /// </summary>
    public class TizenGameApplication : CoreUIApplication
    {
        private TizenGameWindow window;

        /// <summary>
        /// Gets the GameWindow instance that created in OnCreate() method.
        /// </summary>
        public IGameWindow Window => window;

        /// <summary>
        /// Gets the Window Attributes instance that include the window attributes could be changed by the user.
        /// </summary>
        public ITizenWindowAttributes WindowAttributes => window;

        /// <summary>
        /// The major version for the OpenGL GraphicsContext.
        /// </summary>
        public int GLMajor { get; set; } = 2;

        /// <summary>
        /// The minor version for the OpenGL GraphicsContext.
        /// </summary>
        public int GLMinor { get; set; } = 0;

        /// <summary>
        /// The format for graphics operations.
        /// </summary>
        public Graphics.GraphicsMode GraphicsMode { get; set; } = Graphics.GraphicsMode.Default;

        /// <summary>
        /// Initializes the TizenGameApplication class.
        /// </summary>
        public TizenGameApplication() : base(new TizenGameCoreBackend())
        {
        }

        /// <summary>
        /// Overrides this method if you want to handle the behavior when the application is resumed.
        /// If base.OnResume() is not called, the event 'Resumed' will not be emitted.
        /// </summary>
        protected override void OnResume()
        {
            base.OnResume();
        }

        /// <summary>
        /// Overrides this method if you want to handle the behavior when the application is paused.
        /// If base.OnPause() is not called, the event 'Paused' will not be emitted.
        /// </summary>
        protected override void OnPause()
        {
            base.OnPause();
        }

        /// <summary>
        /// Runs the UI application's main loop. The GameWindow uses the maximum update rate.
        /// </summary>
        /// <param name="args">Arguments from the commandline.</param>
        public override void Run(string[] args)
        {
            Run(args, 0.0, 0.0);
        }

        /// <summary>
        /// Runs the UI application's main loop. The GameWindow uses the specified update rate.
        /// </summary>
        /// <param name="args">Arguments from the commandline.</param>
        /// <param name="updatesPerSecond">The frequency of UpdateFrame events.</param>
        public void Run(string[] args, double updatesPerSecond)
        {
            Run(args, updatesPerSecond, 0.0);
        }

        /// <summary>
        /// Runs the UI application's main loop. The GameWindow updates and renders at the specified frequency.
        /// </summary>
        /// <param name="args">Arguments from the commandline.</param>
        /// <param name="updatesPerSecond">The frequency of UpdateFrame events.</param>
        /// <param name="framesPerSecond">The frequency of RenderFrame events.</param>
        public void Run(string[] args, double updatesPerSecond, double framesPerSecond)
        {
            // Initialize SDL2
            SDL.TizenAppInit(args.Length, args);
            SDL.SetMainReady();
            Toolkit.Init();

            // Set Internal Event Handlers
            Backend.AddEventHandler(TizenGameCoreBackend.InternalCreateEventType, () =>
            {
                // In Tizen SDL Backend, GL Attributes should be set before creating the window.
                SetGLAttributes(GraphicsMode, GLMajor, GLMinor);
                window = new TizenGameWindow(GraphicsMode, DisplayDevice.Default, Current.ApplicationInfo.ExecutablePath, GLMajor, GLMinor);
            });

            Backend.AddEventHandler(TizenGameCoreBackend.InternalTerminateEventType, () =>
            {
                window.Exit();
            });

            Backend.AddEventHandler(TizenGameCoreBackend.InternalResumeEventType, () =>
            {
                window.Paused = false;
            });

            Backend.AddEventHandler(TizenGameCoreBackend.InternalPauseEventType, () =>
            {
                window.Paused = true;
            });


            // Configure callbacks for application events
            base.Run(args);

            // Run mainloop
            window.RunInternal(updatesPerSecond, framesPerSecond);
        }

        /// <summary>
        /// Exits the main loop of the application.
        /// </summary>
        public override void Exit()
        {
            window.Exit();
        }

        private static void SetGLAttributes(GraphicsMode mode, int major, int minor)
        {
            SDL.GL.SetAttribute(ContextAttribute.ACCUM_ALPHA_SIZE, 0);
            SDL.GL.SetAttribute(ContextAttribute.ACCUM_RED_SIZE, 0);
            SDL.GL.SetAttribute(ContextAttribute.ACCUM_GREEN_SIZE, 0);
            SDL.GL.SetAttribute(ContextAttribute.ACCUM_BLUE_SIZE, 0);
            SDL.GL.SetAttribute(ContextAttribute.DOUBLEBUFFER, 0);
            SDL.GL.SetAttribute(ContextAttribute.ALPHA_SIZE, 0);
            SDL.GL.SetAttribute(ContextAttribute.RED_SIZE, 0);
            SDL.GL.SetAttribute(ContextAttribute.GREEN_SIZE, 0);
            SDL.GL.SetAttribute(ContextAttribute.BLUE_SIZE, 0);
            SDL.GL.SetAttribute(ContextAttribute.DEPTH_SIZE, 0);
            SDL.GL.SetAttribute(ContextAttribute.MULTISAMPLEBUFFERS, 0);
            SDL.GL.SetAttribute(ContextAttribute.MULTISAMPLESAMPLES, 0);
            SDL.GL.SetAttribute(ContextAttribute.STENCIL_SIZE, 0);
            SDL.GL.SetAttribute(ContextAttribute.STEREO, 0);

            if (mode.AccumulatorFormat.BitsPerPixel > 0)
            {
                SDL.GL.SetAttribute(ContextAttribute.ACCUM_ALPHA_SIZE, mode.AccumulatorFormat.Alpha);
                SDL.GL.SetAttribute(ContextAttribute.ACCUM_RED_SIZE, mode.AccumulatorFormat.Red);
                SDL.GL.SetAttribute(ContextAttribute.ACCUM_GREEN_SIZE, mode.AccumulatorFormat.Green);
                SDL.GL.SetAttribute(ContextAttribute.ACCUM_BLUE_SIZE, mode.AccumulatorFormat.Blue);
            }

            if (mode.Buffers > 0)
            {
                SDL.GL.SetAttribute(ContextAttribute.DOUBLEBUFFER, mode.Buffers > 1 ? 1 : 0);
            }

            if (mode.ColorFormat > 0)
            {
                SDL.GL.SetAttribute(ContextAttribute.ALPHA_SIZE, mode.ColorFormat.Alpha);
                SDL.GL.SetAttribute(ContextAttribute.RED_SIZE, mode.ColorFormat.Red);
                SDL.GL.SetAttribute(ContextAttribute.GREEN_SIZE, mode.ColorFormat.Green);
                SDL.GL.SetAttribute(ContextAttribute.BLUE_SIZE, mode.ColorFormat.Blue);
            }

            if (mode.Depth > 0)
            {
                SDL.GL.SetAttribute(ContextAttribute.DEPTH_SIZE, mode.Depth);
            }

            if (mode.Samples > 0)
            {
                SDL.GL.SetAttribute(ContextAttribute.MULTISAMPLEBUFFERS, 1);
                SDL.GL.SetAttribute(ContextAttribute.MULTISAMPLESAMPLES, mode.Samples);
            }

            if (mode.Stencil > 0)
            {
                SDL.GL.SetAttribute(ContextAttribute.STENCIL_SIZE, 1);
            }

            if (mode.Stereo)
            {
                SDL.GL.SetAttribute(ContextAttribute.STEREO, 1);
            }

            if (major > 0)
            {
                SDL.GL.SetAttribute(ContextAttribute.CONTEXT_MAJOR_VERSION, major);
                SDL.GL.SetAttribute(ContextAttribute.CONTEXT_MINOR_VERSION, minor);
            }

            SDL.GL.SetAttribute(ContextAttribute.CONTEXT_EGL, 1);
            SDL.GL.SetAttribute(ContextAttribute.CONTEXT_PROFILE_MASK, ContextProfileFlags.ES);
        }
    }
}
