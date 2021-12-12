using NoiseUltra.Nodes;
using Sirenix.OdinInspector;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace NoiseUltra.Output
{
    [NodeTint(NodeColor.EXPORT_TEXTURE)]
    public class TextureExportNode : NodeInputOutput
    {
        private const string SAVE_FOLDER_PATH = "NoiseTextures";
        private const string FILES_EXTENSION = ".png";
        private const string FILE_ASSET_EXTENSION = ".asset";

        [SerializeField]
        [OnValueChanged(nameof(UpdateNoiseName))]
        private string nodeTitle;

        [SerializeField]
        private int textureResolution = 1024;

        protected override void Init()
        {
            base.Init();
            nodeTitle = name;
        }

        private void UpdateNoiseName()
        {
            name = nodeTitle;
        }

        [Button]
        private void ExportTexture()
        {
        #if UNITY_EDITOR
                    previewImage.SetImageSize(textureResolution);
                    string currentPath = AssetDatabase.GetAssetPath(graph);
                    string folderPath = currentPath.Replace(graph.name + FILE_ASSET_EXTENSION, string.Empty) + SAVE_FOLDER_PATH;

                    DrawSync();

                    byte[] bytes = previewImage.GetTexture().EncodeToPNG();

                    string filename = string.Format("{0}/{1}{2}", folderPath, name, FILES_EXTENSION);

                    if (!System.IO.Directory.Exists(folderPath))
                    {
                        System.IO.Directory.CreateDirectory(folderPath);
                    }

                    System.IO.File.WriteAllBytes(filename, bytes);
                    AssetDatabase.Refresh();
                    previewImage.ResetImageSize();
        #endif
        }
    }
}

