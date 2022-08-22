using System;
using VRBuilder.Core.Properties;

namespace VRBuilder.VRIF.Properties
{
    /// <summary>
    /// Interface for interactable objects that can be set to a continuous value within a range.
    /// </summary>
    public interface IContinuousControlProperty : ISceneObjectProperty
    {
        /// <summary>
        /// Called when the control is at its minimum position.
        /// </summary>
        event EventHandler<EventArgs> MinPosition;

        /// <summary>
        /// Called when the control is at its maximum position.
        /// </summary>
        event EventHandler<EventArgs> MaxPosition;

        /// <summary>
        /// Called when the position of the control has changed.
        /// </summary>
        event EventHandler<EventArgs> ChangedPosition;

        /// <summary>
        /// Normalized control position.
        /// </summary>
        float Position { get; }

        /// <summary>
        /// True when the object is being grabbed.
        /// </summary>
        bool IsInteracting { get; }

        /// <summary>
        /// Fast forward to the specified position.
        /// </summary>        
        void FastForwardPosition(float position);
    }
}