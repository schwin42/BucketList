using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class Main : MonoBehaviour
{
	#region Execution


	private static Main _instance;
	public static Main instance {
		get {
			if(!_instance) {
				_instance = GameObject.FindObjectOfType<Main>();
			} 
			return _instance;
		}
		set {
			_instance = value;
		}
	}

	public List<Character> PossibleTargets {
		get {
			return new List<Character> {
				new Character("Becky"),
				new Character("Christy"),
			};
		}
	}

	public List<Idea> PossibleActivities {
		get {
//			print ("Activity count: " + GameData.Instance.templateMaster.ideas.Where(i => i.template_Kind == Kind.Activity).Count());
			return GameData.Instance.templateMaster.ideas.Where(i => i.template_Kind == Kind.Activity).ToList();
		}
	}

	public List<Idea> PossibleStyles {
		get {
			return GameData.Instance.templateMaster.ideas.Where(i => i.template_Kind == Kind.Style).ToList();
		}
	}

	public List<Idea> AvailableGoalsForAlex {
		get {
			return GenerateGoalPool();
		}
	}


	private static Dictionary<int, Idea> _ideaGenesis = new Dictionary<int, Idea> ();

	public static Dictionary<int, Idea> ideaGenesis { //Master lookup of all ideas for this runtime
		get {
			return _ideaGenesis;
		}
	}

	void Awake () {
		_instance = this;
	}

	public void Start ()
	{
		//GameData.Instance.Load ();

	}

	public List<Idea> GenerateGoalPool ()
	{

		//TODO Selection of goals by character applicability

		//Select all goals from database
		return GameData.Instance.templateMaster.ideas.Where (i => i.template_Kind == Kind.Goal).ToList();
	}
	#endregion
	public static void RegisterIdea (Idea idea)
	{
		idea.token_Id = _ideaGenesis.Count;
		_ideaGenesis.Add (idea.token_Id, idea);
	}
}

	#region Model
public enum Kind
{
	Character,
	Goal,
	Activity,
	PastEvent,
	Disposition,
	Interest,
	Film,
	Game,
	Band,
	Book,
	Style,
	Food,
	Item,
	Drug,
	Stat,
	Skill,
	RelationshipStat,
}

public class World
{
	
	public List<Fact> stateOfAffairs;
	public List<Location> revealedLocations;
	public List<Item> communityItems;
}

[System.Serializable]
public class Idea
{
	public int template_Id;
	public int token_Id;
	public List<Kind> template_Objects;
	public List<int> token_Objects;
	public Kind template_Kind;
	public string template_Text;
	public DesireColor template_DesireColor;
	public string template_Description;

	public Idea ()
	{
		Main.RegisterIdea (this);
	}
}

public class Interest : Idea
{

}

public abstract class InstantiatedObject : Idea
{
}

public class Fact : Idea
{
	public virtual string subject { get; set; }

//	public string[] objects;
//	public string predicate;

	public Fact ()
	{
	}

//	public Fact (string predicate, string subject, string[] objects)
//	{
//		this.subject = subject;
//		this.predicate = predicate;
//		this.objects = objects;
//	}
}

public class CharacterAttribute : Fact
{

//	public override string subject { get { return character; } }
//
//	public string character;
//
//	public CharacterAttribute () { }
//
//	public CharacterAttribute (string predicate, string subject, string[] objects)
//	{
//		this.predicate = predicate;
//		this.objects = objects;
//		this.character = subject;
//	}

}

[System.Serializable]
public class Goal : Idea
{
	public string DisplayText {
		get {
			return Utilities.UppercaseFirst (string.Format (template_Text, tokenObjects));
		}
	}

	public List<string> tokenObjects;
//	public string target;
//	public string activity;
//	public string instrument;
//	public DesireColor desireColor;

//	public Goal (string activity, string target, DesireColor desireColor)
//	{
//		this.activity = activity;
//		this.target = target;
//		this.desireColor = desireColor;
//	}
		
//	public Goal (string activity, string target, string instrument, DesireColor desireColor)
//	{
//		this.activity = activity;
//		this.target = target;
//		this.instrument = instrument;
//		this.desireColor = desireColor;
//	}
}

public class Belief : Idea
{
//	public List<string> characters;
//	public string predicate;
}

public class Activity : Idea
{ 
	public Activity ()
	{
	}
}

[System.Serializable]
public class Character : InstantiatedObject
{
	public string characterName;
	public List<Goal> goals;
	public List<Relationship> relationships;
	public List<Item> inventory;
	public Dictionary<string, float> stats;
	//public List<string> traits;
	public List<Idea> attributes;
	public List<Belief> beliefs;

	public Character (string name) {
		characterName = name;
	}
}

[System.Serializable]
public class Item : InstantiatedObject
{
	public string owner;
}

public class Location : InstantiatedObject
{
		
}

[System.Serializable]
public class Relationship : Idea
{
	public string targetCharacter;
	public float depth; //-1 to 1
	public float quality; //-1 to 1
	public string status;

	public Relationship (string targetCharacter, float depth, float quality, string status)
	{
		this.targetCharacter = targetCharacter;
		this.depth = depth;
		this.quality = quality;
		this.status = status;
	}
}
	#endregion


public static class Utilities
{
	public static string UppercaseFirst (string s)
	{
		// Check for empty string.
		if (string.IsNullOrEmpty (s)) {
			return string.Empty;
		}
		// Return char and concat substring.
		return char.ToUpper (s [0]) + s.Substring (1);
	}

	public static Color ToColor (this DesireColor desireColor)
	{
		return Appearance.instance.inspector_reasonColors [(int)desireColor];
	}
}
