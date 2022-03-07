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
            LinearToGamma=1, // rendere in linear final blit to gamma
            GammaToLinear=3, // rendere in gamma final blit to linear
        }

        static string GetKeyword(ColorSpaceUsage colorSpaceUsage)
        {
            if (QualitySettings.activeColorSpace != ColorSpace.Linear)
                return "";

            switch (colorSpaceUsage)
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



        public static void EnableColorSpace(ColorSpaceUsage colorSpaceUsage, CommandBuffer cmd, Material material = null)
        {
            var kw = GetKeyword(colorSpaceUsage);
            if (string.IsNullOrEmpty(kw))
                return;

            cmd?.EnableShaderKeyword(kw);
            material?.EnableKeyword(kw);
        }

        public static void EnableColorSpace(CameraData cameraData,CommandBuffer cmd,Material material = null)
        {
            EnableColorSpace(cameraData.colorSpaceUsage, cmd, material);
        }

        public static void DisableColorSpace(ColorSpaceUsage colorSpaceUsage, CommandBuffer cmd, Material material = null)
        {

            var kw = GetKeyword(colorSpaceUsage);
            if (string.IsNullOrEmpty(kw))
                return;

            cmd?.DisableShaderKeyword(kw);
            material?.DisableKeyword(kw);
        }
        public static void DisableColorSpace(CameraData cameraData, CommandBuffer cmd, Material material = null)
        {
            DisableColorSpace(cameraData.colorSpaceUsage, cmd, material);
        }
    }
}
