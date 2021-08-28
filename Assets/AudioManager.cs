using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource[] audio_hits;
    [SerializeField] private AudioSource[] audio_single_shots;
    [SerializeField] private AudioSource[] audio_spierce_shots;
    [SerializeField] private AudioSource[] audio_wall_hits;
    [SerializeField] private AudioSource[] audio_extra_seconds;
    [SerializeField] private AudioSource[] audio_knockbacks;


    public void PlayAudioHit(Vector3 pos)
    {
        AudioSource audio = audio_hits[Random.Range(0, audio_hits.Length)];
        audio.time = Random.Range(0, 0.1f);
        audio.Play();
    }
    public void PlayAudioShot(Vector3 pos)
    {
        AudioSource audio = audio_single_shots[Random.Range(0, audio_single_shots.Length)];
        audio.time = Random.Range(0, 0.05f);
        audio.Play();

        /* AudioSource.PlayClipAtPoint(audio.clip, pos); */
    }
    public void PlayAudioPierce(Vector3 pos)
    {
        AudioSource audio = audio_spierce_shots[Random.Range(0, audio_spierce_shots.Length)];
        audio.time = Random.Range(0, 0.05f);
        audio.Play();
        /* AudioSource.PlayClipAtPoint(audio.clip, pos); */
    }

    public void PlayAudioWallHit(Vector3 pos)
    {
        AudioSource audio = audio_wall_hits[Random.Range(0, audio_wall_hits.Length)];
        audio.time = Random.Range(0, 0.05f);
        audio.Play();
        /* AudioSource.PlayClipAtPoint(audio.clip, pos); */
    }

    public void PlayAudioExtraSeconds(Vector3 pos)
    {
        AudioSource audio = audio_extra_seconds[Random.Range(0, audio_extra_seconds.Length)];
        audio.Play();
        audio.time = Random.Range(0, 0.05f);
        /* AudioSource.PlayClipAtPoint(audio.clip, pos); */
    }

    public void PlayAudioHitSecondary(Vector3 pos)
    {
        AudioSource audio = audio_knockbacks[Random.Range(0, audio_knockbacks.Length)];
        audio.Play();
        audio.time = Random.Range(0, 0.05f);
        /* AudioSource.PlayClipAtPoint(audio.clip, pos); */
    }
}
