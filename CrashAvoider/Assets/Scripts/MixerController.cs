using UnityEngine;
using UnityEngine.Audio;

public class MixerController : MonoBehaviour
{
    public AudioMixer Mixer;
    public float VolumeValue;
public void SetVolume(float volume)
    {
        VolumeValue = volume;
        Mixer.SetFloat("Music", volume);
    }
}
