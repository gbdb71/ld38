﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LevelSelector : MonoBehaviour {

	private int current;
	private Planet[] planets;
	private List<GameObject> poops;

	void Awake() {
		Application.targetFrameRate = 60;
	}

	// Use this for initialization
	void Start () {

		poops = new List<GameObject> ();

		// find the activated level
		int idx = 0;
		foreach(Transform child in transform) {
			if (child.gameObject.activeSelf) {
				current = idx;
			}

			idx++;
		}

		ActivateLevel (SaveManager.Instance.Level);
		ActivatePlanets ();
	}

	public void AddPoop(GameObject poop) {
		poops.Add (poop);
	}

	public void ClearPoops() {
		foreach (GameObject p in poops) {
			Destroy (p);
		}

		poops.Clear ();
	}

	void Update() {

		if (Application.isEditor) {
			if (Input.GetKeyDown (KeyCode.KeypadPlus)) {
				NextLevel ();
			}

			if (Input.GetKeyDown (KeyCode.KeypadMinus)) {
				NextLevel (-1);
			}
		}
	}

	public void ActivateLevel(int level) {
		// deactivate current level
		transform.GetChild (current).gameObject.SetActive (false);

		ClearPoops ();

		current = level;

		// activate next level
		transform.GetChild (current).gameObject.SetActive (true);

		ActivatePlanets ();
	}

	public bool NextLevel(int dir = 1) {

		bool looped = false;

		if (current + dir >= transform.childCount) {
			SaveManager.Instance.UnlockLevel (0);
			SceneManager.LoadScene("End");
			return true;
		}

		// deactivate current level
		transform.GetChild (current).gameObject.SetActive (false);

		ClearPoops ();

		current += dir;

		if (current < 0) {
			current = transform.childCount - 1;
		}

		SaveManager.Instance.UnlockLevel (current);

		// activate next level
		transform.GetChild (current).gameObject.SetActive (true);

		ActivatePlanets ();

		return looped;
	}

	void ActivatePlanets() {
		planets = transform.GetChild (current).gameObject.GetComponentsInChildren<Planet> ();
//		Debug.Log (planets.Length + " planets found");
	}

	public Vector2 ClosestPlanet(Vector2 pos) {

		float distance = 9999f;
		Planet p = null;

		for (int i = 0; i < planets.Length; i++) {
			float d = (pos - (Vector2)planets [i].transform.position).magnitude / planets[i].Mass();

			if (d < distance) {
				p = planets [i];
				distance = d;
			}
		}

//		Debug.DrawLine (pos, p.transform.position, Color.red, 0.5f);

		return p.transform.position;
	}
}
