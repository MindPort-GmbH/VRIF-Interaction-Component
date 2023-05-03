using System;
using UnityEngine;
using UnityEngine.Events;
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
        [Header("Events")]
        [SerializeField]
        private UnityEvent<UsablePropertyEventArgs> useStarted = new UnityEvent<UsablePropertyEventArgs>();

        [SerializeField]
        private UnityEvent<UsablePropertyEventArgs> useEnded = new UnityEvent<UsablePropertyEventArgs>();

        public UnityEvent<UsablePropertyEventArgs> UseStarted => useStarted;

        public UnityEvent<UsablePropertyEventArgs> UseEnded => useEnded;

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
            EmitUsageStarted();
        }

        private void HandleUnused()
        {
            IsBeingUsed = false;
            EmitUsageStopped();
        }

        protected void EmitUsageStarted()
        {
            UsageStarted?.Invoke(this, EventArgs.Empty);
            UseStarted?.Invoke(new UsablePropertyEventArgs());
        }

        protected void EmitUsageStopped()
        {
            UsageStopped?.Invoke(this, EventArgs.Empty);
            UseEnded?.Invoke(new UsablePropertyEventArgs());
        }

        public void FastForwardUse()
        {
            HandleUsed();
            HandleUnused();
        }

        protected override void InternalSetLocked(bool lockState)
        {
        }

        public void ForceSetUsed(bool isUsed)
        {
            if (IsBeingUsed == isUsed)
            {
                return;
            }

            IsBeingUsed = isUsed;
            if (IsBeingUsed)
            {
                EmitUsageStarted();
            }
            else
            {
                EmitUsageStopped();
            }
        }
    }
}