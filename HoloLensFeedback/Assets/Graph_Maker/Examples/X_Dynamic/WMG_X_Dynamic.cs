using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;

public class WMG_X_Dynamic : MonoBehaviour {
	public GameObject graphPrefab;
	public WMG_Axis_Graph graph;

	public bool performTests;
	public bool noTestDelay;
	
	public float testInterval;
	public float testGroupInterval = 2;

	public Ease easeType;
	public GameObject realTimePrefab;

	GameObject realTimeObj;
	float animDuration;
	WaitForSeconds waitTime;

	void Start() {
		GameObject graphGO = GameObject.Instantiate(graphPrefab) as GameObject;
		graph = graphGO.GetComponent<WMG_Axis_Graph>();

		graph.changeSpriteParent(graphGO, this.gameObject);
		graph.changeSpritePositionTo(graphGO, Vector3.zero);
		graph.graphTitleOffset = new Vector2(0, 40);
		graph.autoAnimationsDuration = testInterval - 0.1f;

		waitTime = new WaitForSeconds(testInterval);
		animDuration = testInterval - 0.1f; // have animations slightly faster than the test interval
		if (animDuration < 0) animDuration = 0;

		if (performTests) {
			StartCoroutine(startTests());
		}
	}

	void Update() {
//		if (Input.GetKeyDown(KeyCode.A)) {
//			WMG_Anim.animSize(graph.gameObject, 1, Ease.Linear, new Vector2(530, 420));
//		}
//		if (Input.GetKeyDown(KeyCode.B)) {
//			WMG_Anim.animSize(graph.gameObject, 1, Ease.Linear, new Vector2(300, 200));
//		}
	}

	IEnumerator startTests() {
		yield return new WaitForSeconds(testGroupInterval);
		graph.graphTitleString = "CALEX Feedback";
		StartCoroutine(autoAnimationTests());
		//if (!noTestDelay) yield return new WaitForSeconds(testInterval * 15);
		//yield return new WaitForSeconds(testGroupInterval);
    }

    

	IEnumerator autoAnimationTests() {
		WMG_Series s1 = graph.lineSeries[0].GetComponent<WMG_Series>();
		WMG_Series s2 = graph.lineSeries[1].GetComponent<WMG_Series>();
		List<Vector2> s1Data = s1.pointValues.list;
		List<Vector2> s2Data = s2.pointValues.list;
    //    print(s1Data);
		graph.autoAnimationsEnabled = true;
		//graph.graphType = WMG_Axis_Graph.graphTypes.line;
		/*
		if (!noTestDelay) yield return waitTime;
		graph.orientationType = WMG_Axis_Graph.orientationTypes.horizontal;
		
		if (!noTestDelay) yield return waitTime;
		graph.orientationType = WMG_Axis_Graph.orientationTypes.vertical;
		
    	if (!noTestDelay) yield return waitTime;
		graph.graphType = WMG_Axis_Graph.graphTypes.bar_side;
		
		if (!noTestDelay) yield return waitTime;
		graph.orientationType = WMG_Axis_Graph.orientationTypes.horizontal;
		
		if (!noTestDelay) yield return waitTime;
		graph.orientationType = WMG_Axis_Graph.orientationTypes.vertical;
		*/
		if (!noTestDelay) yield return waitTime;
		graph.graphType = WMG_Axis_Graph.graphTypes.line;
/*
		// change 1 value
		if (!noTestDelay) yield return waitTime;
		List<Vector2> s1Data2 = new List<Vector2>(s1Data);
		s1Data2[6] = new Vector2(s1Data2[6].x, s1Data2[6].y + 5);
		s1.pointValues.SetList(s1Data2);

		if (!noTestDelay) yield return waitTime;
		s1.pointValues.SetList(s1Data);

		// change multiple values
		if (!noTestDelay) yield return waitTime;
		s1.pointValues.SetList(WMG_Util.GenRandomY(s1Data.Count, 0, s1Data.Count-1, graph.yAxis.AxisMinValue, graph.yAxis.AxisMaxValue));

		if (!noTestDelay) yield return waitTime;
		s1.pointValues.SetList(s1Data);

		// change multiple series multiple values multiple times before animation can finish
		if (!noTestDelay) yield return waitTime;
		graph.autoAnimationsDuration = 2*testInterval - 0.1f;
		s1.pointValues.SetList(WMG_Util.GenRandomY(s1Data.Count, 0, s1Data.Count-1, graph.yAxis.AxisMinValue, graph.yAxis.AxisMaxValue));
		s2.pointValues.SetList(WMG_Util.GenRandomY(s2Data.Count, 0, s2Data.Count-1, graph.yAxis.AxisMinValue, graph.yAxis.AxisMaxValue));

		if (!noTestDelay) yield return waitTime;
		s1.pointValues.SetList(WMG_Util.GenRandomY(s1Data.Count, 0, s1Data.Count-1, graph.yAxis.AxisMinValue, graph.yAxis.AxisMaxValue));

		if (!noTestDelay) yield return waitTime;
		if (!noTestDelay) yield return waitTime;
		graph.autoAnimationsDuration = testInterval - 0.1f;
		s1.pointValues.SetList(s1Data);
		s2.pointValues.SetList(s2Data);

		if (!noTestDelay) yield return waitTime;
        */
		graph.autoAnimationsEnabled = false;
        
	}

}
