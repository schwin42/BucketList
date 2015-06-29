using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
	#region Data
	public List<Character> characterRoster = new List<Character> {
		new Character() {
			characterName = "Alex",
			relationships = new List<Relationship> { new Relationship("Becky", 0.8F, 0.7F, "married") },
			attributes = new List<CharacterAttribute> { 
//				new CharacterAttribute("{0} is married to {1}", "Alex", new [] {"Becky"}),
//				new CharacterAttribute("{0} is attracted to {1}", "Alex", new [] {"Crystal"}), 
			},
		}
	};
	public List<Goal> potentialGoalsForAlex = new List<Goal> {
//		new Goal ("Spend last moments with Crystal", "Crystal", DesireColor.Cyan),
//		new Goal ("have sex with {0}", "Crystal", DesireColor.Yellow),
//		new Goal ("profess love to {0}", "Becky", DesireColor.Cyan),
//		new Goal ("talk to {0} about {1}", "Crystal", "Drugs", DesireColor.Magenta),
//		new Goal ("reminisce with {0} about {1}", "Becky", "that time you did it in the apple tree", DesireColor.Green),
//		new Goal ("apologize to {0} for {1}", "Becky", "all the sodomy", DesireColor.Orange),
//		new Goal ("give a great gift to {0}", "Becky", DesireColor.Cyan),
//		new Goal ("challenge {0} to a dance-off", "Crystal", DesireColor.Red),
//		new Goal ("have a threesome with {0} and {1}", "Becky", "Crystal", DesireColor.Blue),
	};
	#endregion

	#region Execution
	private Dictionary<int, Idea> _ideaGenesis;
	public Dictionary<int, Idea> ideaGenesis { //Master lookup of all ideas for this runtime
		get {
			return _ideaGenesis;
		}
	} 

	public void Start ()
	{
		GenerateGoalPool ();

		UiController.instance.PopulateGoalPanel (potentialGoalsForAlex);
	}

	public void GenerateGoalPool () {
		//potentialGoalsForAlex.Shuffle ();
	}
	#endregion
	public void RegisterIdea(Idea idea) {
		idea.token_Id = _ideaGenesis.Count;
		_ideaGenesis.Add (idea.token_Id, idea);
	}
}

	#region Model
public class World
{
	
	public List<Fact> stateOfAffairs;
	public List<Location> revealedLocations;
	public List<Item> communityItems;
}

[System.Serializable] public class Idea
{
	public int type_Id;
	public int token_Id;
	public string[] type_Objects;
	public List<string> token_Objects;

	public string type_KindId;
	public string type_Name;
	public DesireColor type_DesireColor;
	public string type_Description;

	public Idea () { }
}

public class Interest : Idea {

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
public class Goal : CharacterAttribute
{
	public string DisplayText {
		get {
			return Utilities.UppercaseFirst( string.Format (type_Name, tokenObjects));
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
	public Activity() { }
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
	public List<CharacterAttribute> attributes;
	public List<Belief> beliefs;
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
//public class GameData
//{
////		public List<Goal> goalBank = new List<Goal> {
////			new Goal()
////		};
//	public List<Activity> activityBank = new List<Activity> {
//			new Activity ("make out with"),
//			new Activity ("chat about"),
//		};
//
//
//}

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

	public static Color ToColor (this DesireColor desireColor) {
		return Appearance.instance.inspector_reasonColors[(int)desireColor];
	}
}
