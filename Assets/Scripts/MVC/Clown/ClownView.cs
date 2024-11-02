using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownView : NPCView
{
    [Header("Clown Audio")]
    [SerializeField] AudioClip runningAwaySound;

    protected override void Awake()
    {
        base.Awake();

        audioClipDict[Sound.RunningAway] = runningAwaySound;
        AddAudioSource(Sound.RunningAway, runningAwaySound);
    }
}

