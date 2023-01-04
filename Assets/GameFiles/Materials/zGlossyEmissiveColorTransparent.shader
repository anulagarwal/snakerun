// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "sundy/ColorCameraFade"
{
	Properties
	{
		_Color("Color", Color) = (0.3644536,0.8490566,0.7650407,0)
		_ColorShine("ColorShine", Color) = (1,1,1,0)
		_Shine("Shine", Range( 0 , 2)) = 0
		_Smoothness("Smoothness", Range( 0 , 1)) = 0
		_metallic("metallic", Range( 0 , 1)) = 0
		_Bias("Bias", Range( 0 , 1)) = 0
		_falloff("falloff", Range( 0 , 10)) = 0
		_distamce("distamce", Range( 0 , 20)) = 0
		_Scale("Scale", Range( 0 , 3)) = 0
		_Power("Power", Range( 0 , 8)) = 0
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 4.6
		#pragma surface surf Standard alpha:fade keepalpha noshadow vertex:vertexDataFunc 
		struct Input
		{
			float3 worldPos;
			float3 worldNormal;
			float eyeDepth;
		};

		uniform float4 _Color;
		uniform float4 _ColorShine;
		uniform float _Bias;
		uniform float _Scale;
		uniform float _Power;
		uniform float _Shine;
		uniform float _metallic;
		uniform float _Smoothness;
		uniform float _falloff;
		uniform float _distamce;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			o.eyeDepth = -UnityObjectToViewPos( v.vertex.xyz ).z;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float3 ase_worldNormal = i.worldNormal;
			float fresnelNdotV2 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode2 = ( _Bias + _Scale * pow( 1.0 - fresnelNdotV2, _Power ) );
			o.Emission = ( _Color + ( _ColorShine * fresnelNode2 * _Shine ) ).rgb;
			o.Metallic = _metallic;
			o.Smoothness = _Smoothness;
			float cameraDepthFade30 = (( i.eyeDepth -_ProjectionParams.y - _distamce ) / _falloff);
			float clampResult35 = clamp( saturate( cameraDepthFade30 ) , 0.4 , 1.0 );
			o.Alpha = clampResult35;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18900
90.4;84.8;1290.4;644.6;699.6224;148.7946;1.707366;True;False
Node;AmplifyShaderEditor.RangedFloatNode;11;-841.5,222;Inherit;False;Property;_Bias;Bias;5;0;Create;True;0;0;0;False;0;False;0;0.183;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;13;-848.5,393;Inherit;False;Property;_Power;Power;9;0;Create;True;0;0;0;False;0;False;0;1.08;0;8;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-835.5,303;Inherit;False;Property;_Scale;Scale;8;0;Create;True;0;0;0;False;0;False;0;3;0;3;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;31;774.0323,173.2573;Inherit;False;Property;_falloff;falloff;6;0;Create;True;0;0;0;False;0;False;0;0.5;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;32;802.6321,239.5575;Inherit;False;Property;_distamce;distamce;7;0;Create;True;0;0;0;False;0;False;0;3;0;20;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;3;-499.5,54;Inherit;False;Property;_ColorShine;ColorShine;1;0;Create;True;0;0;0;False;0;False;1,1,1,0;0.4260587,0.1273585,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CameraDepthFade;30;1027.67,95.95428;Inherit;False;3;2;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FresnelNode;2;-535.5,260;Inherit;False;Standard;WorldNormal;ViewDir;False;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;5;-454.5,512;Inherit;False;Property;_Shine;Shine;2;0;Create;True;0;0;0;False;0;False;0;0.361;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;4;-163.5,150;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;1;-496.5,-116;Inherit;False;Property;_Color;Color;0;0;Create;True;0;0;0;False;0;False;0.3644536,0.8490566,0.7650407,0;1,0,0.01900101,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;34;1205.132,280.0574;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;10;6.208933,-110.6103;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;7;52.5,246;Inherit;True;Property;_Smoothness;Smoothness;3;0;Create;True;0;0;0;False;0;False;0;0.61;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;8;67.49582,95.14841;Inherit;False;Property;_metallic;metallic;4;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;35;1315.432,368.9972;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0.4;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1303.449,-166.1753;Float;False;True;-1;6;ASEMaterialInspector;0;0;Standard;sundy/ColorCameraFade;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;False;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;3.5;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;30;0;31;0
WireConnection;30;1;32;0
WireConnection;2;1;11;0
WireConnection;2;2;12;0
WireConnection;2;3;13;0
WireConnection;4;0;3;0
WireConnection;4;1;2;0
WireConnection;4;2;5;0
WireConnection;34;0;30;0
WireConnection;10;0;1;0
WireConnection;10;1;4;0
WireConnection;35;0;34;0
WireConnection;0;2;10;0
WireConnection;0;3;8;0
WireConnection;0;4;7;0
WireConnection;0;9;35;0
ASEEND*/
//CHKSM=1953C113A0169269331AEC00A1F3FB05C3398699