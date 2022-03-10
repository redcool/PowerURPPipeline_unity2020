using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityEngine.Rendering.Universal
{
    public partial class PostProcessData : ScriptableObject
    {
        public partial class ShaderResources
        {

            [Reload("Shaders/PostProcessing/EdgeAdaptiveSpatialUpsampling.compute")]
            public ComputeShader easuCS;
            [Reload("Shaders/PostProcessing/RobustContrastAdaptiveSharpen.compute")]
            public ComputeShader rcasCS;
        }
    }
}
