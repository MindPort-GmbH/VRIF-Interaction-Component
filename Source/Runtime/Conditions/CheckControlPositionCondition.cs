using System.Runtime.Serialization;
using VRBuilder.Core;
using VRBuilder.Core.Attributes;
using VRBuilder.Core.Conditions;
using VRBuilder.Core.SceneObjects;
using VRBuilder.Core.Utils;
using VRBuilder.VRIF.Properties;

namespace VRBuilder.VRIF.Conditions
{
    [DataContract(IsReference = true)]
    public class CheckControlPositionCondition : Condition<CheckControlPositionCondition.EntityData>
    {
        [DisplayName("Check Control Position")]
        public class EntityData : IConditionData
        {
            [DataMember]
            [DisplayName("Control")]
            public ScenePropertyReference<ILinearControlProperty> ControlProperty { get; set; }

            [DataMember]
            [DisplayName("Required position")]
            public float RequiredPosition { get; set; }

            [DataMember]
            [DisplayName("Tolerance")]
            public float Tolerance { get; set; }

            [DataMember]
            [DisplayName("Require release")]
            public bool RequireRelease { get; set; }

            public bool IsCompleted { get; set; }

            [DataMember]
            [HideInProcessInspector]
            public string Name { get; set; }

            public Metadata Metadata { get; set; }
        }

        private class EntityAutocompleter : Autocompleter<EntityData>
        {
            public EntityAutocompleter(EntityData data) : base(data)
            {
            }

            public override void Complete()
            {
                Data.ControlProperty.Value.FastForwardPosition(Data.RequiredPosition);
            }
        }

        private class ActiveProcess : BaseActiveProcessOverCompletable<EntityData>
        {
            protected override bool CheckIfCompleted()
            {
                if(Data.RequireRelease && Data.ControlProperty.Value.IsInteracting)
                {
                    return false;
                }

                return Data.ControlProperty.Value.Position >= Data.RequiredPosition - Data.Tolerance && Data.ControlProperty.Value.Position <= Data.RequiredPosition + Data.Tolerance;
            }

            public ActiveProcess(EntityData data) : base(data)
            {
            }
        }

        public CheckControlPositionCondition() : this("", 0)
        {
        }

        public CheckControlPositionCondition(ILinearControlProperty control, float requiredPosition, float tolerance = 0.1f, bool requireRelease = false, string name = null) : this(ProcessReferenceUtils.GetNameFrom(control), requiredPosition, tolerance, requireRelease, name)
        {
        }

        public CheckControlPositionCondition(string controlName, float requiredPosition, float tolerance = 0.1f, bool requireRelease = false, string name = "Check Control Position")
        {
            Data.ControlProperty = new ScenePropertyReference<ILinearControlProperty>(controlName);
            Data.RequiredPosition = requiredPosition;
            Data.Tolerance = tolerance;
            Data.RequireRelease = requireRelease;
            Data.Name = name;
        }

        public override IStageProcess GetActiveProcess()
        {
            return new ActiveProcess(Data);
        }

        protected override IAutocompleter GetAutocompleter()
        {
            return new EntityAutocompleter(Data);
        }
    }
}