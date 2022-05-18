using BNG;
using System;
using UnityEngine;
using VRBuilder.BasicInteraction.Properties;
using VRBuilder.Core.Configuration.Modes;
using VRBuilder.Core.Properties;

namespace VRBuilder.VRIF.Properties
{
    [AddComponentMenu("VR Builder/Properties/VRIF/Snap Zone Property (VRIF)")]
    [RequireComponent(typeof(SnapZone))]
    public class SnapZoneProperty : LockableProperty, ISnapZoneProperty
    {
        private SnapZone snapZone;
        protected SnapZone SnapZone
        {
            get
            {
                if(snapZone == null)
                {
                    snapZone = GetComponent<SnapZone>();
                }

                return snapZone;
            }
        }

        public bool IsObjectSnapped => SnapZone.HeldItem != null;

        public ISnappableProperty SnappedObject { get; set; }


        public GameObject SnapZoneObject => snapZone.gameObject;

        public event EventHandler<EventArgs> ObjectSnapped;
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
                Debug.LogWarning($"SnapZone '{SceneObject.UniqueName}' received snap from object '{grabbable.gameObject.name}' without {typeof(SnappableProperty).Name}");
            }
            else
            {
                ObjectSnapped?.Invoke(this, EventArgs.Empty);
            }
        }

        private void HandleUnsnapped(Grabbable grabbable)
        {
            if (SnappedObject != null)
            {
                SnappedObject = null;
                ObjectUnsnapped?.Invoke(this, EventArgs.Empty);
            }
        }

        public void Configure(IMode mode)
        {            
        }

        protected override void InternalSetLocked(bool lockState)
        {
            SnapZone.enabled = lockState == false || (SnappedObject != null);
        }
    }
}