using System;
using VRBuilder.Core.Properties;

namespace VRBuilder.VRIF.Properties
{
    public interface ILinearControlProperty : ISceneObjectProperty
    {
        event EventHandler<EventArgs> MinPosition;
        event EventHandler<EventArgs> MaxPosition;
        event EventHandler<EventArgs> ChangedPosition;

        float Position { get; }

        bool IsInteracting { get; }

        void FastForwardPosition(float position);
    }
}