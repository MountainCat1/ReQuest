using System;
using CRTFilter;
using Managers;
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

public class CrtEffectController : MonoBehaviour
{
    [Inject] private IPhaseManager _phaseManager;
    [Inject] private IPlayerCharacterProvider _playerProvider;
    
    [SerializeField] private Renderer2DData renderer2DData;
    [SerializeField] private CrtSettings defaultCrtSettings;
    [SerializeField] private CrtSettings animatedCrtSettings;
    [SerializeField] private CrtSettings deathCrtSettings;
    [SerializeField] private Animator animator;
    
    private CRTRendererFeature _crtFilter;
    
    private static readonly int PlayerDeath = Animator.StringToHash("PlayerDeath");

    private void Start()
    {
        _crtFilter = renderer2DData.rendererFeatures.Find(x => x is CRTRendererFeature) as CRTRendererFeature;
        if (!_crtFilter)
        {
            Debug.LogError("CRT Filter not found");
        }
        
        animatedCrtSettings = defaultCrtSettings;
        
        _phaseManager.PhaseChanged += OnNewPhase;
        
        SetLayerWeight(0f);
        
        var player = _playerProvider.Get();
        player.Death += OnPlayerDeath;
    }

    private void OnPlayerDeath(DeathContext obj)
    {
        animator.SetTrigger(PlayerDeath);
    }

    private void OnNewPhase(int phase)
    {
        if(_playerProvider.PlayerDead)
            return;
        
        if (phase == 1)
        {
            SetLayerWeight(0.2f);
        }
        if(phase == 2)
        {
            SetLayerWeight(0.4f);
        }
        if(phase == 3)
        {
            SetLayerWeight(0.6f);
        }
        if(phase == 4)
        {
            SetLayerWeight(0.8f);
        }
        if(phase == 5)
        {
            SetLayerWeight(1f);
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
    
    private void SetLayerWeight(float weight)
    {
        var layers = animator.layerCount;
        for (int i = 0; i < layers; i++)
        {
            animator.SetLayerWeight(i, weight);
        }
    }
}

