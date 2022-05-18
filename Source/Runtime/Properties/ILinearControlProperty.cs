using System;

namespace VRBuilder.VRIF.Properties
{
    public interface ILinearControlProperty
    {
        event EventHandler<EventArgs> MinPosition;
        event EventHandler<EventArgs> MaxPosition;
        event EventHandler<EventArgs> ChangedPosition;

        float Position { get; }

        void FastForwardPosition(float position);
    }
}