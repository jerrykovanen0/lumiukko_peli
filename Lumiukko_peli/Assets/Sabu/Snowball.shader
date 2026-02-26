Shader "Custom/Snow Interactive"
{
    Properties
    {
        _Noise("Snow Noise", 2D) = "gray" {}
        _NoiseScale("Noise Scale", Range(0,2)) = 0.1
        _NoiseWeight("Noise Weight", Range(0,2)) = 0.1

        [Header(Tessellation)]
        _MaxTessDistance("Max Tessellation Distance", Range(10,100)) = 50
        _Tess("Tessellation", Range(1,500)) = 20

        [Header(Snow)]
        [HDR]_Color("Snow Color", Color) = (0.8,0.8,0.8,1)
        _MainTex("Snow Texture", 2D) = "white" {}
        _SnowHeight("Snow Height", Range(0,2)) = 0.3
        _SnowTextureOpacity("Snow Texture Opacity", Range(0,1)) = 0.3
        _SnowTextureScale("Snow Texture Scale", Range(0,2)) = 0.3
        _Normal("Snow Normal", 2D) = "bump" {}
        _SnowNormalStrength("Snow Normal Strength", Range(0,1)) = 0.3
        [HDR]_ShadowColor("Shadow Color", Color) = (0.5,0.5,0.7,1)
	_ToonThreshold("Toon Shadow Threshold", Range(0,1)) = 0.5
	_ToonSoftness("Toon Shadow Softness", Range(0.001,0.5)) = 0.1
_LightBoostStrength("Light Boost Strength", Range(0,3)) = 1
_LightBoostThreshold("Light Boost Threshold", Range(0,1)) = 0.75
_LightBoostSoftness("Light Boost Softness", Range(0.01,0.5)) = 0.15
[HDR]_LightBoostColor("Light Boost Color", Color) = (1,1,1,1)


        [Header(Sparkles)]
        _SparkleScale("Sparkle Scale", Range(0,10)) = 10
        _SparkCutoff("Sparkle Cutoff", Range(0,1)) = 0.8
        _SparkleNoise("Sparkle Noise", 2D) = "gray" {}

        [Header(Rim)]
        _RimPower("Rim Power", Range(0,20)) = 20
        [HDR]_RimColor("Rim Color", Color) = (1,1,1,1)
    }

    HLSLINCLUDE
    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Shadows.hlsl"
    #include "SnowTessellationOff.hlsl"

    #pragma vertex TessellationVertexProgram
    #pragma hull hull
    #pragma domain domain
    #pragma require tessellation tessHW
    #pragma target 4.5
    ENDHLSL

    SubShader
    {
        Tags{ "RenderPipeline"="UniversalPipeline" "RenderType"="Opaque" }

        Pass
        {
            Name "Forward"
            Tags{ "LightMode"="UniversalForward" }

            HLSLPROGRAM
            #pragma fragment frag
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE
            #pragma multi_compile _ _ADDITIONAL_LIGHTS
            #pragma multi_compile_fog

            sampler2D _MainTex;
            sampler2D _Normal;
            sampler2D _SparkleNoise;

            float4 _Color;
            float4 _RimColor;
            float4 _ShadowColor;
            float _RimPower;
            float _SparkleScale;
            float _SparkCutoff;
            float _SnowTextureOpacity;
            float _SnowTextureScale;
	float _ToonThreshold;
	float _ToonSoftness;
float _LightBoostStrength;
float _LightBoostThreshold;
float _LightBoostSoftness;
float4 _LightBoostColor;

            half4 frag(Varyings2 IN) : SV_Target
            {
                // === Base Color ===
                float3 snowTex = tex2D(_MainTex, IN.worldPos.xz * _SnowTextureScale).rgb;

                float3 baseColor = lerp(
                    _Color.rgb,
                    snowTex * _Color.rgb,
                    _SnowTextureOpacity
                );

                // === Normal Mapping ===
                float3 normalTS = UnpackNormal(tex2D(_Normal, IN.worldPos.xz));

                float3 normalWS =
                    normalTS.x * IN.tangent +
                    normalTS.y * IN.bitangent +
                    normalTS.z * IN.normal;

                normalWS = normalize(normalWS);

                // === Main Light ===
                half4 shadowCoord = TransformWorldToShadowCoord(IN.worldPos);
                Light mainLight = GetMainLight(shadowCoord);

                float NdotL = saturate(dot(normalWS, mainLight.direction));

                // Toon step (adjust threshold here)
                //float toon = smoothstep(0.3, 0.7, NdotL);
float toon = smoothstep(
    _ToonThreshold - _ToonSoftness,
    _ToonThreshold + _ToonSoftness,
    NdotL
);



                float shadowAtten = mainLight.shadowAttenuation;

                float lightMask = toon * shadowAtten;

                float3 lightColor = lerp(
                    _ShadowColor.rgb,
                    mainLight.color.rgb,
                    lightMask
                );

                float3 lighting = baseColor * lightColor;
// === Soft Light Boost (Stylized Highlight) ===
float lightBoostMask = smoothstep(
    _LightBoostThreshold - _LightBoostSoftness,
    _LightBoostThreshold + _LightBoostSoftness,
    NdotL
);

lighting += _LightBoostColor.rgb *
            lightBoostMask *
            _LightBoostStrength *
            lightMask;   // keeps it only in lit areas

                // Ambient boost (prevents gray look)
                lighting += baseColor * unity_AmbientSky.rgb * 0.3;

                // === Sparkles ===
                float sparkle = tex2D(_SparkleNoise, IN.uv * _SparkleScale).r;
                lighting += step(_SparkCutoff, sparkle) * 3;

                // === Rim Lighting ===
                float rim = 1.0 - saturate(dot(normalWS, normalize(IN.viewDir)));
                lighting += _RimColor.rgb * pow(rim, _RimPower);

                lighting = MixFog(lighting, IN.fogFactor);

                return float4(lighting, 1.0);
            }

            ENDHLSL
        }

        UsePass "Universal Render Pipeline/Lit/ShadowCaster"
        UsePass "Universal Render Pipeline/Lit/DepthOnly"
    }
}