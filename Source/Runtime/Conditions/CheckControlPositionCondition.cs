using System;
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
    /// <summary>
    /// Condition that checks if a control like a lever is within a specified range.
    /// </summary>
    [DataContract(IsReference = true)]
    public class CheckControlPositionCondition : Condition<CheckControlPositionCondition.EntityData>
    {
        [DisplayName("Check Control Position")]
        public class EntityData : IConditionData
        {
            [DataMember]
            [DisplayName("Control")]
            public SingleScenePropertyReference<IContinuousControlProperty> ContinuousControl { get; set; }

            [DataMember]
            [HideInProcessInspector]
            [Obsolete("Use ContinuousControl instead.")]
            [LegacyProperty(nameof(ContinuousControl))]
            public ScenePropertyReference<IContinuousControlProperty> ControlProperty { get; set; }

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
            public string Name => $"Set {ContinuousControl} between {MinPosition} and {MaxPosition}";

            public Metadata Metadata { get; set; }
        }

        private class EntityAutocompleter : Autocompleter<EntityData>
        {
            public EntityAutocompleter(EntityData data) : base(data)
            {
            }

            public override void Complete()
            {
                Data.ContinuousControl.Value.FastForwardPosition(Data.MinPosition + (Data.MaxPosition - Data.MinPosition) / 2);
            }
        }

        private class ActiveProcess : BaseActiveProcessOverCompletable<EntityData>
        {
            protected override bool CheckIfCompleted()
            {
                if (Data.MinPosition > Data.MaxPosition)
                {
                    Debug.LogError($"{typeof(CheckControlPositionCondition).Name} for object {Data.ContinuousControl} will never complete as the minimum value is greater than the maximum value.");
                }

                if (Data.RequireRelease && Data.ContinuousControl.Value.IsInteracting)
                {
                    return false;
                }

                return Data.ContinuousControl.Value.Position >= Data.MinPosition && Data.ContinuousControl.Value.Position <= Data.MaxPosition;
            }

            public ActiveProcess(EntityData data) : base(data)
            {
            }
        }

        public CheckControlPositionCondition() : this(Guid.Empty)
        {
        }

        public CheckControlPositionCondition(IContinuousControlProperty control, float minPosition, float maxPosition, bool requireRelease = false) : this(ProcessReferenceUtils.GetUniqueIdFrom(control), minPosition, maxPosition, requireRelease)
        {
        }

        public CheckControlPositionCondition(Guid control, float minPosition = 0, float maxPosition = 1, bool requireRelease = false)
        {
            Data.ContinuousControl = new SingleScenePropertyReference<IContinuousControlProperty>(control);
            Data.MinPosition = minPosition;
            Data.MaxPosition = maxPosition;
            Data.RequireRelease = requireRelease;
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