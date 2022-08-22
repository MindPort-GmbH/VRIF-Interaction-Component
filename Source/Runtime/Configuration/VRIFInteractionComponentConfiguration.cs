using VRBuilder.Core.Configuration;

namespace VRBuilder.VRIF.Configuration
{
    /// <summary>
    /// Interaction component configuration for the VRIF integration.
    /// </summary>
    public class VRIFInteractionComponentConfiguration : IInteractionComponentConfiguration
    {
        /// <inheritdoc/>
        public string DisplayName => "VRIF Interaction Component";

        /// <inheritdoc/>
        public bool IsXRInteractionComponent => true;

        /// <inheritdoc/>
        public string DefaultRigPrefab => "[XR Rig Advanced]";
    }
}