using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : Singleton<AudioManager> {
	protected AudioManager() {}

	AudioSource musicPlayer;

	Dictionary<string, AudioClip> music;
	Dictionary<string, AudioClip> sounds;

	void Awake () {
		musicPlayer = gameObject.AddComponent<AudioSource>();

		music = new Dictionary<string, AudioClip>();
		sounds = new Dictionary<string, AudioClip>();

		// Load music
		//**Note** The directory in Load<T> must be a sub directory of a directory called Resources in the unity Assets folder
		music["canon"] = Resources.Load<AudioClip>("Audio/Music/canon");
		music["sea"] = Resources.Load<AudioClip>("Audio/Music/sea");

		// Load sound effects
		sounds["fireBall"] = Resources.Load<AudioClip>("Audio/Effects/fireBall");
		sounds["shootFireBall"] = Resources.Load<AudioClip>("Audio/Effects/shootFireBall");
		sounds["checkpoint"] = Resources.Load<AudioClip>("Audio/Effects/checkpoint");

		// Automatically start background music
		PlayMusic("canon", 1.0f);
		PlayLoopingMusic("sea", 0.8f);
	}
	
	public void Mute () {
		//GameManager.Instance.player.GetComponent<AudioListener>().enabled = !GameManager.Instance.player.GetComponent<AudioListener>().enabled;

		// find better way to mute everything, but try:
		/*
		 AudioListener.pause = true;
		 AudioListener.volume = 0;
		*/
		AudioListener al = null;
		foreach (GameObject go in GameObject.FindGameObjectsWithTag("MainCamera")) {
			al = go.GetComponent<AudioListener>(); 
			if (al != null) {
				break;
			}
		}

		if (al != null) {
			//al.audio.Pause();
			//al.audio.volume = 0;
			al.enabled = !al.enabled;
		}
	}

	public void PlayMusic(string toPlay, float volume = 0.08f) {
		musicPlayer.Stop();
		try {
			musicPlayer.clip = music[toPlay];
		} catch(KeyNotFoundException) {
			Debug.LogWarning("AudioManager::PlayMusic(" + toPlay + "): [KeyNotFoundException] Clip of this name not found.");
			return;
		} catch(NullReferenceException) {
			Debug.LogWarning("AudioManager::PlayMusic(" + toPlay + "): [NullReferenceException] Clip of this name not loaded from Resources.");
			return;
		}
		musicPlayer.volume = volume;
		musicPlayer.Play();
	}

	public void PlayLoopingMusic(string toPlay, float volume = 0.08f) {
		try {
			PlayLoopingMusicHelper(music[toPlay], volume);
		} catch(KeyNotFoundException) {
			Debug.LogWarning("AudioManager::PlayLoopingMusic(" + toPlay + "): [KeyNotFoundException] Clip of this name not found.");
			return;
		} catch(NullReferenceException) {
			Debug.LogWarning("AudioManager::PlayLoopingMusic(" + toPlay + "): [NullReferenceException] Clip of this name not loaded from Resources.");
			return;
		}
	}

	public void StopMusic() {
		musicPlayer.Stop();
	}

	public AudioClip getAudioClip(string clip) {
		try {
			return sounds[clip];
		} catch(KeyNotFoundException) {
			Debug.LogWarning("AudioManager::getAudioClip(" + clip + "): [KeyNotFoundException] Clip of this name not found.");
			return null;
		} catch(NullReferenceException) {
			Debug.LogWarning("AudioManager::getAudioClip(" + clip + "): [NullReferenceException] Clip of this name not loaded from Resources.");
			return null;
		}
	}

	public void PlaySoundEffect(string toPlay, GameObject location, float volume = 1.0f) {
		try {
			PlayClipAtLocation(sounds[toPlay], location.transform.position, volume);
		} catch(KeyNotFoundException) {
			Debug.LogWarning("AudioManager::PlaySoundEffect(" + toPlay + "): [KeyNotFoundException] Clip of this name not found.");
			return;
		} catch(NullReferenceException) {
			Debug.LogWarning("AudioManager::PlaySoundEffect(" + toPlay + "): [NullReferenceException] Clip of this name not loaded from Resources.");
			return;
		}
	}

	#region helper
	private AudioSource PlayClipAtLocation(AudioClip clip, Vector3 pos, float volume) {
		GameObject temp = new GameObject("OneShotAudio");
		temp.transform.position = pos;
		AudioSource source = temp.AddComponent<AudioSource>();
		source.clip = clip;
		source.volume = volume;
		source.Play();
		Destroy(temp, clip.length);
		return source;
	}

	private AudioSource PlayLoopingMusicHelper(AudioClip clip, float volume) {
		GameObject temp = new GameObject("LoopingMusic");
		AudioSource source = temp.AddComponent<AudioSource>();
		source.clip = clip;
		source.volume = volume;
		source.loop = true;
		source.Play();
		return source;
	}
	#endregion
}
