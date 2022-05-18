using BNG;
using System;
using UnityEngine;
using VRBuilder.BasicInteraction.Properties;
using VRBuilder.Core.Properties;

namespace VRBuilder.VRIF.Properties
{
    [AddComponentMenu("VR Builder/Properties/VRIF/Usable Property (VRIF)")]
    [RequireComponent(typeof(GrabbableUnityEvents))]
    public class UsableProperty : LockableProperty, IUsableProperty
    {
        public bool IsBeingUsed { get; private set; }

        public event EventHandler<EventArgs> UsageStarted;
        public event EventHandler<EventArgs> UsageStopped;

        private Grabbable grabbable;
        private GrabbableUnityEvents grabbableEvents;

        protected Grabbable Grabbable
        {
            get
            {
                if (grabbable == null)
                {
                    grabbable = GetComponent<Grabbable>();
                }

                return grabbable;
            }
        }

        protected GrabbableUnityEvents GrabbableEvents
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

        protected override void OnEnable()
        {
            base.OnEnable();
            GrabbableEvents.onTriggerDown.AddListener(HandleUsed);
            GrabbableEvents.onTriggerUp.AddListener(HandleUnused);

            InternalSetLocked(IsLocked);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            GrabbableEvents.onTriggerDown.RemoveListener(HandleUsed);
            GrabbableEvents.onTriggerUp.RemoveListener(HandleUnused);
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
            throw new NotImplementedException();
        }

        protected override void InternalSetLocked(bool lockState)
        {
            //TODO
        }
    }
}