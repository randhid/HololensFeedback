using UnityEngine;

/// <summary>
/// Helper class for drawing Enums in custom Unity Editor inspector windows.
/// </summary>
public class WMG_EnumFlagAttribute : PropertyAttribute
{
	public string enumName;
	
	public WMG_EnumFlagAttribute() {}
	
	public WMG_EnumFlagAttribute(string name) {
		enumName = name;
	}

    public override object TypeId => base.TypeId;

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override bool IsDefaultAttribute()
    {
        return base.IsDefaultAttribute();
    }

    public override bool Match(object obj)
    {
        return base.Match(obj);
    }

    public override string ToString()
    {
        return base.ToString();
    }
}