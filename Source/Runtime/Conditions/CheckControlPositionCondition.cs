using System.Runtime.Serialization;
using UnityEngine;
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
            [DisplayName("Min position")]
            public float MinPosition { get; set; }

            [DataMember]
            [DisplayName("Max position")]
            public float MaxPosition { get; set; }

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
                Data.ControlProperty.Value.FastForwardPosition(Data.MinPosition + (Data.MaxPosition - Data.MinPosition) / 2);
            }
        }

        private class ActiveProcess : BaseActiveProcessOverCompletable<EntityData>
        {
            protected override bool CheckIfCompleted()
            {
                if(Data.MinPosition > Data.MaxPosition)
                {
                    Debug.LogError($"{typeof(CheckControlPositionCondition).Name} for object {Data.ControlProperty.UniqueName} will never complete as the minimum value is greater than the maximum value.");
                }

                if(Data.RequireRelease && Data.ControlProperty.Value.IsInteracting)
                {
                    return false;
                }

                return Data.ControlProperty.Value.Position >= Data.MinPosition && Data.ControlProperty.Value.Position <= Data.MaxPosition;
            }

            public ActiveProcess(EntityData data) : base(data)
            {
            }
        }

        public CheckControlPositionCondition() : this("", 0)
        {
        }

        public CheckControlPositionCondition(ILinearControlProperty control, float minPosition, float maxPosition, bool requireRelease = false, string name = null) : this(ProcessReferenceUtils.GetNameFrom(control), minPosition, maxPosition, requireRelease, name)
        {
        }

        public CheckControlPositionCondition(string controlName, float minPosition, float maxPosition = 0.1f, bool requireRelease = false, string name = "Check Control Position")
        {
            Data.ControlProperty = new ScenePropertyReference<ILinearControlProperty>(controlName);
            Data.MinPosition = minPosition;
            Data.MaxPosition = maxPosition;
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