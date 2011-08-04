Shader "Additive Green Combiner" {
Properties {
    _GreenTex ("Texture", 2D) = "white" { }
	_Negative ("Negative", Range(0, 1)) = 0
	_PreFactor ("Pre-Negative Color Factor", Range(0, 1)) = 1
	_PostFactor ("Post-Negative Color Factor", Range(0, 1)) = 1
}
SubShader {
    Pass {
		Blend One One

CGPROGRAM
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"

sampler2D _GreenTex;
float _Negative;
float _PreFactor;
float _PostFactor;

struct v2f {
    float4  pos : SV_POSITION;
    float2  uvGreen : TEXCOORD0;
};

float4 _GreenTex_ST;

v2f vert (appdata_base v)
{
    v2f o;
    o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
    o.uvGreen = TRANSFORM_TEX(v.texcoord, _GreenTex);
    return o;
}

half4 frag (v2f i) : COLOR
{
	half g = tex2D(_GreenTex, i.uvGreen).g * _PreFactor;
	g = g * (1 - _Negative) + (1 - g) * _Negative;
	g *= _PostFactor;
    return half4(0, g, 0, 1);
}
ENDCG

    }
}
} 