using BNG;
using System;
using UnityEngine;
using VRBuilder.BasicInteraction.Properties;
using VRBuilder.Core.Properties;

namespace VRBuilder.VRIF.Properties
{
    [AddComponentMenu("VR Builder/Properties/VRIF/Grabbable Property (VRIF)")]
    [RequireComponent(typeof(TouchableProperty))]
    public class GrabbableProperty : LockableProperty, IGrabbableProperty
    {
        [SerializeField]
        [Tooltip("If true, the object will count as grabbed only if grabbed with two hands. Note that it may still be possible to grab and move the object with one hand, but the condition will not trigger.")]
        private bool requireTwoHandGrab = false;

        private Grabbable grabbable;
        private GrabbableUnityEvents grabbableEvents;

        public event EventHandler<EventArgs> Grabbed;
        public event EventHandler<EventArgs> Ungrabbed;

        /// <inheritdoc/>
        public bool IsGrabbed => requireTwoHandGrab ? Grabbable.BeingHeldWithTwoHands : Grabbable.BeingHeld;

        /// <summary>
        /// If true, the object will count as grabbed only if grabbed with two hands. Note that it may still be possible to grab and move the object with one hand, but the condition will not trigger.       
        /// </summary>
        public bool RequireTwoHandGrab => requireTwoHandGrab;

        public Grabbable Grabbable
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

        protected override void OnEnable()
        {
            base.OnEnable();
            GrabbableEvents.onGrab.AddListener(HandleGrabbed);
            GrabbableEvents.onRelease.AddListener(HandleReleased);

            InternalSetLocked(IsLocked);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            GrabbableEvents.onGrab.RemoveListener(HandleGrabbed);
            GrabbableEvents.onRelease.RemoveListener(HandleReleased);
        }

        private void HandleReleased()
        {
            Ungrabbed?.Invoke(this, EventArgs.Empty);
        }

        private void HandleGrabbed(Grabber grabber)
        {
            Grabbed?.Invoke(this, EventArgs.Empty);
        }

        protected override void InternalSetLocked(bool lockState)
        {
            if (IsGrabbed)
            {
                Grabbable.DropItem(true, true);
            }

            Grabbable.enabled = !lockState;
        }

        public void FastForwardGrab()
        {
            if(IsGrabbed)
            {
                Grabbable.DropItem(true, true);
            }

            Grabbed?.Invoke(this, EventArgs.Empty);
            Ungrabbed?.Invoke(this, EventArgs.Empty);
        }

        public void FastForwardUngrab()
        {
            if (IsGrabbed)
            {
                Grabbable.DropItem(true, true);
            }

            Grabbed?.Invoke(this, EventArgs.Empty);
            Ungrabbed?.Invoke(this, EventArgs.Empty);
        }
    }
}