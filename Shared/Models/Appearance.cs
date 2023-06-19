namespace Shared.Models;

public class Appearance
{
	public uint Sex { get; set; }
	
	public uint FaceFather { get; set; }
	
	public uint FaceMother { get; set; }
	
	public uint SkinFather { get; set; }
	
	public uint SkinMother { get; set; }
	
	public float FaceMix { get; set; }
	
	public float SkinMix { get; set; }
	
	public float[] Structure { get; set; } = Array.Empty<float>();
	
	public ushort Hair { get; set; }
	
	public byte? HairDlc { get; set; }
	
	public byte HairColor1 { get; set; }
	
	public byte HairColor2 { get; set; }

	public HairOverlayData? HairOverlay { get; set; } = default!;
	
	public byte FacialHair { get; set; }
	
	public byte FacialHairColor1 { get; set; }
	
	public float FacialHairOpacity { get; set; }
	
	public byte Eyebrows { get; set; }
	
	public byte EyebrowsColor1 { get; set; }
	
	public float EyebrowsOpacity { get; set; }
	
	public byte? ChestHair { get; set; }
	
	public byte ChestHairColor1 { get; set; }
	
	public float ChestHairOpacity { get; set; }
	
	public ushort Eyes { get; set; }

	public OpacityOverlayData[] OpacityOverlays { get; set; } = Array.Empty<OpacityOverlayData>();

	public ColorOverlayData[] ColorOverlays { get; set; } = Array.Empty<ColorOverlayData>();
}

public class HairOverlayData
{
	public string Overlay { get; set; } = "";
	public string Collection { get; set; } = "";
}

public class OpacityOverlayData
{
	public byte Id { get; set; }
	public byte Value { get; set; }
	public float Opacity { get; set; }
	public string Collection { get; set; } = "";
	public string Overlay { get; set; } = "";
}

public class ColorOverlayData
{
	public byte Id { get; set; }
	public byte Value { get; set; }
	public byte Color1 { get; set; }
	public byte? Color2 { get; set; }
	public float Opacity { get; set; }
}