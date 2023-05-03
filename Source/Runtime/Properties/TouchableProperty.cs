using BNG;
using System;
using UnityEngine;
using UnityEngine.Events;
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
        [Header("Events")]
        [SerializeField]
        private UnityEvent<TouchablePropertyEventArgs> touchStarted = new UnityEvent<TouchablePropertyEventArgs>();

        [SerializeField]
        private UnityEvent<TouchablePropertyEventArgs> touchEnded = new UnityEvent<TouchablePropertyEventArgs>();

        /// <inheritdoc/>        
        public event EventHandler<EventArgs> Touched;

        /// <inheritdoc/>        
        public event EventHandler<EventArgs> Untouched;

        /// <inheritdoc/>        
        public UnityEvent<TouchablePropertyEventArgs> TouchStarted => touchStarted;

        /// <inheritdoc/>        
        public UnityEvent<TouchablePropertyEventArgs> TouchEnded => touchEnded;

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
            EmitTouched();
        }

        private void HandleUntouch()
        {
            IsBeingTouched = false;
            EmitUntouched();
        }

        protected void EmitTouched()
        {
            Touched?.Invoke(this, EventArgs.Empty);
            TouchStarted?.Invoke(new TouchablePropertyEventArgs());
        }

        protected void EmitUntouched()
        {
            Untouched?.Invoke(this, EventArgs.Empty);
            TouchEnded?.Invoke(new TouchablePropertyEventArgs());
        }

        protected override void InternalSetLocked(bool lockState)
        {
        }

        public void ForceSetTouched(bool isTouched)
        {
            if (IsBeingTouched == isTouched)
            {
                return;
            }

            IsBeingTouched = isTouched;
            if (IsBeingTouched)
            {
                EmitTouched();
            }
            else
            {
                EmitUntouched();
            }
        }

        public void FastForwardTouch()
        {
            EmitTouched();
            EmitUntouched();
        }
    }
}
