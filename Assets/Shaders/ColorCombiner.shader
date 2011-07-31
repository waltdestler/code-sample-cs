Shader "Color Combiner" {
Properties {
    _RedTex ("Texture", 2D) = "white" { }
    _GreenTex ("Texture", 2D) = "white" { }
    _BlueTex ("Texture", 2D) = "white" { }
}
SubShader {
    Pass {

CGPROGRAM
#pragma target 3.0
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"

sampler2D _RedTex;
sampler2D _GreenTex;
sampler2D _BlueTex;

struct v2f {
    float4  pos : SV_POSITION;
    float2  uvRed : TEXCOORD1;
    float2  uvGreen : TEXCOORD2;
    float2  uvBlue : TEXCOORD3;
};

float4 _RedTex_ST;
float4 _GreenTex_ST;
float4 _BlueTex_ST;

v2f vert (appdata_base v)
{
    v2f o;
    o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
    o.uvRed = TRANSFORM_TEX(v.texcoord, _RedTex);
	o.uvGreen = TRANSFORM_TEX(v.texcoord, _GreenTex);
	o.uvBlue = TRANSFORM_TEX(v.texcoord, _BlueTex);
    return o;
}

half4 frag (v2f i) : COLOR
{
	half r = 1 - tex2D(_RedTex, i.uvRed).r;
	half g = 1 - tex2D(_GreenTex, i.uvGreen).g;
	half b = 1 - tex2D(_BlueTex, i.uvBlue).b;
    return half4(r, g, b, 1);
}
ENDCG

    }
}
} 