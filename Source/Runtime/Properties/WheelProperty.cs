using BNG;
using System;
using UnityEngine;
using VRBuilder.Core.Properties;

namespace VRBuilder.VRIF.Properties
{
    /// <summary>
    /// Property that reads the position of a VRIF driving wheel as a -1 to 1 value.
    /// </summary>
    [AddComponentMenu("VR Builder/Properties/VRIF/Wheel Property (VRIF)")]
    [RequireComponent(typeof(SteeringWheel))]
    [RequireComponent(typeof(GrabbableProperty))]
    public class WheelProperty : LockableProperty, IContinuousControlProperty
    {
        private SteeringWheel wheel;

        /// <summary>
        /// Steering wheel component on this game object.
        /// </summary>
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

        /// <summary>
        /// Grabbable property on this game object.
        /// </summary>
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

        /// <summary>
        /// Position of the wheel as a -1 to 1 value.
        /// </summary>
        public float Position => Wheel.GetScaledValue(Wheel.Angle, Wheel.MinAngle, Wheel.MaxAngle);

        /// <inheritdoc/>
        public bool IsInteracting => GrabbableProperty.IsGrabbed;

        /// <inheritdoc/>
        public event EventHandler<EventArgs> MinPosition;

        /// <inheritdoc/>
        public event EventHandler<EventArgs> MaxPosition;

        /// <inheritdoc/>
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