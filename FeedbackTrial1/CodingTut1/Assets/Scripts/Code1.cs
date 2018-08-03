using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Code1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
        const int MaxScore = 100;
        int score = 55;
        float percent = (float)score / MaxScore;
        print("Percentage: " + percent);
		
	}
	
}
