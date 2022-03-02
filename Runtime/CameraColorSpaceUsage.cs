using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityEngine.Rendering.Universal
{

    public static class CameraColorSpaceUsage
    {
        public enum ColorSpaceUsage
        {
            PipelineColorSpace=0,
            LinearToGamma=1, // rendered blit to gamma
            GammaToLinear=3, // rendered blit to linear
        }

        static string GetKeyword(CameraData camData)
        {
            switch (camData.colorSpaceUsage)
            {
                case ColorSpaceUsage.LinearToGamma: return ShaderKeywordStrings.LinearToSRGBConversion; 
                case ColorSpaceUsage.GammaToLinear: return ShaderKeywordStrings.SRGBToLinearConversion; 
            }
            return "";
        }

        public static void DisableKeywords(CommandBuffer cmd)
        {
            cmd?.DisableShaderKeyword(ShaderKeywordStrings.LinearToSRGBConversion);
            cmd?.DisableShaderKeyword(ShaderKeywordStrings.SRGBToLinearConversion);
        }

        public static void EnableColorSpace(CameraData camData, CommandBuffer cmd, Material material = null)
        {
            DisableKeywords(cmd);

            var kw = GetKeyword(camData);
            if (string.IsNullOrEmpty(kw))
                return;

            cmd?.EnableShaderKeyword(kw);
            material?.EnableKeyword(kw);

        }
        public static void DisableColorSpace(CameraData camData, CommandBuffer cmd, Material material = null)
        {
            DisableKeywords(cmd);

            var kw = GetKeyword(camData);
            if (string.IsNullOrEmpty(kw))
                return;

            cmd?.DisableShaderKeyword(kw);
            material?.DisableKeyword(kw);
        }
    }
}
