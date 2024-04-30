using BNG;
using System;
using UnityEngine;
using UnityEngine.Events;
using VRBuilder.BasicInteraction.Properties;
using VRBuilder.Core.Configuration.Modes;
using VRBuilder.Core.Properties;

namespace VRBuilder.VRIF.Properties
{
    /// <summary>
    /// Snap zone property for VRIF snap zones.
    /// </summary>
    [AddComponentMenu("VR Builder/Properties/VRIF/Snap Zone Property (VRIF)")]
    [RequireComponent(typeof(SnapZone))]
    public class SnapZoneProperty : LockableProperty, ISnapZoneProperty
    {
        [Header("Events")]
        [SerializeField]
        private UnityEvent<SnapZonePropertyEventArgs> objectAttached = new UnityEvent<SnapZonePropertyEventArgs>();

        [SerializeField]
        private UnityEvent<SnapZonePropertyEventArgs> objectDetached = new UnityEvent<SnapZonePropertyEventArgs>();

        private SnapZone snapZone;

        /// <summary>
        /// The snap zone on this game object.
        /// </summary>
        protected SnapZone SnapZone
        {
            get
            {
                if (snapZone == null)
                {
                    snapZone = GetComponent<SnapZone>();
                }

                return snapZone;
            }
        }

        /// <inheritdoc/>        
        public bool IsObjectSnapped => SnapZone.HeldItem != null;

        /// <inheritdoc/>        
        public ISnappableProperty SnappedObject { get; set; }

        /// <inheritdoc/>        
        public GameObject SnapZoneObject => snapZone.gameObject;

        /// <inheritdoc/>        
        public UnityEvent<SnapZonePropertyEventArgs> ObjectAttached => objectAttached;

        /// <inheritdoc/>        
        public UnityEvent<SnapZonePropertyEventArgs> ObjectDetached => objectDetached;

        /// <inheritdoc/>        
        public event EventHandler<EventArgs> ObjectSnapped;

        /// <inheritdoc/>        
        public event EventHandler<EventArgs> ObjectUnsnapped;

        protected override void OnEnable()
        {
            base.OnEnable();
            SnapZone.OnSnapEvent.AddListener(HandleSnapped);
            SnapZone.OnDetachEvent.AddListener(HandleUnsnapped);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            SnapZone.OnSnapEvent.RemoveListener(HandleSnapped);
            SnapZone.OnDetachEvent.RemoveListener(HandleUnsnapped);
        }

        private void HandleSnapped(Grabbable grabbable)
        {
            SnappedObject = grabbable.GetComponent<SnappableProperty>();
            if (SnappedObject == null)
            {
                Debug.LogWarning($"SnapZone '{SceneObject.GameObject.name}' received snap from object '{grabbable.gameObject.name}' without {typeof(SnappableProperty).Name}");
            }
            else
            {
                EmitSnapped();
            }
        }

        private void HandleUnsnapped(Grabbable grabbable)
        {
            if (SnappedObject != null)
            {
                SnappedObject = null;
                EmitUnsnapped();
            }
        }

        /// <summary>
        /// Invokes the <see cref="EmitSnapped"/> event.
        /// </summary>
        protected void EmitSnapped()
        {
            ObjectSnapped?.Invoke(this, EventArgs.Empty);
            ObjectAttached?.Invoke(new SnapZonePropertyEventArgs());
        }

        /// <summary>
        /// Invokes the <see cref="EmitUnsnapped"/> event.
        /// </summary>
        protected void EmitUnsnapped()
        {
            ObjectUnsnapped?.Invoke(this, EventArgs.Empty);
            ObjectDetached?.Invoke(new SnapZonePropertyEventArgs());
        }

        public void Configure(IMode mode)
        {
        }

        protected override void InternalSetLocked(bool lockState)
        {
        }
    }
}