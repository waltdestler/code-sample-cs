Shader "Transparent Pure Color" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
}
SubShader {
	Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
	LOD 200

CGPROGRAM
#pragma surface surf Lambert alpha

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
