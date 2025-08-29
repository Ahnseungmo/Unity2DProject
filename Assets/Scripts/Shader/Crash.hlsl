void Crash_float(UnityTexture2D MainTex, UnityTexture2D CrashTex, float2 UV, float4 color, float value, out float4 RGBA)
{
    // 기본 색상을 MainTex에서 가져옵니다.
    float4 basecolor = MainTex.Sample(MainTex.samplerstate, UV);
    
    float crashMask = CrashTex.Sample(CrashTex.samplerstate, UV).a;
    // CrashTex에서 파괴 마스크 값을 가져옵니다.
//    float crashMask = tex2D(CrashTex, UV).a;
    value = 0.5;
    // 파괴 진행 정도에 따라 색상 처리
    // 'value'가 0일 때는 파괴가 전혀 일어나지 않음, value가 1일 때 완전히 파괴된 상태
    float dissolveAmount = smoothstep(0.0, 1.0, crashMask - value);


    
    float finalAlpha = 1.0 - dissolveAmount;
    
    if (crashMask > 0 && basecolor.a > 0)
    {
        RGBA = lerp(basecolor, color, dissolveAmount);
//        RGBA.a = finalAlpha; // 최종 알파 값 수정

    }
    else
    {
        RGBA = basecolor;

    }
    
    
//    color.a = finalAlpha;

    
    float4 texColor = CrashTex.Sample(CrashTex.samplerstate, UV);
    texColor.rgb *= texColor.a;
    RGBA = texColor;

}