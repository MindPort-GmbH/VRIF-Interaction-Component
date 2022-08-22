using VRBuilder.BasicInteraction.RigSetup;

namespace VRBuilder.VRIF.Rigs
{
    /// <summary>
    /// Setup for the rigidbody XR rig.
    /// </summary>
    public class XRRigRigidbodySetup : InteractionRigProvider
    {
        /// <inheritdoc/>
        public override string Name => "XR Rig Rigidbody";

        /// <inheritdoc/>
        public override string PrefabName => "[XR Rig Rigidbody]";
    }
}
