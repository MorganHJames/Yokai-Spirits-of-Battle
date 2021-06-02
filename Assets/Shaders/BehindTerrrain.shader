Shader "BehindTerrain"
{
	SubShader
	{
		Tags{ "RenderType" = "Opaque" "Queue" = "Geometry-1" }

		Stencil
		{
			Ref 1
			Comp Always
			Pass Replace
		}

		ZWrite off

		Pass {}
	}
}