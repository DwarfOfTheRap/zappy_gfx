  Shader "Character/Disintegrate Bumped Diffuse" {
    Properties {
        _MainTex("Albedo", 2D) = "white" {}
      	_NoiseTex ("Effect Map (RGB)", 2D) = "white" {}
      	_DisintegrateAmount ("Effect Amount", Range(0.0, 1.01)) = 0.0
      	_DissolveColor("Edge Color", Color) = (1.0,0.5,0.2,0.0)
      	_EdgeEmission ("Edge Emission", Color) = (0,0,0,0)
      	_DissolveEdge("Edge Range",Range(0.0,0.1)) = 0.01
      	_TileFactor ("Tile Factor", Range(0.0,4.0)) = 1.0
        // Added Outline properties
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _Outline ("Outline width", Range (.002, 0.03)) = .005
        // -------------------------
    }
    SubShader {
      Tags { "RenderType" = "Opaque" }
      CGPROGRAM
      #pragma target 3.0
      #pragma surface surf Lambert addshadow nolightmap
      struct Input {
          float2 uv_MainTex;
          float2 uv_BumpMap;
      };
      sampler2D _MainTex;
      sampler2D _BumpMap;
      sampler2D _NoiseTex;
      float  _DisintegrateAmount;
      float4 _DissolveColor;
      float  _DissolveEdge;
      float  _TileFactor;
      float4 _EdgeEmission;  
      
      void surf (Input IN, inout SurfaceOutput o) 
      {
          float clipval = tex2D (_NoiseTex, IN.uv_MainTex * _TileFactor).rgb - _DisintegrateAmount; 
          
          clip(clipval);
          

          if (clipval < _DissolveEdge && _DisintegrateAmount > 0)
          {
              o.Emission = _EdgeEmission;
              o.Albedo = _DissolveColor;          
          }
          else
          {
              o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
          }     
          float4 nrm =  tex2D (_BumpMap,IN.uv_BumpMap);    
          o.Normal = UnpackNormal(nrm);       
      }
      ENDCG
    }
    Fallback "Diffuse"
  }