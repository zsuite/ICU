using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class MetricManagerScript : MonoBehaviour {

	string createText = "";

	public static int blinkMetric;
	public static List<Vector3> locationMetrics = new List<Vector3>();
	public static List<Quaternion> directionMetrics = new List<Quaternion>();
	public static List<bool> somethingHappened = new List<bool>();


	void Start () {}
	void Update () {}
	
	//When the game quits we'll actually write the file.
	void OnApplicationQuit(){
		GenerateMetricsString ();
		string time = System.DateTime.UtcNow.ToString ();
		string dateTime = System.DateTime.Now.ToString (); //Get the time to tack on to the file name
		time = time.Replace ("/", "-"); //Replace slashes with dashes, because Unity thinks they are directories..
		time = time.Replace (":", "."); //Replace slashes with dashes, because Unity thinks they are directories..

		string reportFile = "ICU_Metrics_" + time + ".txt"; 
		File.WriteAllText (reportFile, createText);
		//In Editor, this will show up in the project folder root (with Library, Assets, etc.)
		//In Standalone, this will show up in the same directory as your executable
	}

	void GenerateMetricsString(){
		List<string> locationStrings = new List<string>();
		List<string> directionStrings = new List<string>();
		List<string> somethingHappenedStrings = new List<string>();

		createText = 
			"Number of times player blinked and nothing happened: " + blinkMetric + " \r\n" +
				"Their location and direction: ";
		for(int i = 0; i < locationMetrics.Count; i++){
			locationStrings.Add(locationMetrics[i].ToString("F3"));
			directionStrings.Add (directionMetrics[i].ToString ("F3"));

			somethingHappenedStrings.Add (somethingHappened[i].ToString());
			createText+= "\r\n" + locationStrings[i] + "\r\n"+ directionStrings[i] + "\r\n" + somethingHappenedStrings[i];

		}

	}

	//Add to your set metrics from other classes whenever you want
	public static void AddBlinkNumber(int amtToAdd){
		blinkMetric += amtToAdd;
	}
	public static void AddPosition(Vector3 amtToAdd){
		locationMetrics.Add(amtToAdd);
	}
	public static void AddDirection(Quaternion amtToAdd){
		directionMetrics.Add(amtToAdd);
	}
	public static void DidSomethingHappen(bool boolToAdd){
		somethingHappened.Add(boolToAdd);
	}
}
