using BNG;
using System;
using UnityEngine;
using VRBuilder.BasicInteraction.Properties;
using VRBuilder.Core.Properties;

namespace VRBuilder.VRIF.Properties
{
    /// <summary>
    /// Usable property for VRIF.
    /// </summary>
    [AddComponentMenu("VR Builder/Properties/VRIF/Usable Property (VRIF)")]
    [RequireComponent(typeof(GrabbableProperty))]
    public class UsableProperty : LockableProperty, IUsableProperty
    {        
        /// <inheritdoc/>        
        public bool IsBeingUsed { get; private set; }

        /// <inheritdoc/>        
        public event EventHandler<EventArgs> UsageStarted;

        /// <inheritdoc/>        
        public event EventHandler<EventArgs> UsageStopped;
       
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

        protected override void OnEnable()
        {
            base.OnEnable();
            GrabbableProperty.GrabbableEvents.onTriggerDown.AddListener(HandleUsed);
            GrabbableProperty.GrabbableEvents.onTriggerUp.AddListener(HandleUnused);

            InternalSetLocked(IsLocked);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            GrabbableProperty.GrabbableEvents.onTriggerDown.RemoveListener(HandleUsed);
            GrabbableProperty.GrabbableEvents.onTriggerUp.RemoveListener(HandleUnused);
        }

        private void HandleUsed()
        {
            IsBeingUsed = true;
            UsageStarted?.Invoke(this, EventArgs.Empty);
        }

        private void HandleUnused()
        {
            IsBeingUsed = false;
            UsageStopped?.Invoke(this, EventArgs.Empty);
        }

        public void FastForwardUse()
        {
            HandleUsed();
            HandleUnused();
        }

        protected override void InternalSetLocked(bool lockState)
        {
        }
    }
}