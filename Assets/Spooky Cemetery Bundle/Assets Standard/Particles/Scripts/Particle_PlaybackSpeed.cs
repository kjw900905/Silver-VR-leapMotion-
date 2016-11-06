using UnityEngine;
using System.Collections;

public class Particle_PlaybackSpeed : MonoBehaviour
{
	ParticleSystem GameObject;
	public float PlaybackSpeed = 0.5f;

	// Use this for initialization
	void Start ()
	{
		GameObject = gameObject.GetComponent<ParticleSystem>();
		GameObject.playbackSpeed = PlaybackSpeed;
	}
}
