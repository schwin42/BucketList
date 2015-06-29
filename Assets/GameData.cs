using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

public class GameData : MonoBehaviour
{

	private static GameData _instance;

	public static GameData Instance {
		get {
			if (_instance == null) {
				_instance = FindObjectOfType<GameData> ();
			}
			return _instance;
		}
	}

	public List<Idea> masterIdeaRecord;
	private const string SOURCE_FILENAME = "/GameData/BucketList Master - Ideas.tsv";

	public void Start() {
		masterIdeaRecord = LoadIdeas ();
	}

	public List<Idea> LoadIdeas ()
	{
		string fullFilename = Application.dataPath + SOURCE_FILENAME;
		Debug.Log ("Loading data from" + fullFilename);
		string[] dataLines = File.ReadAllLines (fullFilename);
		Dictionary<int, string> fieldLookup = new Dictionary<int, string> ();
		
		List<Idea> outputIdeas = new List<Idea> ();
		for (int i = 0; i < dataLines.Length; i++) {
			if (i == 0) {
				string [] fieldStrings = dataLines [0].Split ('\t');
				
				for (int j = 0; j < fieldStrings.Length; j++) {
					fieldLookup.Add (j, fieldStrings [j]);
				}
			} else {
//				Idea record = new Idea (); 
				string[] recordStrings = dataLines [i].Split ('\t'); //
				Dictionary<string, string> recordFields = new Dictionary<string, string>();
				for (int j = 0; j < fieldLookup.Count; j++) {
					if(j < recordStrings.Length) {
						recordFields.Add(fieldLookup[j], recordStrings[j]);
					}
					
				}
				outputIdeas.Add (InstantiateIdea(recordFields));
//				AttributeData.Add (recordAttribute);
			}
			
		}
		return outputIdeas;
	}

	Idea InstantiateIdea (Dictionary<string, string> fields)
	{
		Idea idea = new Idea ();
//		switch (fields ["Type"]) {
//		case "Activity":
//			idea = new Activity();
//			break;
//		case "Interest":
//			idea = new Interest();
//			break;
//		case "Disposition":
//			idea = new Disposition();
//			break;
//		case "Goal":
//			idea = new Goal();
//			break;
//		case "Past Event":
//			idea = new PastEvent();
//			break;
//		default:
//			Debug.LogError("Invalid type: " + fields["Type"]);
//			break;
		foreach (KeyValuePair<string, string> pair in fields) {
			switch(pair.Key) {
			case "Kind":
				idea.type_KindId = pair.Value;
				break;
			case "Name":
				idea.type_Name = pair.Value;
				break;
			case "Objects":
				idea.type_Objects = Regex.Split (pair.Value, ", ");
				break;
			case "Desire Color":
				if(!string.IsNullOrEmpty(pair.Value)) {
					idea.type_DesireColor = (DesireColor)Enum.Parse(typeof(DesireColor), pair.Value);
				}
				break;
			case "Description":
				idea.type_Description = pair.Value;
				break;
			}

		}
		return idea;
	}
}