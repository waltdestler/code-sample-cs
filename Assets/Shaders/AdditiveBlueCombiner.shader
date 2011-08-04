Shader "Additive Blue Combiner" {
Properties {
    _BlueTex ("Texture", 2D) = "white" { }
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

sampler2D _BlueTex;
float _Negative;
float _PreFactor;
float _PostFactor;

struct v2f {
    float4  pos : SV_POSITION;
    float2  uvBlue : TEXCOORD0;
};

float4 _BlueTex_ST;

v2f vert (appdata_base v)
{
    v2f o;
    o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
    o.uvBlue = TRANSFORM_TEX(v.texcoord, _BlueTex);
    return o;
}

half4 frag (v2f i) : COLOR
{
	half b = tex2D(_BlueTex, i.uvBlue).b * _PreFactor;
	b = b * (1 - _Negative) + (1 - b) * _Negative;
	b *= _PostFactor;
    return half4(0, 0, b, 1);
}
ENDCG

    }
}
} 