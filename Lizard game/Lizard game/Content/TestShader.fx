

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

float4 MainPS(VertexShaderOutput input) : COLOR
{
    float4 pixelColor = tex2D(SpriteTextureSampler, input.TextureCoordinates);
    float2 pixelPosition = input.TextureCoordinates;
    
    float distance;
    for (int index = 0; index < 5; index = index + 1)
    {
        distance = length(AdjustForAspectRatio(pixelPosition - lightPositions[index]));
        pixelColor.a -= 1 - clamp((distance - (lightRadius - fadeLength)) * resizer, 0, 1);
        //pixelColor.a = min(pixelColor.a, ((distance - (lightRadius - fadeLength)) * resizer));
        
    }
    
    
    
    
    return pixelColor;
}

technique SpriteDrawing
{
    pass P0
    {
        PixelShader = compile PS_SHADERMODEL /**/MainPS();
    }
};