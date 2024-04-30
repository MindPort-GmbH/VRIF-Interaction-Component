using BNG;
using System;
using UnityEngine;
using UnityEngine.Events;
using VRBuilder.BasicInteraction.Properties;
using VRBuilder.Core.Properties;

namespace VRBuilder.VRIF.Properties
{
    /// <summary>
    /// Snappable property for snapping VRIF grabbables in VRIF snap zones.
    /// </summary>
    [AddComponentMenu("VR Builder/Properties/VRIF/Snappable Property (VRIF)")]
    [RequireComponent(typeof(GrabbableProperty))]
    public class SnappableProperty : LockableProperty, ISnappableProperty
    {
        [Header("Events")]
        [SerializeField]
        private UnityEvent<SnappablePropertyEventArgs> attachedToSnapZone = new UnityEvent<SnappablePropertyEventArgs>();

        [SerializeField]
        private UnityEvent<SnappablePropertyEventArgs> detachedFromSnapZone = new UnityEvent<SnappablePropertyEventArgs>();

        private GrabbableProperty grabbableProperty;

        /// <summary>
        /// The grabbable property on this game object.
        /// </summary>
        protected GrabbableProperty GrabbableProperty
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

        /// <inheritdoc/>        
        public bool LockObjectOnSnap { get; set; }

        /// <inheritdoc/>        
        public ISnapZoneProperty SnappedZone { get; set; }

        /// <inheritdoc/>        
        public bool IsSnapped => GrabbableProperty.transform.parent != null && GrabbableProperty.transform.parent.GetComponent<SnapZone>() != null;

        public UnityEvent<SnappablePropertyEventArgs> AttachedToSnapZone => attachedToSnapZone;

        public UnityEvent<SnappablePropertyEventArgs> DetachedFromSnapZone => detachedFromSnapZone;

        /// <inheritdoc/>        
        public event EventHandler<EventArgs> Snapped;

        /// <inheritdoc/>        
        public event EventHandler<EventArgs> Unsnapped;

        protected override void OnEnable()
        {
            base.OnEnable();
            GrabbableProperty.Grabbable.CanBeSnappedToSnapZone = true;
            GrabbableProperty.GrabbableEvents.onSnapZoneEnter.AddListener(HandleSnapped);
            GrabbableProperty.GrabbableEvents.onSnapZoneExit.AddListener(HandleUnsnapped);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            GrabbableProperty.Grabbable.CanBeSnappedToSnapZone = false;
            GrabbableProperty.GrabbableEvents.onSnapZoneEnter.RemoveListener(HandleSnapped);
            GrabbableProperty.GrabbableEvents.onSnapZoneExit.RemoveListener(HandleUnsnapped);
        }

        private void HandleUnsnapped()
        {
            EmitUnsnapped(SnappedZone);
        }

        private void HandleSnapped()
        {
            Transform parent = transform.parent;

            if (parent == null)
            {
                Debug.LogError($"Object {SceneObject.GameObject.name} should be snapped but is not child object.");
                return;
            }

            SnapZoneProperty snapZone = parent.GetComponent<SnapZoneProperty>();

            if (snapZone == null)
            {
                Debug.LogWarning($"Object {SceneObject.GameObject.name} has been snapped to a snap zone without a {typeof(SnapZoneProperty).Name}. The VR Builder process will not see the object as snapped.");
                return;
            }

            SnappedZone = snapZone;

            if (LockObjectOnSnap)
            {
                SceneObject.SetLocked(true);
            }

            EmitSnapped(SnappedZone);
        }

        /// <summary>
        /// Invokes the <see cref="EmitSnapped"/> event.
        /// </summary>
        protected void EmitSnapped(ISnapZoneProperty snapZone)
        {
            AttachedToSnapZone?.Invoke(new SnappablePropertyEventArgs(this, snapZone));
            Snapped?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Invokes the <see cref="EmitUnsnapped"/> event.
        /// </summary>
        protected void EmitUnsnapped(ISnapZoneProperty snapZone)
        {
            DetachedFromSnapZone?.Invoke(new SnappablePropertyEventArgs(this, snapZone));
            Unsnapped?.Invoke(this, EventArgs.Empty);
        }

        public void FastForwardSnapInto(ISnapZoneProperty snapZone)
        {
            SnapZone snapZoneComponent = snapZone.SnapZoneObject.GetComponent<SnapZone>();

            snapZoneComponent?.GrabGrabbable(grabbableProperty.Grabbable);
        }

        protected override void InternalSetLocked(bool lockState)
        {
        }
    }
}