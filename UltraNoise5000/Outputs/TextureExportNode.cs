using NoiseUltra.Nodes;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEditor;

namespace NoiseUltra.Output
{
    [NodeTintAttribute(NodeProprieties.NodeTintBlue)]
    public class TextureExportNode : NodeInputOutput
    {

        private const string saveFolderPath = "NoiseTextures";
        private const string filesExtention = ".png";
        
        [SerializeField] [OnValueChanged(nameof(UpdateNoiseName))]
        private string nodeTitle;

        [SerializeField]  private int textureResolution = 1024;
        
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
        void ExportTexture()
        {
            previewImage.SetImageSize(textureResolution);
           
           var currntPath =  AssetDatabase.GetAssetPath(this.graph);
           var folderPath = currntPath.Replace(  this.graph.name + ".asset", "") + saveFolderPath;

           Draw();
           
           byte[] bytes = previewImage.GetTexture().EncodeToPNG();
           string filename = string.Format("{0}/{1}{2}", folderPath, name, filesExtention);

           if (!System.IO.Directory.Exists(folderPath))
               System.IO.Directory.CreateDirectory(folderPath);

           System.IO.File.WriteAllBytes(filename, bytes);
           AssetDatabase.Refresh();
           
           previewImage.ResetImageSize();
        }
        
        
        
    }


}

