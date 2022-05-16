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
                    snapZone = new SnapZone();
                }

                return snapZone;
            }
        }

        public bool IsObjectSnapped => SnapZone.HeldItem != null;

        public ISnappableProperty SnappedObject { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public GameObject SnapZoneObject => snapZone.gameObject;

        public event EventHandler<EventArgs> ObjectSnapped;
        public event EventHandler<EventArgs> ObjectUnsnapped;

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }

        public void Configure(IMode mode)
        {            
        }

        protected override void InternalSetLocked(bool lockState)
        {            
        }
    }
}