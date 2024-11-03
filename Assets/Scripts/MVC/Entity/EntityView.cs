using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

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

    protected Dictionary<Sound, AudioClip> audioClipDict;
    protected Dictionary<Sound, AudioSource> audioSourceDict;

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
    }

    protected virtual void OnDestroy()
    {
        model.OnEmmitSound -= OnEmmitSoundHandler;
    }

    private void OnEmmitSoundHandler(Sound sound)
    {
        if (audioClipDict[sound] != null)
        {
            audioSourceDict[sound].Play();
        }
    }

    protected virtual void Update()
    {
        anim.SetFloat("Velocity", new Vector3(Rb.velocity.x, 0, Rb.velocity.z).magnitude);
        anim.SetBool("IsInPain", model.IsInPain);
        anim.SetBool("IsDead", model.IsDead);
    }
}
