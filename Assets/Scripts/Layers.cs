public static class Layers
{
	public const int Rgb = 8;
	public const int R = 9;
	public const int G = 10;
	public const int B = 11;
	public const int Rg = 12;
	public const int Rb = 13;
	public const int Gb = 14;

	public const int RgbMask = 1 << Rgb;
	public const int RMask = 1 << R;
	public const int GMask = 1 << G;
	public const int BMask = 1 << B;
	public const int RgMask = 1 << Rg;
	public const int RbMask = 1 << Rb;
	public const int GbMask = 1 << Gb;

	public const int RCollisionMask = /*RgbMask |*/ RMask | RgMask | RbMask;
	public const int GCollisionMask = /*RgbMask |*/ GMask | RgMask | GbMask;
	public const int BCollisionMask = /*RgbMask |*/ BMask | RbMask | GbMask;

	public static int FromRgbMode(RgbMode mode)
	{
		switch(mode)
		{
			case RgbMode.Rgb: return Rgb;
			case RgbMode.R: return R;
			case RgbMode.G: return G;
			case RgbMode.B: return B;
			case RgbMode.Rg: return Rg;
			case RgbMode.Rb: return Rb;
			case RgbMode.Gb: return Gb;
			default: return 0;
		}
	}
}