using System;
using NoiseUltra.Nodes;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEditor;

namespace NoiseUltra.Output
{
    [NodeTint(NodeColor.Blue)]
    public class TextureExportNode : NodeInputOutput
    {
        private const string SaveFolderPath = "NoiseTextures";
        private const string FilesExtension = ".png";
        private const string FileAssetExtension = ".asset";

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
            previewImage.SetImageSize(textureResolution);
            var currentPath = AssetDatabase.GetAssetPath(graph);
            var folderPath = currentPath.Replace(graph.name + FileAssetExtension, string.Empty) + SaveFolderPath;

            Draw();

            byte[] bytes = previewImage.GetTexture().EncodeToPNG();
            string filename = string.Format("{0}/{1}{2}", folderPath, name, FilesExtension);

            if (!System.IO.Directory.Exists(folderPath))
                System.IO.Directory.CreateDirectory(folderPath);

            System.IO.File.WriteAllBytes(filename, bytes);
            AssetDatabase.Refresh();

            previewImage.ResetImageSize();
        }
    }
}

