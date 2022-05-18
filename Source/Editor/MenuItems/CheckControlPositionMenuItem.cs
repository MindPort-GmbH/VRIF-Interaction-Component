using VRBuilder.Core.Conditions;
using VRBuilder.Editor.UI.StepInspector.Menu;
using VRBuilder.VRIF.Conditions;

namespace VRBuilder.Editor.VRIF.UI.Conditions
{
    public class CheckControlPositionMenuItem : MenuItem<ICondition>
    {
        public override string DisplayedName => "Interaction/Check Control Position";

        public override ICondition GetNewItem()
        {
            return new CheckControlPositionCondition();
        }
    }
}