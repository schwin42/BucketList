﻿using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

public class GameData : MonoBehaviour
{
	[System.Serializable] public class TemplateMaster
	{
		public List<Idea> ideas = new List<Idea>();

		public void RegisterIdeas(List<Idea> ideas) {
			for(int i = 0; i < ideas.Count; i++) {
				Idea idea = ideas[i];
				idea.template_Id = i;
				this.ideas.Add(idea);
			}
		}
	}
	private static GameData _instance;

	public static GameData Instance {
		get {
			if (_instance == null) {
				_instance = FindObjectOfType<GameData> ();
			}
			return _instance;
		}
	}

	private TemplateMaster _templateMaster = null;
	public TemplateMaster templateMaster {
		get {
			if(_templateMaster == null) {
				_templateMaster = new TemplateMaster ();
				_templateMaster.RegisterIdeas(LoadData<Idea> ());
			}
			return _templateMaster;
		}
	}

	private const string SOURCE_FILENAME = "/GameData/BucketList Master - Ideas.tsv";

	public List<T> LoadData<T> () where T : class, new()
	{
		string fullFilename = Application.dataPath + SOURCE_FILENAME;
		print ("Loading data from" + fullFilename);
		string[] dataLines = File.ReadAllLines (fullFilename);
		Dictionary<int, string> fieldLookup = new Dictionary<int, string> ();
		
		List<T> outputIdeas = new List<T> ();
		for (int i = 0; i < dataLines.Length; i++) {
			if (i == 0) {
				string [] fieldStrings = dataLines [0].Split ('\t');
				
				for (int j = 0; j < fieldStrings.Length; j++) {
					fieldLookup.Add (j, fieldStrings [j]);
				}
			} else {
				string[] recordStrings = dataLines [i].Split ('\t'); //
				Dictionary<string, string> recordFields = new Dictionary<string, string>();
				for (int j = 0; j < fieldLookup.Count; j++) {
					if(j < recordStrings.Length) {
						recordFields.Add(fieldLookup[j], recordStrings[j]);
					}
					
				}
				outputIdeas.Add (InstantiateData<T>(recordFields));
			}
		}
		return outputIdeas;
	}

	T InstantiateData<T> (Dictionary<string, string> fields) where T : class, new()
	{
		if (typeof(T) != typeof(Idea)) {
			Debug.LogError("Invalid type: " + typeof(T));
			return null as T;
		}
		Idea idea = new T () as Idea;
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
			if(string.IsNullOrEmpty(pair.Value)) {
				continue;
			}
			switch(pair.Key) {
			case "Kind":
				idea.template_Kind = (Kind)Enum.Parse(typeof(Kind), pair.Value);
				break;
			case "Name":
				idea.template_Text = pair.Value;
				break;
			case "Objects":
				List<Kind> objects = new List<Kind>();
				foreach(string s in	Regex.Split (pair.Value, ", ")) {
					objects.Add((Kind)Enum.Parse(typeof(Kind), s));
				}
				idea.template_Objects = objects;
				break;
			case "Desire Color":
				idea.template_DesireColor = (DesireColor)Enum.Parse(typeof(DesireColor), pair.Value);
				break;
			case "Description":
				idea.template_Description = pair.Value;
				break;
			}
		}
		return idea as T;
	}
}