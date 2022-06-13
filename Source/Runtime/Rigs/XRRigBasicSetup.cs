using VRBuilder.BasicInteraction.RigSetup;

namespace VRBuilder.VRIF.Rigs
{
    /// <summary>
    /// Setup for the basic XR rig.
    /// </summary>
    public class XRRigBasicSetup : InteractionRigProvider
    {
        /// <inheritdoc/>
        public override string Name => "XR Rig Basic";

        /// <inheritdoc/>
        public override string PrefabName => "[XR Rig Barebones]";
    }
}
