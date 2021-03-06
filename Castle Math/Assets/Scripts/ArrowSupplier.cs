﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSupplier : MonoBehaviour {

	public int NumberToSpawn = 12;

	public AudioClip SpawnSound;

	public int NumberOfArrows;

	public GameObject[] ArrowToSpawn;

	public GameObject[] Arrows;
	public List<int> ArrowIndex;

	private int AmountToSpawn;

	private AudioSource A_Source;

	// Use this for initialization
	void Start () {

		A_Source = GameObject.Find ("UIAudio").GetComponent<AudioSource> ();

		Arrows = new GameObject[0];

		CreateArrow ();

	}


	public void CreateArrow()
	{
		NumberOfArrows += NumberToSpawn;

		GroupResize (NumberOfArrows,ref Arrows);

		StartCoroutine (DelaySpawn (0));
	}

	IEnumerator DelaySpawn(int Index)
	{

		for (int i = 0; i < NumberToSpawn; i++) {
			GameObject newArrow = Instantiate (ArrowToSpawn[Index], transform.position, transform.rotation);

			Arrows [i + (Arrows.Length-NumberToSpawn)] = newArrow;
			ArrowIndex.Add (Index);


			yield return new WaitForSeconds (.1f);

		}
			

	}

	public void UseArrow()
	{
		if (NumberOfArrows > 0) {
			NumberOfArrows -= 1;

			GameObject destroyObject = Arrows [NumberOfArrows].gameObject;
			ArrowIndex.RemoveAt (NumberOfArrows);


			Destroy (destroyObject);
		}
	}


	public void GroupResize (int Size, ref GameObject[] Group)
	{

		GameObject[] temp = new GameObject[Size];
		for (int c = 0; c < Mathf.Min(Size, Group.Length); c++ ) {
			temp [c] = Group [c];
		}
		Group = temp;
	}


}
