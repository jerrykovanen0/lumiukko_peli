#if defined(SHADER_API_D3D11) || defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE) || defined(SHADER_API_VULKAN) || defined(SHADER_API_METAL) || defined(SHADER_API_PSSL)
#define UNITY_CAN_COMPILE_TESSELLATION 1
#define UNITY_domain domain
#define UNITY_partitioning partitioning
#define UNITY_outputtopology outputtopology
#define UNITY_patchconstantfunc patchconstantfunc
#define UNITY_outputcontrolpoints outputcontrolpoints
#endif

float3 _LightDirection;
float3 _LightPosition;

float _Tess;
float _MaxTessDistance;

sampler2D _Noise;
float _NoiseScale;
float _NoiseWeight;
float _SnowHeight;

struct Attributes2
{
    float4 vertex : POSITION;
    float3 normal : NORMAL;
    float2 uv : TEXCOORD0;
    float4 tangent : TANGENT;
};

struct ControlPoint
{
    float4 vertex : INTERNALTESSPOS;
    float2 uv : TEXCOORD0;
    float3 normal : NORMAL;
    float4 tangent : TANGENT;
};

struct Varyings2
{
    float4 vertex : SV_POSITION;
    float3 worldPos : TEXCOORD0;
    float3 normal : TEXCOORD1;
    float2 uv : TEXCOORD2;
    float3 viewDir : TEXCOORD3;
    float fogFactor : TEXCOORD4;
    float3 tangent : TEXCOORD5;
    float3 bitangent : TEXCOORD6;
};

struct TessellationFactors
{
    float edge[3] : SV_TessFactor;
    float inside : SV_InsideTessFactor;
};

ControlPoint TessellationVertexProgram(Attributes2 v)
{
    ControlPoint p;
    p.vertex = v.vertex;
    p.uv = v.uv;
    p.normal = v.normal;
    p.tangent = v.tangent;
    return p;
}

TessellationFactors UnityCalcTriEdgeTessFactors(float3 f)
{
    TessellationFactors tess;
    tess.edge[0] = 0.5 * (f.y + f.z);
    tess.edge[1] = 0.5 * (f.x + f.z);
    tess.edge[2] = 0.5 * (f.x + f.y);
    tess.inside = (f.x + f.y + f.z) / 3.0;
    return tess;
}

float CalcDistanceTessFactor(float4 vertex)
{
    float3 worldPos = mul(unity_ObjectToWorld, vertex).xyz;
    float dist = distance(worldPos, _WorldSpaceCameraPos);
    float f = saturate(1.0 - (dist - 2.0) / _MaxTessDistance);
    return (f * _Tess) + 1;
}

TessellationFactors patchConstantFunction(InputPatch<ControlPoint,3> patch)
{
    float3 f;
    f.x = CalcDistanceTessFactor(patch[0].vertex);
    f.y = CalcDistanceTessFactor(patch[1].vertex);
    f.z = CalcDistanceTessFactor(patch[2].vertex);
    return UnityCalcTriEdgeTessFactors(f);
}

[UNITY_domain("tri")]
[UNITY_outputcontrolpoints(3)]
[UNITY_outputtopology("triangle_cw")]
[UNITY_partitioning("fractional_odd")]
[UNITY_patchconstantfunc("patchConstantFunction")]
ControlPoint hull(InputPatch<ControlPoint,3> patch, uint id : SV_OutputControlPointID)
{
    return patch[id];
}

Varyings2 vert(Attributes2 input)
{
    Varyings2 o;

    float3 worldPos = mul(unity_ObjectToWorld, input.vertex).xyz;
    float noise = tex2Dlod(_Noise, float4(worldPos.xz * _NoiseScale,0,0)).r;

    input.vertex.xyz += normalize(input.normal) *
                        saturate(_SnowHeight + noise * _NoiseWeight);

    o.vertex = TransformObjectToHClip(input.vertex.xyz);
    o.worldPos = mul(unity_ObjectToWorld, input.vertex).xyz;
    o.normal = TransformObjectToWorldNormal(input.normal);
    o.uv = input.uv;
    o.viewDir = GetWorldSpaceNormalizeViewDir(o.worldPos);
    o.fogFactor = ComputeFogFactor(o.vertex.z);

    o.tangent = TransformObjectToWorldDir(input.tangent.xyz);
    o.bitangent = cross(o.normal, o.tangent) * input.tangent.w;

    return o;
}

[UNITY_domain("tri")]
Varyings2 domain(TessellationFactors factors,
                 OutputPatch<ControlPoint,3> patch,
                 float3 bary : SV_DomainLocation)
{
    Attributes2 v;

#define INTERP(field) v.field = \
    patch[0].field * bary.x + \
    patch[1].field * bary.y + \
    patch[2].field * bary.z;

    INTERP(vertex)
    INTERP(uv)
    INTERP(normal)
    INTERP(tangent)

    return vert(v);
}