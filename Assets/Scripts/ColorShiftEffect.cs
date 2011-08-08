using UnityEngine;

[ExecuteInEditMode]
public class ColorShiftEffect : ImageEffectBase
{
	public float HueShiftDegrees;
	public float SaturationFactor = 1;
	public float ValueFactor = 1;
	public bool OverrideColorBlindSetting;

	public void Update()
	{
		if(!OverrideColorBlindSetting)
			HueShiftDegrees = Settings.ColorBlindHueShift;
	}

	// Called by camera to apply image effect
	public void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		// Algorithm taken from: http://beesbuzz.biz/code/hsv_color_transforms.php

		float h = HueShiftDegrees;
		float s = SaturationFactor;
		float v = ValueFactor;
		float vsu = v * s * Mathf.Cos(h * Mathf.PI / 180);
		float vsw = v * s * Mathf.Sin(h * Mathf.PI / 180);

		Matrix4x4 m = new Matrix4x4();
		m[0, 0] = .299f * v + .701f * vsu + .168f * vsw;
		m[0, 1] = .587f * v - .587f * vsu + .330f * vsw;
		m[0, 2] = .114f * v - .114f * vsu - .497f * vsw;
		m[1, 0] = .299f * v - .299f * vsu - .328f * vsw;
		m[1, 1] = .587f * v + .413f * vsu + .035f * vsw;
		m[1, 2] = .114f * v - .114f * vsu + .292f * vsw;
		m[2, 0] = .299f * v - .300f * vsu + 1.25f * vsw;
		m[2, 1] = .587f * v - .588f * vsu - 1.05f * vsw;
		m[2, 2] = .114f * v + .886f * vsu - .203f * vsw;
		m[3, 3] = 1;

		material.SetMatrix("_ColorMatrix", m);
		Graphics.Blit(source, destination, material);
	}
}
