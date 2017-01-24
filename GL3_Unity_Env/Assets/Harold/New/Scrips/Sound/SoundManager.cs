using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public AudioClip walking;
    public AudioClip heartbeat;
    public AudioClip enemyHit;
    public AudioClip playerHit;
    public AudioClip weaponSwitch;
    public AudioClip playerJump;
    public AudioClip playerDeath;
    //public AudioClip collect;
    public AudioClip buttonSound;
    public AudioClip ggBoxSound;
    public AudioClip weaponAttack; //list nodig?
    public AudioClip weaponHit; //list nodikg?
    public GameObject[] weaponSoundObject; // kan een source meerdere afpelen?
    public GameObject enemySoundObject;
    public GameObject[] playerWalkObject;
    public GameObject buttonObject;
    public GameObject ggBoxObject;

    public void PlaySound(AudioClip clip, float volume) {
        AudioSource audio = GetComponent<AudioSource>();
        audio.PlayOneShot(clip, volume);
    }

    public void Walk() {
        AudioSource audio = playerWalkObject[0].GetComponent<AudioSource>();
        if (audio.isPlaying == false) {
            audio.PlayOneShot(walking, 1);
        }
    }

    public void HeartBeat() {
        AudioSource audio = playerWalkObject[1].GetComponent<AudioSource>();
        if (audio.isPlaying == false) {
            audio.PlayOneShot(heartbeat, 1);
        }
        else {
            audio.Stop();
        }
    }

    public void PlayerHit() {
        AudioSource audio = playerWalkObject[2].GetComponent<AudioSource>();
        if (audio.isPlaying == false) {
            audio.PlayOneShot(playerHit, 1);
        }
    }

    public void PlayerJump() {
        AudioSource audio = playerWalkObject[3].GetComponent<AudioSource>();
        if (audio.isPlaying == false) {
            audio.PlayOneShot(playerJump, 1);
        }
    }

    public void WeaponSwitch() {
        AudioSource audio = playerWalkObject[4].GetComponent<AudioSource>();
        if (audio.isPlaying == false) {
            audio.PlayOneShot(weaponSwitch, 1);
        }
    }

    public void PlayerDeath() {
        AudioSource audio = playerWalkObject[5].GetComponent<AudioSource>();
        if (audio.isPlaying == false) {
            audio.PlayOneShot(playerDeath, 1);
        }
    }

    public void EnemyHit() {
        AudioSource audio = enemySoundObject.GetComponent<AudioSource>();
        if (audio.isPlaying == false) {
            audio.PlayOneShot(enemyHit, 1);
        }
    }


    public void ButtonSound() {
        AudioSource audio = buttonObject.GetComponent<AudioSource>();
        if (audio.isPlaying == false) {
            audio.PlayOneShot(buttonSound, 1);
        }
    }

    public void GGBox() {
        AudioSource audio = ggBoxObject.GetComponent<AudioSource>();
        if (audio.isPlaying == false) {
            audio.PlayOneShot(ggBoxSound, 1);
        }
    }

    public void WeaponAttack() {
        AudioSource audio = weaponSoundObject[0].GetComponent<AudioSource>();
        if (audio.isPlaying == false) {
            audio.PlayOneShot(weaponAttack, 1);
        }
    }

    public void WeaponHit() {
        AudioSource audio = weaponSoundObject[1].GetComponent<AudioSource>();
        if (audio.isPlaying == false) {
            audio.PlayOneShot(weaponHit, 1);
        }
    }
}
