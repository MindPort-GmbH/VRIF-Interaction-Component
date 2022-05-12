using BNG;
using System;
using UnityEngine;
using VRBuilder.BasicInteraction.Properties;
using VRBuilder.Core.Properties;

namespace VRBuilder.VRIF.Properties
{
    [AddComponentMenu("VR Builder/Properties/Interhaptics/Grabbable Property (VRIF)")]
    [RequireComponent(typeof(GrabbableUnityEvents))]
    // Since Grabbable does not work without a rigid body, but does not strictly require it, we add the requirement here.
    [RequireComponent(typeof(Rigidbody))]
    public class GrabbableProperty : LockableProperty, IGrabbableProperty
    {
        [SerializeField]
        private bool requireTwoHandGrab = false;

        private Grabbable grabbable;
        private GrabbableUnityEvents grabbableEvents;

        public event EventHandler<EventArgs> Grabbed;
        public event EventHandler<EventArgs> Ungrabbed;

        public bool IsGrabbed => requireTwoHandGrab ? Grabbable.BeingHeldWithTwoHands : Grabbable.BeingHeld;

        public bool RequireTwoHandGrab => requireTwoHandGrab;

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
                if (lockState)
                {
                    Grabbable.Release(Vector3.zero, Vector3.zero);
                }
            }

            if (lockState)
            {
                Grabbable.LockGrabbableWithRotation();
            }
            else
            {
                Grabbable.UnlockGrabbable();
            }
        }

        public void FastForwardGrab()
        {
            throw new NotImplementedException();
        }

        public void FastForwardUngrab()
        {
            throw new NotImplementedException();
        }
    }
}