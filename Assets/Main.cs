using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
	public GameObject uiPrefab_Goal;
	public GridLayoutGroup uiHierarchy_GoalBankPanel;
	public List<Character> characterRoster = new List<Character> {
		new Character() {
			characterName = "Alex",
			relationships = new List<Relationship> { new Relationship("Becky", 0.8F, 0.7F, "married") },
			attributes = new List<Attribute> { 
				new Attribute("{0} is married to {1}", "Alex", new [] {"Becky"}),
				new Attribute("{0} is attracted to {1}", "Alex", new [] {"Crystal"}), 
			},
			goals = new List<Goal> { potentialGoalsForAlex[0], potentialGoalsForAlex[1] },
		}
	};
	public static List<Goal> potentialGoalsForAlex = new List<Goal> {
		new Goal ("make out with {0}", "Crystal", DesireColor.Yellow),
		new Goal ("profess love to {0}", "Becky", DesireColor.Cyan),
		new Goal ("talk to {0} about {1}", "Crystal", "Drugs", DesireColor.Magenta),
		new Goal ("reminisce with {0} about {1}", "Becky", "that time you did it in the apple tree", DesireColor.Green),
		new Goal ("apologize to {0} for {1}", "Becky", "all the sodomy", DesireColor.Orange),
		new Goal ("give a great gift to {0}", "Becky", DesireColor.Cyan),
		new Goal ("challenge {0} to a dance-off", "Crystal", DesireColor.Red),
		new Goal ("give a great gift to {0}", "Becky", DesireColor.Cyan),
		new Goal ("have a threesome with {0} and {1}", "Becky", "Crystal", DesireColor.Blue),
	};
	
	public void Start ()
	{
		GenerateGoalPool ();
		PopulateGoalPanel ();
	}

	public void GenerateGoalPool () {
		//potentialGoalsForAlex.Shuffle ();
	}

	public void PopulateGoalPanel ()
	{
		Slot[] slots = uiHierarchy_GoalBankPanel.GetComponentsInChildren<Slot> ();
		for (int i = 0; i < slots.Length; i++) {
			Slot slot = slots [i];
			GameObject uiGoal = Instantiate<GameObject> (uiPrefab_Goal);
			uiGoal.transform.SetParent (slot.gameObject.transform);
			uiGoal.transform.localPosition = Vector3.zero;
			MonoGoal monoGoal = uiGoal.GetComponent<MonoGoal> ();

			//Select and remove goal at random
			var randomIndex = Random.Range(0, potentialGoalsForAlex.Count);
			monoGoal.goal = potentialGoalsForAlex[randomIndex];
			potentialGoalsForAlex.RemoveAt(randomIndex);

			uiGoal.GetComponentInChildren<Text> ().text = monoGoal.goal.DisplayText;
			uiGoal.GetComponent<DragHandler> ().Init ();
			uiGoal.GetComponent<Image>().color = monoGoal.goal.desireColor.ToColor();
		}
	}
}

	#region Model
public class World
{
	
	public List<Fact> stateOfAffairs;
	public List<Location> revealedLocations;
	public List<Item> communityItems;
}

public abstract class Idea
{

}

public abstract class InstantiatedObject : Idea
{
	[System.NonSerialized]
	public int
		id;
}

public class Fact : Idea
{
	public virtual string subject { get; set; }

	public string[] objects;
	public string predicate;

	public Fact ()
	{
	}

	public Fact (string predicate, string subject, string[] objects)
	{
		this.subject = subject;
		this.predicate = predicate;
		this.objects = objects;
	}
}

public class Attribute : Fact
{

	public override string subject { get { return character; } }

	public string character;

	public Attribute (string predicate, string subject, string[] objects)
	{
		this.predicate = predicate;
		this.objects = objects;
		this.character = subject;
	}

}

[System.Serializable]
public class Goal : Idea
{
	public string DisplayText {
		get {
			return Utilities.UppercaseFirst( string.Format (activity, target, instrument));
		}
	}

	public string target;
	public string activity;
	public string instrument;
	public DesireColor desireColor;

	public Goal (string activity, string target, DesireColor desireColor)
	{
		this.activity = activity;
		this.target = target;
		this.desireColor = desireColor;
	}
		
	public Goal (string activity, string target, string instrument, DesireColor desireColor)
	{
		this.activity = activity;
		this.target = target;
		this.instrument = instrument;
		this.desireColor = desireColor;
	}
}

public class Belief : Idea
{
	public List<string> characters;
	public string predicate;
}

public class Activity : Idea
{ 
	public string activityName;

	public Activity (string name)
	{
		this.activityName = name;
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
	public List<Attribute> attributes;
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
public class GameData
{
//		public List<Goal> goalBank = new List<Goal> {
//			new Goal()
//		};
	public List<Activity> activityBank = new List<Activity> {
			new Activity ("make out with"),
			new Activity ("chat about"),
		};


}

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

//	public static void Shuffle<T>(this IList<T> list)
//	{
//		RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
//		int n = list.Count;
//		while (n > 1)
//		{
//			byte[] box = new byte[1];
//			do provider.GetBytes(box);
//			while (!(box[0] < n * (Byte.MaxValue / n)));
//			int k = (box[0] % n);
//			n--;
//			T value = list[k];
//			list[k] = list[n];
//			list[n] = value;
//		}
//	}
}
