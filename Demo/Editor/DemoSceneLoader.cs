using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace VRBuilder.Editor.VRIF.DemoScene
{
    /// <summary>
    /// Menu item for loading the demo scene after checking the process file is in the StreamingAssets folder.
    /// </summary>
    public static class DemoSceneLoader
    {
        private const string demoScenePath = "Assets/MindPort/VR Builder/Add-ons/VRIF Interaction Component/Demo/Scenes/VR Builder Demo - VRIF Integration.unity";
        private const string demoProcessOrigin = "Assets/MindPort/VR Builder/Add-ons/VRIF Interaction Component/Demo/StreamingAssets/Processes/Demo - VRIF Integration/Demo - VRIF Integration.json";
        private const string demoProcessDirectory = "Assets/StreamingAssets/Processes/Demo - VRIF Integration";
        private const string demoProcessDestination = "Assets/StreamingAssets/Processes/Demo - VRIF Integration/Demo - VRIF Integration.json";

        [MenuItem("Tools/VR Builder/Demo Scenes/VRIF Integration", false, 64)]
        public static void LoadDemoScene()
        {
#if VR_BUILDER_XR_INTERACTION
            if (EditorUtility.DisplayDialog("XR Interaction Component Found", "It looks like the built-in XR Interaction Component is enabled. It might interfere with this integration, and it's recommended to disable it in order to have a single interaction component in a project. You can disable it in Project Settings > VR Builder > Settings.", "Ok")) 
            {
                return;
            }
#endif
            if (File.Exists(demoProcessDestination) == false)
            {
                if(EditorUtility.DisplayDialog("Demo Scene Setup", "Before opening the demo scene, the sample process needs to be copied in Assets/StreamingAssets. Press Ok to proceed.", "Ok"))
                {
                    Directory.CreateDirectory(demoProcessDirectory);
                    FileUtil.CopyFileOrDirectory(demoProcessOrigin, demoProcessDestination);
                }
            }

            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            EditorSceneManager.OpenScene(demoScenePath);
        }
    }
}