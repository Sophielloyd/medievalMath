﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MathManager : MonoBehaviour {


	public Text QuestionText;
	public Text QuestionText_hud;
	public Text FeedbackText;

	public AudioClip CorrectSound;
	public AudioClip IncorrectSound;

	private int ProblemType;

	private int FirstNumber;
	private int SecondNumber;

	private int CorrectAnswer;

	private AnswerInput A_Input;
	private ArrowSupplier A_Supply;
	private PlayerMathStats Math_Stats;

	private AudioSource A_Source;

	// Use this for initialization
	void Start () {

		GenerateProblem ();

		A_Source = GameObject.Find ("PlayerAudio").GetComponent<AudioSource> ();

		A_Input = GameObject.FindObjectOfType<AnswerInput> ();
		A_Supply = GameObject.FindObjectOfType<ArrowSupplier> ();
		Math_Stats = GameObject.FindObjectOfType<PlayerMathStats> ();
	}


	public void GenerateProblem()
	{

		FirstNumber = Random.Range (0, 21);
		SecondNumber = Random.Range (0, 21);

		int isMultiplication = Random.Range (0, 9);

		if (isMultiplication > 0) {

			int AddorSubtract = Random.Range (0, 2);

			//choose whether or not the problem will be addition or subtraction
			if (AddorSubtract == 0) {
				ProblemType = 0;
				CorrectAnswer = FirstNumber + SecondNumber;
				QuestionText.text = FirstNumber.ToString () + " + " + SecondNumber.ToString () + " =";
				QuestionText_hud.text = FirstNumber.ToString () + " + " + SecondNumber.ToString ();

			} else if (AddorSubtract == 1) {
				ProblemType = 0;
				CorrectAnswer = FirstNumber - SecondNumber;
				QuestionText.text = FirstNumber.ToString () + " - " + SecondNumber.ToString () + " =";
				QuestionText_hud.text = FirstNumber.ToString () + " - " + SecondNumber.ToString ();
			}
		} else {
			FirstNumber = Random.Range (0, 11);
			SecondNumber = Random.Range (0, 11);

			ProblemType = 1;
			CorrectAnswer = FirstNumber * SecondNumber;
			QuestionText.text = FirstNumber.ToString () + " x " + SecondNumber.ToString () + " =";
			QuestionText_hud.text = FirstNumber.ToString () + " x " + SecondNumber.ToString ();
		}
	}


	public void CheckAnswer(int Answer)
	{
		//got the question right
		if (Answer == CorrectAnswer) {
			
			FeedbackText.text = "Correct";
			FeedbackText.color = Color.green;
			FeedbackText.gameObject.SetActive (true);
			StartCoroutine (DisplayFeedback ());

			A_Input.ClearAnswer ();

			A_Supply.CreateArrow ();

			A_Source.clip = CorrectSound;
			A_Source.Play ();

			Math_Stats.CorrectlyAnswered ();

			GenerateProblem ();
		} 
		//got the question wrong
		else {

			FeedbackText.text = "Incorrect";
			FeedbackText.color = Color.red;
			FeedbackText.gameObject.SetActive (true);
			StartCoroutine (DisplayFeedback ());

			A_Source.clip = IncorrectSound;
			A_Source.Play ();

			Math_Stats.IncorrectlyAnswered ();

			A_Input.ClearAnswer ();

		}

	}

	//wait to remove the feedback
	IEnumerator DisplayFeedback()
	{

		yield return new WaitForSeconds (2);

		FeedbackText.gameObject.SetActive (false);

	}

}
