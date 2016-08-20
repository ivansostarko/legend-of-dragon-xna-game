
float4x4 World;
float4x4 ViewProjection;

bool textured;

float4 LightDirection = {1.0f,1.0f,1.0f,1.0f};

float Time;
float magnitude;

texture BaseTexture;

sampler2D baseSampler = sampler_state
{
	Texture = <BaseTexture>;
    ADDRESSU = WRAP;
	ADDRESSV = WRAP;
	MAGFILTER = LINEAR;
	MINFILTER = LINEAR;
	MIPFILTER = LINEAR;
};


struct VS_INPUT
{
	float4 Position : POSITION0; 
	float3 Normal	: NORMAL; 
	float2 Texcoord	: TEXCOORD0;
};

struct VS_OUTPUT
{
	float4 Position	: POSITION0;
	float2 Texcoord : TEXCOORD0;
    float3 Normal	: TEXCOORD1;
};

float baseHeight = 0.0f;

VS_OUTPUT VertexShaderCommon (VS_INPUT Input,
	float4x4 instanceWorld	: TEXCOORD1)
{
	VS_OUTPUT Output; 
	
	float4 vertex = mul(Input.Position, instanceWorld);
	float amplitude = magnitude * (vertex.y - baseHeight);
	float4 wave = amplitude * float4(sin(Time + vertex.x),
		0,
		cos(Time + vertex.z),
		0);
	vertex = vertex + wave;
	Output.Position = mul(vertex, ViewProjection); 
	Output.Texcoord = Input.Texcoord;
	Output.Normal = mul(Input.Normal, instanceWorld);
	
	return Output;
}

VS_OUTPUT NoInstancingVertexShader (VS_INPUT Input)
{
	return VertexShaderCommon(Input, World);
}

VS_OUTPUT InstancedVertexShader (VS_INPUT Input,
	float4x4 instanceWorld	: TEXCOORD1)
{
	return VertexShaderCommon(Input, transpose(instanceWorld));
}

float4 PixelShader (VS_OUTPUT Input) : COLOR0
{
	float4 texColor = tex2D(baseSampler, Input.Texcoord);
	clip(texColor.a - 0.7);
	float3 Normal = normalize(Input.Normal);
	float4 Diffuse = max(dot(Normal, LightDirection.xyz),0.0);
		
	return texColor * Diffuse * 0.5 + texColor * 0.5;
}


technique Textured
{
    pass Pass1
    {
		VertexShader = compile vs_2_0 NoInstancingVertexShader();
        PixelShader = compile ps_2_0 PixelShader();
    }
}

technique Instanced
{
    pass Pass1
    {
		VertexShader = compile vs_3_0 InstancedVertexShader();
        PixelShader = compile ps_3_0 PixelShader();
    }
}