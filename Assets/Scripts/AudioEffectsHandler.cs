using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffectsHandler : MonoBehaviour
{
    public SoundAudioClip[] soundAudioClips;

    static SoundAudioClip[] soundAudioClipsArray;

    private void Awake() => soundAudioClipsArray = soundAudioClips;

    [System.Serializable]
    public class SoundAudioClip
    {
        public EffectsSounds sound;
        public SoundAudioClip audioClip;
    }

    public static SoundAudioClip GetAudioClip(EffectsSounds sound)
    {
        foreach(SoundAudioClip soundAudioClip in soundAudioClipsArray)
        {
            if (soundAudioClip.sound == sound)
                return soundAudioClip.audioClip;
        }
        return null;
    }
}

// TODO: update with what's needed
public enum EffectsSounds
{
    PlayerLaser_Lv1,
    LaserHit
}