using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class StaticArrays
{
    public static System.Random random = null;

    public static Vector3[] initialPositions = new Vector3[3] { new Vector3(-8.25f, 4f, -0.5f), new Vector3(-8.25f, 1f, -0.5f), new Vector3(-8.25f, -2f, -0.5f) };

    //public static float leftX = 0f;
    public static float aspect = 0f;
    /*
	public static string[] objectNames = new string[]{"arrow","circle","cone","cube","crescent","cross","cube","cuboid",
										"cylinder","egg","heart","oval","parallelogram","pentagon","prism","pyramid",
										"rectangle","rhombus","sphere","square","star","triangle"};
										*/

    //public static string[] objectNames_2D = new string[]{"arrow","circle","crescent","cross","heart","oval","parallelogram","pentagon","rectangle","rhombus","square","star","triangle"};

    //public static string[] objectNames_3D = new string[]{"cone","cube","cuboid","cylinder","prism","pyramid","sphere"};

    public static string[] colorNames = new string[] { "black", "blue", "brown", "green", "grey", "orange", "pink", "purple", "red", /* "violet",*/ "white", "yellow" };

    public static Dictionary<string, string[]> objectNamesDictionary = new Dictionary<string, string[]>()
    {
        { "black", new string[] { "blackberry", "crow", "orca", "tyre"} },
        { "blue", new string[] { "balloon", "blueberry", "bluebox", "mail", "shark", "twitter", "water"} },
        { "brown", new string[] { "bear", "cake", "chocolate", "coconut", "kiwi", "potato", "wood"} },
        { "green", new string[] { "avocado", "balloon", "candy", "capsicum", "cucumber", /*"flight",*/ "greenapple", "greencar", "papaya", "peas", "tortoise"} },
        { "grey", new string[] { "dolphin", "elephant", "koala", "raccoon", "walrus"} },
        { "orange", new string[] { "carrot", "fish", "fox", "giraffe", "orangefruit", "pumpkin"} },
        { "pink", new string[] { "balloon", "donut", "flamingo", "hippo", "octopus", "onion", "pig", "pinkbox", "pinkrose", "scooter"} },
        { "purple", new string[] { "eggplant", "fig", "mangosteen", "pillow", "plum", "purplecandy" } },
        { "red", new string[] { "apple", "capsicum", "car", "cherry",/* "chilli",*/ "crab", "cricketball", "macaw", "redrose", "strawberry", "tomato", "tram"} },
        //{ "violet", new string[] { "arrow", "circle", "crescent", "cross", "heart", "oval", "parallelogram", "pentagon", "rectangle", "rhombus", "square", "star", "triangle" } },
        { "white", new string[] { "ball", "egg", "golfball", "milk", "tooth", "whiterose" } },
        { "yellow", new string[] { "balloon", "banana", "bus", "capsicum", "cheese", "duck", "lemon", "sun", "tin", "yellowrose"} }

    };

    public static string[] shapeNames = new string[] { "s", "t", "c" };

    public static string[] objectNames = colorNames;
    public static Dictionary<string, string[]> objectDictionary = objectNamesDictionary;


    public static string matchingType = "colors";


    public static List<string> createdObjectsList;
	public static int currentIndex = 0;

	public static string currentTarget;

	public static string currentMode;
	public static int currentModeHighScore;
	public static int currentScore;
	public static int currentTime;
	public static int currentHits;
	public static int currentMisses;


	public static string relaxedModeHighScoreKey = "RelaxedModeHighScore";
	public static string timedModeHighScoreKey = "TimedModeHighScore";

}
