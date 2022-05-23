using BNG;
using System;
using UnityEngine;
using VRBuilder.BasicInteraction.Properties;
using VRBuilder.Core.Properties;

namespace VRBuilder.VRIF.Properties
{
    /// <summary>
    /// Touchable property for VRIF.
    /// </summary>
    [AddComponentMenu("VR Builder/Properties/VRIF/Touchable Property (VRIF)")]
    [RequireComponent(typeof(GrabbableUnityEvents))]
    public class TouchableProperty : LockableProperty, ITouchableProperty
    {
        /// <inheritdoc/>        
        public event EventHandler<EventArgs> Touched;

        /// <inheritdoc/>        
        public event EventHandler<EventArgs> Untouched;

        private GrabbableUnityEvents grabbableEvents;

        /// <summary>
        /// The event component on this game object.
        /// </summary>
        public GrabbableUnityEvents GrabbableEvents
        {
            get
            {
                if (grabbableEvents == null)
                {
                    grabbableEvents = GetComponent<GrabbableUnityEvents>();
                }

                return grabbableEvents;
            }
        }

        /// <inheritdoc/>        
        public bool IsBeingTouched { get; private set; }

        protected override void OnEnable()
        {
            base.OnEnable();
            GrabbableEvents.onBecomesClosestGrabbable.AddListener(HandleTouch);
            GrabbableEvents.onNoLongerClosestGrabbable.AddListener(HandleUntouch);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            GrabbableEvents.onBecomesClosestGrabbable.AddListener(HandleTouch);
            GrabbableEvents.onNoLongerClosestGrabbable.AddListener(HandleUntouch);
        }

        private void HandleTouch()
        {
            IsBeingTouched = true;
            Touched?.Invoke(this, EventArgs.Empty);
        }

        private void HandleUntouch()
        {
            IsBeingTouched = false;
            Untouched?.Invoke(this, EventArgs.Empty);
        }

        public void FastForwardTouch()
        {
            Touched?.Invoke(this, EventArgs.Empty);
            Untouched?.Invoke(this, EventArgs.Empty);
        }

        protected override void InternalSetLocked(bool lockState)
        {
        }
    }
}
