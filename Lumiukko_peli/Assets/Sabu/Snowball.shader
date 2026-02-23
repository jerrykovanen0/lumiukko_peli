Shader "Custom/Snowball"
{
    Properties
    {
        [HDR]_Color("Snow Color", Color) = (1,1,1,1)
        _MainTex("Snow Texture", 2D) = "white" {}
        _SnowTextureScale("Texture Scale", Range(0,5)) = 1

        _Tessellation("Tessellation", Range(1,32)) = 8
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline"="UniversalPipeline" }

        Pass
        {
            Tags { "LightMode"="UniversalForward" }

            HLSLPROGRAM

            #pragma target 5.0
            #pragma vertex vert
            #pragma fragment frag
            #pragma hull hull
            #pragma domain domain

            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _ _ADDITIONAL_LIGHTS

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS   : NORMAL;
                float2 uv         : TEXCOORD0;
            };

            struct ControlPoint
            {
                float4 positionOS : INTERNALTESSPOS;
                float3 normalOS   : NORMAL;
                float2 uv         : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float3 positionWS : TEXCOORD0;
                float3 normalWS   : TEXCOORD1;
                float2 uv         : TEXCOORD2;
            };

            float _Tessellation;
            sampler2D _MainTex;
            float4 _Color;
            float _SnowTextureScale;

            // =========================
            // Vertex
            // =========================

            ControlPoint vert (Attributes v)
            {
                ControlPoint o;
                o.positionOS = v.positionOS;
                o.normalOS = v.normalOS;
                o.uv = v.uv;
                return o;
            }

            // =========================
            // Hull (Tessellation)
            // =========================

            struct TessFactors
            {
                float edge[3] : SV_TessFactor;
                float inside  : SV_InsideTessFactor;
            };

            TessFactors PatchConstantFunction(InputPatch<ControlPoint,3> patch)
            {
                TessFactors f;
                f.edge[0] = _Tessellation;
                f.edge[1] = _Tessellation;
                f.edge[2] = _Tessellation;
                f.inside  = _Tessellation;
                return f;
            }

            [domain("tri")]
            [partitioning("integer")]
            [outputtopology("triangle_cw")]
            [patchconstantfunc("PatchConstantFunction")]
            [outputcontrolpoints(3)]
            ControlPoint hull (InputPatch<ControlPoint,3> patch, uint i : SV_OutputControlPointID)
            {
                return patch[i];
            }

            // =========================
            // Domain
            // =========================

            [domain("tri")]
            Varyings domain (TessFactors factors,
                             OutputPatch<ControlPoint,3> patch,
                             float3 bary : SV_DomainLocation)
            {
                Attributes v;

                v.positionOS =
                    patch[0].positionOS * bary.x +
                    patch[1].positionOS * bary.y +
                    patch[2].positionOS * bary.z;

                v.normalOS =
                    patch[0].normalOS * bary.x +
                    patch[1].normalOS * bary.y +
                    patch[2].normalOS * bary.z;

                v.uv =
                    patch[0].uv * bary.x +
                    patch[1].uv * bary.y +
                    patch[2].uv * bary.z;

                Varyings o;

                o.positionWS = TransformObjectToWorld(v.positionOS.xyz);
                o.positionCS = TransformWorldToHClip(o.positionWS);
                o.normalWS = TransformObjectToWorldNormal(v.normalOS);
                o.uv = v.uv;

                return o;
            }

            // =========================
            // Fragment
            // =========================

            half4 frag (Varyings IN) : SV_Target
            {
                float3 baseTex =
                    tex2D(_MainTex, IN.positionWS.xz * _SnowTextureScale).rgb;

                float3 baseColor = baseTex * _Color.rgb;

                Light mainLight = GetMainLight();
                float3 lighting =
                    baseColor * mainLight.color;

                return float4(lighting, 1);
            }

            ENDHLSL
        }
    }
}