using BNG;
using System;
using UnityEngine;
using VRBuilder.Core.Properties;

namespace VRBuilder.VRIF.Properties
{
    [AddComponentMenu("VR Builder/Properties/VRIF/Wheel Property (VRIF)")]
    [RequireComponent(typeof(SteeringWheel))]
    [RequireComponent(typeof(GrabbableProperty))]
    public class WheelProperty : LockableProperty, ILinearControlProperty
    {
        private SteeringWheel wheel;

        public SteeringWheel Wheel
        {
            get
            {
                if (wheel == null)
                {
                    wheel = GetComponent<SteeringWheel>();
                }

                return wheel;
            }
        }


        private GrabbableProperty grabbableProperty;

        public GrabbableProperty GrabbableProperty
        {
            get
            {
                if (grabbableProperty == null)
                {
                    grabbableProperty = GetComponent<GrabbableProperty>();
                }

                return grabbableProperty;
            }
        }

        public float Position => Wheel.GetScaledValue(Wheel.Angle, Wheel.MinAngle, Wheel.MaxAngle);

        public bool IsInteracting => GrabbableProperty.IsGrabbed;

        public event EventHandler<EventArgs> MinPosition;
        public event EventHandler<EventArgs> MaxPosition;
        public event EventHandler<EventArgs> ChangedPosition;

        protected override void OnEnable()
        {
            base.OnEnable();
            Wheel.onValueChange.AddListener(HandleValueChanged);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            Wheel.onValueChange.RemoveListener(HandleValueChanged);
        }

        private void HandleValueChanged(float value)
        {
            ChangedPosition?.Invoke(this, EventArgs.Empty);

            if(value <= -1)
            {
                MinPosition?.Invoke(this, EventArgs.Empty);
            }

            if(value >= 1)
            {
                MaxPosition?.Invoke(this, EventArgs.Empty);
            }

            Debug.Log(value);
        }

        public void FastForwardPosition(float position)
        {
            HandleValueChanged(position);
        }

        protected override void InternalSetLocked(bool lockState)
        {
        }
    }
}