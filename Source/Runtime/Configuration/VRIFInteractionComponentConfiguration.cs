using VRBuilder.Core.Configuration;

namespace VRBuilder.VRIF.Configuration
{
    public class VRIFInteractionComponentConfiguration : IInteractionComponentConfiguration
    {
        /// <inheritdoc/>
        public string DisplayName => "VRIF Interaction Component";

        /// <inheritdoc/>
        public bool IsXRInteractionComponent => true;
    }
}