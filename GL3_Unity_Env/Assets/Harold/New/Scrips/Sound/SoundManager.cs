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
    public AudioClip buttonSound;
    public AudioClip ggBoxSound;
    public AudioClip weaponAttack;
    public AudioClip weaponHit;
    public GameObject[] weaponSoundObject;
    public GameObject enemySoundObject;
    public GameObject[] playerWalkObject;
    public GameObject buttonObject;
    public GameObject ggBoxObject;
    public float globalVolume;
    public int hearBeatTreshhold;
    public bool lowHealth;

    //GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SoundManager>()."soundclip";

    public void Walk() {
        AudioSource audio = playerWalkObject[0].GetComponent<AudioSource>();
        if (audio.isPlaying == false) {
            audio.PlayOneShot(walking, globalVolume);
        }
    }

    public void HeartBeat(int healt) {
        AudioSource audio = playerWalkObject[1].GetComponent<AudioSource>();
        if (lowHealth == false) {
            if (healt <= hearBeatTreshhold) {

                if (audio.isPlaying == false) {
                    audio.PlayOneShot(heartbeat, globalVolume);
                }
                lowHealth = true;
            }
        }
        else {
            if (audio.isPlaying == true) {
                audio.Stop();
            }
        }

    }

    public void PlayerHit() {
        AudioSource audio = playerWalkObject[2].GetComponent<AudioSource>();
        if (audio.isPlaying == false) {
            audio.PlayOneShot(playerHit, globalVolume);
        }
    }

    public void PlayerJump() {
        AudioSource audio = playerWalkObject[3].GetComponent<AudioSource>();
        if (audio.isPlaying == false) {
            audio.PlayOneShot(playerJump, globalVolume);
        }
    }

    public void WeaponSwitch() {
        AudioSource audio = playerWalkObject[4].GetComponent<AudioSource>();
        if (audio.isPlaying == false) {
            audio.PlayOneShot(weaponSwitch, globalVolume);
        }
    }

    public void PlayerDeath() {
        AudioSource audio = playerWalkObject[5].GetComponent<AudioSource>();
        if (audio.isPlaying == false) {
            audio.PlayOneShot(playerDeath, globalVolume);
        }
    }

    public void EnemyHit() {
        AudioSource audio = enemySoundObject.GetComponent<AudioSource>();
        if (audio.isPlaying == false) {
            audio.PlayOneShot(enemyHit, globalVolume);
        }
    }


    public void ButtonSound() {
        AudioSource audio = buttonObject.GetComponent<AudioSource>();
        if (audio.isPlaying == false) {
            audio.PlayOneShot(buttonSound, globalVolume);
        }
    }

    public void GGBox() {
        AudioSource audio = ggBoxObject.GetComponent<AudioSource>();
        if (audio.isPlaying == false) {
            audio.PlayOneShot(ggBoxSound, globalVolume);
        }
    }

    public void WeaponAttack() {
        AudioSource audio = weaponSoundObject[0].GetComponent<AudioSource>();
        if (audio.isPlaying == false) {
            audio.PlayOneShot(weaponAttack, globalVolume);
        }
    }

    public void WeaponHit() {
        AudioSource audio = weaponSoundObject[1].GetComponent<AudioSource>();
        if (audio.isPlaying == false) {
            audio.PlayOneShot(weaponHit, globalVolume);
        }
    }
}
