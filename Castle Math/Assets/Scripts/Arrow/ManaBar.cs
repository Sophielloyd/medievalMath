﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaBar : MonoBehaviour {

	public int NumberOfQuestions;
	public GameObject manaBarEnd;

	public Sprite[] PowerUpIcons;

	private int CurrentNumber;
	private LaunchProjectile ProjectileLauncher;

	public GameObject[] PowerUpDisplays;
	private int PowerUpCount;

	private AudioSource A_Source;
	public AudioClip PowerUpSound;

	// Use this for initialization
	void Start () {
		A_Source = GameObject.Find ("UIAudio").GetComponent<AudioSource> ();


		ProjectileLauncher = FindObjectOfType<LaunchProjectile> ();
	}

	public void ClearPowerUp(int PowerupIndex)
	{
		PowerUpDisplays [PowerupIndex].SetActive (false);
	}

	public void QuestionAnswered()
	{
		CurrentNumber += 1;

		if (CurrentNumber >= NumberOfQuestions) {
			CurrentNumber = 0;

			int RanMod = Random.Range (0, 5);
			ArrowModifier newMod;

			if (RanMod == 0) {
				newMod = ArrowModifier.Bomb;
				//todo: add visual cue
			} else if (RanMod == 1) {
				newMod = ArrowModifier.Burst;
			} else if (RanMod == 2) {
				newMod = ArrowModifier.Homing;
			} else if (RanMod == 3) {
				newMod = ArrowModifier.Shotgun;
			} else {
				newMod = ArrowModifier.Giant;
			}

			PowerUpDisplays [PowerUpCount].SetActive (true);
			PowerUpDisplays [PowerUpCount].GetComponent<SpriteRenderer> ().sprite = PowerUpIcons [RanMod];

			//give player perk
			ProjectileLauncher.AddModifier (newMod, PowerUpCount);

			A_Source.clip = PowerUpSound;
			A_Source.Play ();

			if (PowerUpCount < PowerUpDisplays.Length - 1) {
				PowerUpCount += 1;
			} else {
				PowerUpCount = 0;
			}

		}

		float percent = (CurrentNumber * 1f) / NumberOfQuestions;
		transform.localScale = Vector3.Lerp (new Vector3 (.05f, .75f, 1f), new Vector3 (.8f, .75f, 1f), percent);
		//manaBarEnd.transform.position = ;


	}

}
