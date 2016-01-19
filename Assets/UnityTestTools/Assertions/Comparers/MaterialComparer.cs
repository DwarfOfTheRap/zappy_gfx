using UnityEngine;
using UnityTest;
using System.Collections;

public class MaterialComparer : ComparerBaseGeneric<Material> {

	#region implemented abstract members of ComparerBaseGeneric
	protected override bool Compare (Material a, Material b)
	{
		return (a.color == b.color
			&& a.globalIlluminationFlags == b.globalIlluminationFlags
			&& a.hideFlags == b.hideFlags
			&& a.mainTexture == b.mainTexture
			&& a.mainTextureOffset == b.mainTextureOffset
			&& a.mainTextureScale == b.mainTextureScale
			&& a.passCount == b.passCount
			&& a.renderQueue == b.renderQueue
			&& a.shader == b.shader);
	}
	#endregion
}
