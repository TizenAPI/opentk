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
        /// Get or set whether the window accepts a focus or not.
        /// </summary>
        bool IsFocusAllowed { get; set; }

        /// <summary>
        /// Get or set opacity of the window
        /// </summary>
        float WindowOpacity { get; set; }

        /// <summary>
        /// Add a supported auxiliary hint to the window
        /// </summary>
        /// <param name="hint">The auxiliary hint string</param>
        /// <param name="value">The value string to be set</param>
        /// <returns></returns>
        uint AddAuxiliaryHint(string hint, string value);
    }
}
