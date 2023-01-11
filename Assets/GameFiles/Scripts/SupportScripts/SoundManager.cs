using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lofelt.NiceVibrations;

[System.Serializable]
public class Sound
{
    public SoundType type;
    public AudioClip clip;
}
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance = null;

    [Header("Component References")]
    [SerializeField] List<Sound> sounds;
    [SerializeField] AudioSource source;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        Instance = this;
    }


    public void PlaySound(SoundType t)
    {
        source.clip = sounds.Find(x => x.type == t).clip;
        source.Play();
        HapticPatterns.PlayPreset(HapticPatterns.PresetType.MediumImpact);
    }
}
