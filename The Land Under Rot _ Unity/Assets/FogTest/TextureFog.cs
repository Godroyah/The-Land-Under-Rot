using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(TextureFogRenderer), PostProcessEvent.AfterStack, "Custom/TextureFog")]
public sealed class TextureFog : PostProcessEffectSettings
{
    [Range(0f, 1f)]
    public FloatParameter fogPower = new FloatParameter { value = 0.5f };
    public FloatParameter skyboxOffset = new FloatParameter { value = 10f };

    [Header("Depth")]
    public FloatParameter distanceBlend = new FloatParameter { value = 10f };
    [Range(0f, 10f)]
    public FloatParameter nearPush = new FloatParameter { value = 10f };
    public ColorParameter depthColor = new ColorParameter();
    public BoolParameter debugDepth = new BoolParameter();

   
}

public sealed class TextureFogRenderer : PostProcessEffectRenderer<TextureFog>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/TextureFog"));

        sheet.properties.SetFloat("_FogPower", settings.fogPower);

        sheet.properties.SetFloat("_SkyboxOffset", settings.skyboxOffset);

        sheet.properties.SetFloat("_DistanceBlend", settings.distanceBlend);
        sheet.properties.SetFloat("_NearPush", settings.nearPush);
        sheet.properties.SetFloat("_DebugDepth", settings.debugDepth ? 1 : 0);

        sheet.properties.SetColor("_FogColor", settings.depthColor);

        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}