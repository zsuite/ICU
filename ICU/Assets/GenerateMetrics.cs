using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class GenerateMetrics : MonoBehaviour {
	public class MetricGizmo
	{
		public Vector3 gLocation;
		//public Vector3 gDirection;
		public Quaternion gDirection;
		public bool gSomethingHappened;
		public Color gColor;
		public MetricGizmo(Vector3 gLoc, Quaternion gDir, bool gSom)
		{
			gLocation = gLoc;
			gDirection = gDir;
			gSomethingHappened = gSom;
			gColor = gSom ? Color.green : Color.red;
		}
	}
	List<string> fileLines = new List<string>();
	List<MetricGizmo> mGizmos = new List<MetricGizmo>();
	public string textFile;
	bool doneWithMetrics = false;
	// Use this for initialization
	void Start () {
		ReadFile();
		ReadLines();
		doneWithMetrics = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void ReadFile() {
		var sr = File.OpenText(textFile + ".txt");
		fileLines = sr.ReadToEnd().Split("\n"[0]).ToList();
		sr.Close();
	}

	void ReadLines(){
		Vector3 tempLoc = Vector3.zero;
		Quaternion tempDir = Quaternion.identity;
		bool tempSom = true;
		for(int i = 2; i < fileLines.Count; i+=3){
			tempLoc = ParseVector3(fileLines[i]);
		
			tempDir = ParseQuaternion(fileLines[i+1]);

			//fileLines[i].
			tempSom = bool.Parse(fileLines[i+2]);
			mGizmos.Add(new MetricGizmo(tempLoc, tempDir, tempSom));
		}
	}

	Vector3 ParseVector3(string sourceString) { 
		string outString; 
		Vector3 outVector3;
		string[] splitString = new string[3];
		 // Trim extranious parenthesis
		outString = sourceString.Substring(1, sourceString.Length - 3);
		 // Split delimted values into an array
		splitString = outString.Split("," [0]);
		 // Build new Vector3 from array elements
		outVector3.x = float.Parse(splitString[0]);
		outVector3.y = float.Parse (splitString[1]);
		outVector3.z = float.Parse(splitString[2]);
		return outVector3;
	}
	Quaternion ParseQuaternion(string sourceString) { 
		string outString; 
		Quaternion outQuat;
		string[] splitString = new string[4];
		// Trim extranious parenthesis
		outString = sourceString.Substring(1, sourceString.Length - 3);
		// Split delimted values into an array
		splitString = outString.Split("," [0]);
		// Build new Vector3 from array elements
		outQuat.x = float.Parse(splitString[0]);
		outQuat.y = float.Parse (splitString[1]);
		outQuat.z = float.Parse(splitString[2]);
		outQuat.w = float.Parse(splitString[3]);

		return outQuat;
	}
	void OnDrawGizmosSelected() {
		if(!doneWithMetrics){
		/*ReadFile();
		ReadLines();
			doneWithMetrics = true;*/
		}
		foreach(MetricGizmo giz in mGizmos){
			
			Gizmos.color = giz.gColor;
			//Gizmos.matrix = Matrix4x4.TRS( giz.gLocation, giz.gDirection, Vector3.one );
			//Gizmos.DrawFrustum(giz.gLocation, 60, 10, .03f, 1);

			Vector3 direction = giz.gDirection * Vector3.forward;
			Gizmos.DrawWireSphere(giz.gLocation,.1f);
			Gizmos.DrawRay(giz.gLocation, direction);
		}
		
	}

}
