#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

Texture2D SpriteTexture;

static const float aspectRatio = 9.0 / 16.0;
static const float fadeLength = 0.05;
static const float resizer = 1.0 / fadeLength;

extern float3 shadowData[100];
extern float2 lightPositions[100];


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

float2 AdjustForAspectRatio(float2 position)
{
    return float2(position.x, position.y * aspectRatio);
}

float IsInShadow(float2 dif, float1 offset, float1 upper)
{
    float Pa = atan2(dif.y, dif.x) + offset;
    return step((abs(upper - Pa) + abs(Pa)), upper);

    //return (Pa <= Upper) & (Pa >= Lower) & (Distance <= length(dif));
}

float4 MainPS(VertexShaderOutput input) : COLOR
{
    float4 pixelColor = tex2D(SpriteTextureSampler, input.TextureCoordinates);
    
    int index = round(input.Color.r * 255);
    float2 pixelPosition = input.TextureCoordinates;
    float2 lightPosition = lightPositions[index];
    float1 upperAngle = shadowData[index].x;
    float1 angleOffset = shadowData[index].y;
    float1 casterDistance = shadowData[index].z;
    
    pixelColor.a = 0;
    
    float2 dif = AdjustForAspectRatio(pixelPosition - lightPosition);
    float pixelDistance = length(dif);
    pixelColor.a += IsInShadow(dif, angleOffset, upperAngle) * step(casterDistance, pixelDistance) * (1 - clamp((pixelDistance - (0.3 - fadeLength)) * resizer, 0.0, 1.0));
    

    return pixelColor;
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};