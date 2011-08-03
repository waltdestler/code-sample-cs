Shader "Additive Red Combiner" {
Properties {
    _RedTex ("Texture", 2D) = "white" { }
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

sampler2D _RedTex;
float _Negative;
float _PreFactor;
float _PostFactor;

struct v2f {
    float4  pos : SV_POSITION;
    float2  uvRed : TEXCOORD0;
};

float4 _RedTex_ST;

v2f vert (appdata_base v)
{
    v2f o;
    o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
    o.uvRed = TRANSFORM_TEX(v.texcoord, _RedTex);
    return o;
}

half4 frag (v2f i) : COLOR
{
	half r = tex2D(_RedTex, i.uvRed).r * _PreFactor;
	r = (1 - r) * (1 - _Negative) + r * _Negative;
	r *= _PostFactor;
    return half4(r, 0, 0, 1);
}
ENDCG

    }
}
} 