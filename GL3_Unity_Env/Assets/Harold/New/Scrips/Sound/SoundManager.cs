using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    private AudioSource sound;

	void Start () {
        sound = GetComponent<AudioSource>();
	}

    public void PlayClip(AudioClip clip, float volume) {
        sound.PlayOneShot(clip, volume);

    }
}
