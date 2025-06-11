#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

Texture2D SpriteTexture;

extern float2 resolution;


sampler2D SpriteTextureSampler = sampler_state
{
	Texture = <SpriteTexture>;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

float2 acountForResolution()
{
    return float2(1, 1) / resolution;
}

float average(float4 color)
{
    return color.rgb;
}

float4 MainPS(VertexShaderOutput input) : COLOR
{
    //float2 factor = acountForResolution();
    float2 factor = 0.02;
    
    float4 color1 = tex2D(SpriteTextureSampler, input.TextureCoordinates);
    float4 color2 = tex2D(SpriteTextureSampler, input.TextureCoordinates + (float2(1, 0) * factor));
    float4 color3 = tex2D(SpriteTextureSampler, input.TextureCoordinates + (float2(1, 1) * factor));
    float4 color4 = tex2D(SpriteTextureSampler, input.TextureCoordinates + (float2(0, 1) * factor));
    float4 color5 = tex2D(SpriteTextureSampler, input.TextureCoordinates + (float2(-1, 1) * factor));
    float4 color6 = tex2D(SpriteTextureSampler, input.TextureCoordinates + (float2(-1, 0) * factor));
    float4 color7 = tex2D(SpriteTextureSampler, input.TextureCoordinates + (float2(-1, -1) * factor));
    float4 color8 = tex2D(SpriteTextureSampler, input.TextureCoordinates + (float2(0, -1) * factor));
    float4 color9 = tex2D(SpriteTextureSampler, input.TextureCoordinates + (float2(1, -1) * factor));

    //float4 average = (color1 + color2 + color3 + color4 + color5 + color6 + color7 + color8 + color9) / 9;
    //float4 average = (color2 + color3 + color4 + color5 + color6 + color7 + color8 + color9) / 8;
    //float4 average = (color2 + color3 + color5 + color6 + color7 + color9);
    //float4 average = (color2 + color3 + color4 + color6 + color7 + color8);
    //float4 average = (color3 + color7);
    float4 average = float4(0, 0, 0, 0);
    //float4 average = color1;
    average.r += color1.a - color4.a;
    average.g += color1.a - color8.a;
    return average;
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};