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
    [Header("Component References")]
    [SerializeField] List<Sound> sounds;
    [SerializeField] AudioSource source;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PlaySound(SoundType t)
    {
        source.clip = sounds.Find(x => x.type == t).clip;
        source.Play();
        HapticPatterns.PlayPreset(HapticPatterns.PresetType.MediumImpact);
    }
}
