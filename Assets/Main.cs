using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main
{

	public class Goal
	{
		public string target;
		public string activity;
		public string idea;

		public Goal (string activity, string target) {
			this.activity = activity;
			this.target = target;
		}

		public Goal (string activity, string target, string idea) {
			this.activity = activity;
			this.target = target;
			this.idea = idea;
		}
	}

	public abstract class Idea
	{

	}

	public abstract class InstantiatedObject : Idea
	{
		public int id;
	}

	public class Fact : Idea {
		public List<string> characters;
		public string predicate;

		public Fact (string predicate, params string[] characters) {

		}
	}

	public class Belief : Idea {
		public List<string> characters;
		public string predicate;
	}

	public class Activity : Idea
	{ 
		public string name;

		public Activity (string name)
		{
			this.name = name;
		}
	}

	public class Character : InstantiatedObject
	{
		public string name;
		public List<Goal> goals;
		public List<Relationship> relationships;
		public List<Item> inventory;
		public Dictionary<string, float> stats;
		public List<string> traits;
		public List<Fact> factsAboutSelf;
		public List<Belief> beliefs;
	}

	public class Item : InstantiatedObject
	{
		public Character owner;
	}
	djflsk
	public class Relationship
	{
		public string characterA;
		public string characterB;
		public float depth; //-1 to 1
		public float quality; //-1 to 1

		public Relationship (string characterA, string characterB, float depth, float quality) {
			this.characterA = characterA;
			this.characterB = characterB;
			this.depth = depth;
			this.quality = quality;
		}
	}

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

	public class Execution {
		public List<Character> characterRoster = new List<Character> {
			new Character() {
				name = "Alex",
				relationships = new List<Relationship> { new Relationship("Alex", "Becky", 0.8F, 0.7F) },
				factsAboutSelf = new List<Fact> { 
					new Fact("{0} is married to {1}", "Alex", "Becky"),
					new Fact("{0} is attracted to {1}", "Alex", "Crystal"), 
				},
				goals = new List<Goal> { potentialGoalsForAlex[0], potentialGoalsForAlex[1] },
			}
		};

		public static List<Goal> potentialGoalsForAlex = new List<Goal> {
			new Goal ("make out with {0}", "Crystal"),
			new Goal ("profess love to {0}", "Becky"),
			new Goal ("talk to {0} about {1}", "Crystal", "Drugs"),
			new Goal ("reminisce with {0} about {1}", "Becky", "that time you did it in the apple tree"),
			new Goal ("apologize to {0} for {1}", "Becky", "all the sodomy")
		};
	}
}