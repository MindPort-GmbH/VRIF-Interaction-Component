using VRBuilder.BasicInteraction.RigSetup;

namespace VRBuilder.VRIF.Rigs
{
    /// <summary>
    /// Setup for the standard XR rig.
    /// </summary>
    public class XRRigSetup : InteractionRigProvider
    {
        /// <inheritdoc/>
        public override string Name => "XR Rig";

        /// <inheritdoc/>
        public override string PrefabName => "[XR Rig]";
    }
}
