using BNG;
using UnityEditor;
using UnityEngine;
using VRBuilder.VRIF.Properties;

namespace VRBuilder.Editor.VRIF.UI
{
    [CustomEditor(typeof(GrabbableProperty))]
    public class GrabbablePropertyEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GrabbableProperty property = target as GrabbableProperty;
            Grabbable grabbable = property.GetComponent<Grabbable>();

            if(property.RequireTwoHandGrab && grabbable.SecondaryGrabBehavior != OtherGrabBehavior.DualGrab)
            {
                EditorGUILayout.HelpBox("The grabbable's secondary grab behavior is not set to dual grab. It will not be possible to grab this object with two hands and therefore fulfill any conditions depending on it.", MessageType.Warning);

                if(GUILayout.Button("Set to dual grab"))
                {
                    grabbable.SecondaryGrabBehavior = OtherGrabBehavior.DualGrab;
                }
            }

            if(property.GetComponent<Rigidbody>() == null)
            {
                EditorGUILayout.HelpBox("No rigidbody is present on this grabbable. A rigidbody is necessary if the object is not meant to be stationary, that is if the Grab Physics setting on the grabbable is set to anything other than None.", MessageType.Warning);
            }
        }
    }
}