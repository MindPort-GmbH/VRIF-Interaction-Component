using VRBuilder.Core.Conditions;
using VRBuilder.Core.Editor.UI.StepInspector.Menu;
using VRBuilder.VRIF.Conditions;

namespace VRBuilder.Editor.VRIF.UI.Conditions
{
    /// <summary>
    /// Menu item for the <see cref="CheckControlPositionCondition"/> condition.
    /// </summary>
    public class CheckControlPositionMenuItem : MenuItem<ICondition>
    {
        public override string DisplayedName => "Interaction/Check Control Position";

        public override ICondition GetNewItem()
        {
            return new CheckControlPositionCondition();
        }
    }
}