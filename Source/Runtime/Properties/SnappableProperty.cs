using System;
using UnityEngine;
using VRBuilder.BasicInteraction.Properties;
using VRBuilder.Core.Properties;

namespace VRBuilder.VRIF.Properties
{
    [AddComponentMenu("VR Builder/Properties/VRIF/Snappable Property (VRIF)")]
    [RequireComponent(typeof(GrabbableProperty))]
    public class SnappableProperty : LockableProperty, ISnappableProperty
    {
        private GrabbableProperty grabbableProperty;

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

        public bool LockObjectOnSnap => throw new NotImplementedException();

        public ISnapZoneProperty SnappedZone { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool IsSnapped => throw new NotImplementedException();

        public event EventHandler<EventArgs> Snapped;
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
            Debug.Log("Unsnapped");
        }

        private void HandleSnapped()
        {
            Debug.Log("Snapped");
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