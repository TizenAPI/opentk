using System;
using System.Collections.Generic;
using System.Text;

namespace OpenTK.Platform.Tizen
{
    /// <summary>
    /// Describes Tizen window attributes.
    /// </summary>
    public interface ITizenWindowAttributes
    {
        /// <summary>
        /// Gets or sets whether the window accepts a focus or not.
        /// </summary>
        bool IsFocusAllowed { get; set; }

        /// <summary>
        /// Gets or sets opacity of the window
        /// </summary>
        float WindowOpacity { get; set; }

        /// <summary>
        /// Gets the native window handle.
        /// </summary>
        IntPtr NativeHandle { get; }

        /// <summary>
        /// Adds a supported auxiliary hint to the window
        /// </summary>
        /// <param name="hint">The auxiliary hint string</param>
        /// <param name="value">The value string to be set</param>
        /// <returns></returns>
        uint AddAuxiliaryHint(string hint, string value);
    }
}
