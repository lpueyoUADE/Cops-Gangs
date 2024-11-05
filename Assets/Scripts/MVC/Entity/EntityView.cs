using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class EntityView : EntityBase
{
    [Header("Animator")]
    [SerializeField] protected Animator anim;

    [Header("Audio")]
    [SerializeField] AudioClip shootSound;
    [SerializeField] AudioClip stepSound;
    [SerializeField] AudioClip reloadSound;
    [SerializeField] AudioClip finishReloadSound;
    [SerializeField] AudioClip painSound;
    [SerializeField] AudioClip deadSound;

    EntityModel model;
    Material material;

    protected Dictionary<Sound, AudioClip> audioClipDict;
    protected Dictionary<Sound, AudioSource> audioSourceDict;

    private Color intialMaterialColor;

    public float transitionDuration = 0.4f;

    protected void AddAudioSource(Sound soundKey, AudioClip audioClip)
    {
        if (audioClip != null)
        {
            var newAudioSource = gameObject.AddComponent<AudioSource>();
            newAudioSource.clip = audioClip;
            audioSourceDict[soundKey] = newAudioSource;
        }
    }
    protected override void Awake()
    {
        base.Awake();

        material = Instantiate(GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterial);
        GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterial = material;
        intialMaterialColor = material.color;

        audioClipDict = new()
        {
            {Sound.shoot, shootSound },
            {Sound.step, stepSound },
            {Sound.reload, reloadSound},
            {Sound.finishReload, finishReloadSound},
            {Sound.pain, painSound },
            {Sound.dead, deadSound },
        };

        audioSourceDict = new();

        foreach (var item in audioClipDict)
        {
            AddAudioSource(item.Key, item.Value);
        }

        model = GetComponent<EntityModel>();
        model.OnEmmitSound += OnEmmitSoundHandler;
        model.OnReceivedDamage += OnReceivedDamageHandler;
    }

    protected virtual void OnDestroy()
    {
        model.OnEmmitSound -= OnEmmitSoundHandler;
        model.OnReceivedDamage -= OnReceivedDamageHandler;
    }

    private void OnEmmitSoundHandler(Sound sound)
    {
        if (audioClipDict[sound] != null)
        {
            audioSourceDict[sound].Play();
        }
    }
    private IEnumerator DamageEffectCoroutine()
    {
        material.color = Color.red;

        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / transitionDuration;

            material.color = Color.Lerp(Color.red, intialMaterialColor, t);

            yield return null;
        }

        material.color = intialMaterialColor;
    }
    private void OnReceivedDamageHandler()
    {
        StartCoroutine(DamageEffectCoroutine());
    }

    protected virtual void Update()
    {
        anim.SetFloat("Velocity", new Vector3(Rb.velocity.x, 0, Rb.velocity.z).magnitude);
        anim.SetBool("IsInPain", model.IsInPain);
        anim.SetBool("IsDead", model.IsDead);
    }
}
