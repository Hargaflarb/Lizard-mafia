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
static const float resizer = 1 / fadeLength;
extern float2 lightPositions[5];


// messured in % of the screen(image) width
extern float lightRadius;

extern float Upper;
extern float Lower;
extern float Offset;
extern float Distance;


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

float IsInShadow(float2 dif)
{
    float Pa = atan2(dif.y, dif.x) + Offset;
    return step((abs(Upper - Pa) + abs(Pa - Lower)), abs(Upper - Lower));

    //return (Pa <= Upper) & (Pa >= Lower) & (Distance <= length(dif));
}

float4 MainPS(VertexShaderOutput input) : COLOR
{
    float4 pixelColor = tex2D(SpriteTextureSampler, input.TextureCoordinates);
    float2 pixelPosition = input.TextureCoordinates;
    float2 lightPosition = input.Color.xy;
    float1 lightRadius = input.Color.z;
    
    float2 dif = AdjustForAspectRatio(pixelPosition - lightPosition);
    float distance = length(dif);
    pixelColor.a += IsInShadow(dif) * step(Distance, distance);
    

    return pixelColor;
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};