using VRBuilder.Core.Conditions;
using VRBuilder.Editor.UI.StepInspector.Menu;
using VRBuilder.VRIF.Conditions;

namespace VRBuilder.Editor.VRIF.UI.Conditions
{
    public class LinearControlAtPositionMenuItem : MenuItem<ICondition>
    {
        public override string DisplayedName => "Interaction/Linear Control at Position";

        public override ICondition GetNewItem()
        {
            return new LinearControlAtPositionCondition();
        }
    }
}