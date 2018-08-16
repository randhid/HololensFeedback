using UnityEngine;
using System.Collections.Generic;

public class WMG_X_Tutorial_1 : MonoBehaviour {

	public GameObject emptyGraphPrefab;

	public WMG_Axis_Graph graph;

	public WMG_Series series1;

	public List<Vector2> series1Data;


	// Use this for initialization
	void Start () {
		GameObject graphGO = GameObject.Instantiate(emptyGraphPrefab);
		graphGO.transform.SetParent(this.transform, false);
		graph = graphGO.GetComponent<WMG_Axis_Graph>();

		series1 = graph.addSeries();
		graph.xAxis.AxisMaxValue = 20;
	    series1.pointValues.SetList(series1Data);
		}
	}




