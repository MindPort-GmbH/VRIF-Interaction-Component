using BNG;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using VRBuilder.BasicInteraction.Properties;
using VRBuilder.Core.Properties;

namespace VRBuilder.VRIF.Properties
{
    /// <summary>
    /// Grabbable property for VRIF.
    /// </summary>
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
        public UnityEvent<GrabbablePropertyEventArgs> GrabStarted => grabStarted;
        public UnityEvent<GrabbablePropertyEventArgs> GrabEnded => grabEnded;

        [Header("Events")]
        [SerializeField]
        private UnityEvent<GrabbablePropertyEventArgs> grabStarted = new UnityEvent<GrabbablePropertyEventArgs>();

        [SerializeField]
        private UnityEvent<GrabbablePropertyEventArgs> grabEnded = new UnityEvent<GrabbablePropertyEventArgs>();

        /// <inheritdoc/>
        public bool IsGrabbed { get; protected set; }

        /// <summary>
        /// If true, the object will count as grabbed only if grabbed with two hands. Note that it may still be possible to grab and move the object with one hand, but the condition will not trigger.       
        /// </summary>
        public bool RequireTwoHandGrab => requireTwoHandGrab;

        /// <summary>
        /// The Grabbable component on this game object.
        /// </summary>
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

        /// <summary>
        /// The events component on this game object.
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

        private GrabbableUnityEvents secondaryGrabbableEvents;

        /// <summary>
        /// Events component for secondary grabbable.
        /// </summary>
        public GrabbableUnityEvents SecondaryGrabbableEvents
        {
            get
            {
                if(secondaryGrabbableEvents == null && Grabbable.SecondaryGrabbable != null) 
                {
                    secondaryGrabbableEvents= Grabbable.SecondaryGrabbable.GetComponent<GrabbableUnityEvents>();

                    if(secondaryGrabbableEvents == null)
                    {
                        secondaryGrabbableEvents = Grabbable.SecondaryGrabbable.AddComponent<GrabbableUnityEvents>();
                    }
                }

                return secondaryGrabbableEvents;
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            GrabbableEvents.onGrab.AddListener(HandleGrabbed);
            GrabbableEvents.onRelease.AddListener(HandleReleased);

            SecondaryGrabbableEvents?.onGrab.AddListener(HandleGrabbed);
            SecondaryGrabbableEvents?.onRelease.AddListener(HandleReleased);

            InternalSetLocked(IsLocked);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            GrabbableEvents.onGrab.RemoveListener(HandleGrabbed);
            GrabbableEvents.onRelease.RemoveListener(HandleReleased);

            SecondaryGrabbableEvents?.onGrab.RemoveListener(HandleGrabbed);
            SecondaryGrabbableEvents?.onRelease.RemoveListener(HandleReleased);
        }

        private void HandleReleased()
        {
            IsGrabbed = false;
            EmitUngrabbed();
        }

        private void HandleGrabbed(Grabber grabber)
        {
           //IsGrabbed = RequireTwoHandGrab ? Grabbable.BeingHeld && Grabbable.SecondaryGrabbable != null && Grabbable.SecondaryGrabbable.BeingHeld : Grabbable.BeingHeld;
            IsGrabbed = RequireTwoHandGrab ? Grabbable.BeingHeldWithTwoHands : Grabbable.BeingHeld;
            EmitGrabbed();            
        }

        protected override void InternalSetLocked(bool lockState)
        {            
        }

        public void FastForwardGrab()
        {
            if(IsGrabbed)
            {
                Grabbable.DropItem(true, true);
            }

            EmitGrabbed();
            EmitUngrabbed();
        }

        public void FastForwardUngrab()
        {
            if (IsGrabbed)
            {
                Grabbable.DropItem(true, true);
            }

            EmitGrabbed();
            EmitUngrabbed();
        }

        protected void EmitGrabbed()
        {
            Grabbed?.Invoke(this, EventArgs.Empty);
            GrabStarted?.Invoke(new GrabbablePropertyEventArgs());
        }

        protected void EmitUngrabbed()
        {
            Ungrabbed?.Invoke(this, EventArgs.Empty);
            GrabEnded?.Invoke(new GrabbablePropertyEventArgs());
        }

        public void ForceSetGrabbed(bool isGrabbed)
        {
            if (IsGrabbed == isGrabbed)
            {
                return;
            }

            IsGrabbed = isGrabbed;
            if (isGrabbed)
            {
                EmitGrabbed();
            }
            else
            {
                EmitUngrabbed();
            }
        }
    }
}