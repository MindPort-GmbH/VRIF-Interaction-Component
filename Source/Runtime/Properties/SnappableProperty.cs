using BNG;
using System;
using UnityEngine;
using VRBuilder.BasicInteraction.Properties;
using VRBuilder.Core.Properties;

namespace VRBuilder.VRIF.Properties
{
    /// <summary>
    /// Snappable property for VRIF grabbables and snap zones.
    /// </summary>
    [AddComponentMenu("VR Builder/Properties/VRIF/Snappable Property (VRIF)")]
    [RequireComponent(typeof(GrabbableProperty))]
    public class SnappableProperty : LockableProperty, ISnappableProperty
    {
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
            Unsnapped?.Invoke(this, EventArgs.Empty);
        }

        private void HandleSnapped()
        {
            // TODO support lock on snap

            Transform parent = transform.parent;
            
            if(parent == null)
            {
                Debug.LogError($"Object {SceneObject.UniqueName} should be snapped but is not child object.");
                return;
            }

            SnapZoneProperty snapZone = parent.GetComponent<SnapZoneProperty>();

            if (snapZone == null)
            {
                Debug.LogError($"Object {SceneObject.UniqueName} should be snapped but is not child of a {typeof(SnapZoneProperty).Name}.");
                return;
            }

            SnappedZone = snapZone;
            Snapped?.Invoke(this, EventArgs.Empty);
        }

        public void FastForwardSnapInto(ISnapZoneProperty snapZone)
        {
            throw new NotImplementedException();
        }

        protected override void InternalSetLocked(bool lockState)
        {
            GrabbableProperty.Grabbable.CanBeSnappedToSnapZone = enabled && !lockState;
        }
    }
}