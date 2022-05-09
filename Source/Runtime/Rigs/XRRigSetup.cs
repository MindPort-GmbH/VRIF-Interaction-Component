using VRBuilder.BasicInteraction.RigSetup;

namespace VRBuilder.VRIF.Rigs
{
    /// <summary>
    /// Setup for the standard XR rig.
    /// </summary>
    public class XRRigSetup : InteractionRigProvider
    {
        public override string Name => "XR Rig";

        public override string PrefabName => "XR Rig";
    }
}
