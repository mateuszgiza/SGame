﻿#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

sampler s0;
texture lightMask;
sampler lightSampler = sampler_state
{ 
	Texture = <lightMask>;
};

float4 MainPS(float2 coords: TEXCOORD0) : COLOR0
{
	float4 color = tex2D(s0, coords);
	float4 lightColor = tex2D(lightSampler, coords);
	return color * lightColor;
}

technique LightTechnique
{
	pass Pass1
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};