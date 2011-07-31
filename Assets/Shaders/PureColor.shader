Shader "Pure Color" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
}
SubShader {
	Tags { "RenderType"="Opaque" }
	LOD 200

CGPROGRAM
#pragma surface surf Lambert

fixed4 _Color;

struct Input {
	float a; // Not used. Exists only to suppress a compile error.
};

void surf (Input IN, inout SurfaceOutput o) {
	o.Albedo = _Color.rgb;
	o.Alpha = _Color.a;
}
ENDCG
}

Fallback "VertexLit"
}
