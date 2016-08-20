float4x4 World;
float4x4 View;
float4x4 Projection;



float4 AmbientColor = {0.25f, 0.21f, 0.20f, 1.0f};
float3 LightPosition = { 0,13500, -13500};
float4 LightDirection = {100.0f,100.0f,100.0f,100.0f};

texture2D Texture;

sampler2D DiffuseTextureSampler = sampler_state
{
    Texture = <Texture>;
    MinFilter = linear;
    MagFilter = linear;
    MipFilter = linear;
    AddressU = Mirror;
    AddressV = Mirror;
};

struct VertexShaderInput
{
    float4 Position : POSITION0;
    float3 Normal	: NORMAL0;
    float2 texCoord : TEXCOORD0;
};

struct VertexShaderOutput
{
    float4 Position				: POSITION0;
    float2 texCoord				: TEXCOORD0;
    float3 WorldSpacePosition	: TEXCOORD1;
    float3 Normal				: TEXCOORD2;
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;

	output.WorldSpacePosition = mul(input.Position, World);
	float4x4 worldViewProjection = mul(mul(World, View), Projection);
	output.Position = mul(input.Position, worldViewProjection);
	output.texCoord = input.texCoord;
	output.Normal = normalize(mul(input.Normal, World));

    return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
	float3 LightDirection = normalize(LightPosition - input.WorldSpacePosition);
	float DiffuseLight = dot(LightDirection, input.Normal);
    return (tex2D(DiffuseTextureSampler, input.texCoord) * DiffuseLight) + AmbientColor;
}

technique TextureColor
{
    pass Pass1
    {
        VertexShader = compile vs_2_0 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
