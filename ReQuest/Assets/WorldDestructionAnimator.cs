using System;
using CRTFilter;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Zenject;


[Serializable]
public struct CrtSettings
{
    public float blur;
    public float chromaticAberration;
    public float pixelResolutionX;
    public float pixelResolutionY;
}

public class WorldDestructionAnimator : MonoBehaviour
{
    [Inject] private ITimeManager _timeManager;
    
    [SerializeField] private Renderer2DData renderer2DData;
    [SerializeField] private CrtSettings defaultCrtSettings;
    [SerializeField] private CrtSettings animatedCrtSettings;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource soundtrack;
    
    private CRTRendererFeature _crtFilter;
    
    [SerializeField] private int firstPhaseTime;
    [SerializeField] private int secondPhaseTime;
    [SerializeField] private int thirdPhaseTime;
    [SerializeField] private int fourthPhaseTime;
    [SerializeField] private int fifthPhaseTime;
    

    private void Start()
    {
        _crtFilter = renderer2DData.rendererFeatures.Find(x => x is CRTRendererFeature) as CRTRendererFeature;
        if (!_crtFilter)
        {
            Debug.LogError("CRT Filter not found");
        }
        
        animatedCrtSettings = defaultCrtSettings;
        
        _timeManager.NewSecond += OnNewSecond;
        
        var layers = animator.layerCount;
        for (int i = 0; i < layers; i++)
        {
            animator.SetLayerWeight(i, 0f);
        }
    }

    private void OnNewSecond(int obj)
    {
        if (obj == firstPhaseTime)
        {
            Debug.Log("First phase");
            var layers = animator.layerCount;
            for (int i = 0; i < layers; i++)
            {
                animator.SetLayerWeight(i, 0.2f);
            }
            soundtrack.pitch = 0.8f;
        }
        if(obj == secondPhaseTime)
        {
            Debug.Log("Second phase");
            var layers = animator.layerCount;
            for (int i = 0; i < layers; i++)
            {
                animator.SetLayerWeight(i, 0.4f);
            }
            soundtrack.pitch = 0.6f;
        }
        if(obj == thirdPhaseTime)
        {
            Debug.Log("Third phase");
            var layers = animator.layerCount;
            for (int i = 0; i < layers; i++)
            {
                animator.SetLayerWeight(i, 0.6f);
            }
            soundtrack.pitch = 0.4f;
        }
        if(obj == fourthPhaseTime)
        {
            Debug.Log("Fourth phase");
            var layers = animator.layerCount;
            for (int i = 0; i < layers; i++)
            {
                animator.SetLayerWeight(i, 0.8f);
            }
            soundtrack.pitch = 0.2f;
        }
        if(obj == fifthPhaseTime)
        {
            Debug.Log("Fifth phase");
            var layers = animator.layerCount;
            for (int i = 0; i < layers; i++)
            {
                animator.SetLayerWeight(i, 1f);
            }
        }
    }

    private void Update()
    {
        _crtFilter.blur = animatedCrtSettings.blur;
        SetChromaticAberration(animatedCrtSettings.chromaticAberration);
        _crtFilter.pixelResolutionX = animatedCrtSettings.pixelResolutionX;
        _crtFilter.pixelResolutionY = animatedCrtSettings.pixelResolutionY;
    }

    private void OnDestroy()
    {
        _crtFilter.blur = defaultCrtSettings.blur;
        SetChromaticAberration(defaultCrtSettings.chromaticAberration);
        _crtFilter.pixelResolutionX = defaultCrtSettings.pixelResolutionX;
        _crtFilter.pixelResolutionY = defaultCrtSettings.pixelResolutionY;
    }
    
    private void SetChromaticAberration(float value)
    {
        _crtFilter.chromaticAberration = value;
        _crtFilter.redOffset = new Vector2(value / 10, value / 10);
        _crtFilter.blueOffset = new Vector2(0, value / 10 * 1.4f);
        _crtFilter.greenOffset = new Vector2(-value / 10, value / 10);
    }
}
