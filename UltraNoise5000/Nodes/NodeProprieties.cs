namespace NoiseUltra.Nodes
{
    public static class NodeColor
    {
        public const string YELLOW = "#86853C";
        
        public const string MODIFIER = "#8C7569";
        public const string OPERATION = "#8C8C69";
        public const string UTILITY = "#737373";
        
        public const string GENERATOR_NOISE = "#529945";
        public const string GENERATOR_OTHER = "#45996F";
        
        public const string EXPORT_NODE = "#4273A6";
        public const string EXPORT_TEXTURE = "#4B97A6";
        
        public const string PAINTLAYER = "#927590";
        public const string PAINTEXPORT = "#BA5381";
    }

    public static class NodeLabels
    {
        public const string MinMax = "MinMax";
        public const string Max = "Max";
        public const string Min = "Min";
        public const string Left = "Left";
        public const string Right = "Right";
        public const string Separator = "/";
        
        
        public const string MinMaxLEFTGroup = MinMax + Separator + Left;
        public const string MinMaxLEFTGroupMIN = MinMaxLEFTGroup + Separator + Max;
        
        public const string MinMaxRIGHTGroup = MinMax + Separator + Right;
        public const string MinMaxRIGHTGroupMax = MinMaxRIGHTGroup + Separator + Max;
    }

    /// <summary>
    ///     Global properties for nodes.
    /// </summary>
    public static class NodeProprieties
    {
        public const int DEFAULT_GLOBAL_ZOOM = 200;
        public const int DEFAULT_PREVIEW_SIZE = 256;
        public const int DEFAULT_TEXTURE_SIZE = 128;
        public const int NODE_WIDTH_PIXELS = 286;

        public const float MIN_VALUE = 0;
        public const float MAX_VALUE = 1;
        public const float INVALID_VALUE = -1;
    }
}