using BNG;
using System;
using UnityEngine;
using VRBuilder.Core.Properties;

namespace VRBuilder.VRIF.Properties
{
    [AddComponentMenu("VR Builder/Properties/VRIF/Lever Property (VRIF)")]
    [RequireComponent(typeof(Lever))] 
    public class LeverProperty : LockableProperty, ILinearControlProperty
    {
        private Lever lever;

        public Lever Lever
        {
            get
            {
                if (lever == null)
                {
                    lever = GetComponent<Lever>();
                }

                return lever;
            }
        }

        public event EventHandler<EventArgs> MinPosition;
        public event EventHandler<EventArgs> MaxPosition;
        public event EventHandler<EventArgs> ChangedPosition;

        public float Position => Lever.LeverPercentage / 100;

        protected override void OnEnable()
        {
            base.OnEnable();
            Lever.onLeverChange.AddListener(HandleLeverChanged);
            Lever.onLeverDown.AddListener(HandleLeverDown);
            Lever.onLeverUp.AddListener(HandleLeverUp);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            Lever.onLeverChange.RemoveListener(HandleLeverChanged);
            Lever.onLeverDown.RemoveListener(HandleLeverDown);
            Lever.onLeverUp.RemoveListener(HandleLeverUp);
        }

        private void HandleLeverUp()
        {
            MaxPosition?.Invoke(this, EventArgs.Empty);
        }

        private void HandleLeverDown()
        {
            MinPosition?.Invoke(this, EventArgs.Empty); 
        }

        private void HandleLeverChanged(float percentage)
        {
            ChangedPosition?.Invoke(this, EventArgs.Empty);
        }

        public void FastForwardPosition(float position)
        {
            Lever.SetLeverAngle((Lever.MaximumXRotation - Lever.MinimumXRotation) * position + Lever.MinimumXRotation);
        }

        protected override void InternalSetLocked(bool lockState)
        {
            //throw new System.NotImplementedException();            
        }
    }
}