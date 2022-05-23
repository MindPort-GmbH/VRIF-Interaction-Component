using BNG;
using System;
using UnityEngine;
using VRBuilder.Core.Properties;

namespace VRBuilder.VRIF.Properties
{
    /// <summary>
    /// Property that reads the position of a VRIF lever property as a 0 to 1 value.
    /// </summary>
    [AddComponentMenu("VR Builder/Properties/VRIF/Lever Property (VRIF)")]
    [RequireComponent(typeof(Lever))]
    [RequireComponent(typeof(GrabbableProperty))]
    public class LeverProperty : LockableProperty, IContinuousControlProperty
    {
        private Lever lever;

        /// <summary>
        /// The Lever component on this game object.
        /// </summary>
        public Lever Lever
        {
            get
            {
                if (lever == null)
                {
                    lever = GetComponent<Lever>();
                }

                return lever;
            }
        }

        private GrabbableProperty grabbableProperty;

        /// <summary>
        /// The grabbable property on the same game object.
        /// </summary>
        public GrabbableProperty GrabbableProperty
        {
            get
            {
                if(grabbableProperty == null)
                {
                    grabbableProperty = GetComponent<GrabbableProperty>(); 
                }

                return grabbableProperty;
            }
        }

        /// <inheritdoc/>
        public event EventHandler<EventArgs> MinPosition;

        /// <inheritdoc/>
        public event EventHandler<EventArgs> MaxPosition;

        /// <inheritdoc/>
        public event EventHandler<EventArgs> ChangedPosition;

        /// <summary>
        /// Position of the lever as a 0 to 1 value.
        /// </summary>
        public float Position => Lever.LeverPercentage / 100;

        /// <inheritdoc/>
        public bool IsInteracting => GrabbableProperty.IsGrabbed;

        protected override void OnEnable()
        {
            base.OnEnable();
            Lever.onLeverChange.AddListener(HandleLeverChanged);
            Lever.onLeverDown.AddListener(HandleLeverDown);
            Lever.onLeverUp.AddListener(HandleLeverUp);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            Lever.onLeverChange.RemoveListener(HandleLeverChanged);
            Lever.onLeverDown.RemoveListener(HandleLeverDown);
            Lever.onLeverUp.RemoveListener(HandleLeverUp);
        }

        private void HandleLeverUp()
        {
            MaxPosition?.Invoke(this, EventArgs.Empty);
        }

        private void HandleLeverDown()
        {
            MinPosition?.Invoke(this, EventArgs.Empty); 
        }

        private void HandleLeverChanged(float percentage)
        {
            ChangedPosition?.Invoke(this, EventArgs.Empty);
        }

        public void FastForwardPosition(float position)
        {
            Lever.SetLeverAngle((Lever.MaximumXRotation - Lever.MinimumXRotation) * position + Lever.MinimumXRotation);
        }

        protected override void InternalSetLocked(bool lockState)
        {
        }
    }
}