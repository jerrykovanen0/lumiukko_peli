#ifndef SHADERGRAPH_PREVIEW
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
#endif

void ToonShading_float(
    float3 Normal,
    float ToonRampSmoothness,
    float3 ClipSpacePos,
    float3 WorldPos,
    float4 ToonRampTinting,
    float ToonRampOffset,
    out float3 ToonRampOutput,
    out float3 Direction)
{
#ifdef SHADERGRAPH_PREVIEW
    ToonRampOutput = float3(0.5, 0.5, 0);
    Direction = float3(0.5, 0.5, 0);
#else

#if SHADOWS_SCREEN
    float4 shadowCoord = ComputeScreenPos(ClipSpacePos);
#else
    float4 shadowCoord = TransformWorldToShadowCoord(WorldPos);
#endif 

#if _MAIN_LIGHT_SHADOWS_CASCADE || _MAIN_LIGHT_SHADOWS
    Light light = GetMainLight(shadowCoord);
#else
    Light light = GetMainLight();
#endif

    float d = dot(Normal, light.direction) * 0.5 + 0.5;

    float toonRamp = smoothstep(ToonRampOffset,
        ToonRampOffset + ToonRampSmoothness,
        d);

    toonRamp *= light.shadowAttenuation;

    ToonRampOutput = light.color * toonRamp + ToonRampTinting.rgb;
    Direction = light.direction;

#endif
}

