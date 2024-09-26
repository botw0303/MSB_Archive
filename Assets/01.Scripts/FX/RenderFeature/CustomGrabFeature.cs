using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class CustomGrabFeature : ScriptableRendererFeature
{
    [System.Serializable]
    public class Settings
    {
        [Header("Pass Settings")]
        // ���̴����� �޾ƿ��� GrabPass Texture2D ������
        public string Texture2DPropertyName = "_CustomCameraTexture";
        // �׷��н� ������ ���� ����
        public RenderPassEvent passEvent = RenderPassEvent.AfterRenderingTransparents;
        // �׷��н� �ٿ� ���ø�
        public Downsampling downsamplingMethod = Downsampling.None;

        // ĸó ������Ʈ Ŀ���� ���� ���͸� ���� (�ߺ� GrabPass ����)
        [Header("Target Object Settings")]
        public LayerMask layerMask;
        public string[] shaderTagStrings = new string[]
            {"UniversalForwardOnly", "SRPDefaultUnlit", "LightweightForward", "UniversalForward"};
    }

    // ���� ����
    [SerializeField] private Settings _settings;
    // �׷� �н�
    private GrabPass _grabPass;
    // Ư�� ������Ʈ Ŀ���� ���� �н�
    private FilterPass _renderPass;

    public override void Create()
    {
        List<ShaderTagId> shaderTagIds = new List<ShaderTagId>();
        for (int i = 0; i < _settings.shaderTagStrings.Length; i++)
        {
            shaderTagIds.Add(new ShaderTagId(_settings.shaderTagStrings[i]));
        }
        _grabPass = new GrabPass(shaderTagIds,_settings.passEvent, _settings.Texture2DPropertyName, _settings.downsamplingMethod, _settings.layerMask);
        _renderPass = new FilterPass(_settings.passEvent + 1, shaderTagIds, _settings.layerMask);
    }



    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        //_grabPass.Setup(renderer.cameraColorTargetHandle);

        renderer.EnqueuePass(_grabPass);
        renderer.EnqueuePass(_renderPass);
    }
}
