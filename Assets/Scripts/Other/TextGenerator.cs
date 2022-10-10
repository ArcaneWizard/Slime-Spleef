using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextGenerator
{
    public static string GenerateAIName()
    {
        bool includeAdj = UnityEngine.Random.Range(0, 1) < 0.95;
        bool includeSuffix = UnityEngine.Random.Range(0, 1) < 0.15;
        bool capitalizeAdj = UnityEngine.Random.Range(0, 1) < 0.5;
        bool capitalizeName = UnityEngine.Random.Range(0, 1) < 0.5;
        bool capitalizeSuffix = UnityEngine.Random.Range(0, 1) < 0.5;
        bool includeNumber = UnityEngine.Random.Range(0, 1) < 0.8;

        string[] adjectives = { "super", "epic", "ultra", "gamer", "red", "green", "greedy", "big", "small", "blue", "!",
            "purple", "yellow", "orange", "flacid", "strong", "yeeter", "massive", "mini", "bad", "beefy", "magnetic", "_Xx__",
            "vegetarian", "bored", "raging", "cyan", "magenta", "number1", "frizzy", "ignorant", "pretzel", "bean", "__xX_", "*", ":", ";"};
        string[] names = { "james", "bob", "mary", "patricia", "john", "johnny", "jennifer", "bopzapper", "gazzelle", "llamascreamer", 
            "pizzito", "contraband", "raylay", "santamopper", "funatic", "cornelius", "gyarados", "umbreon", "panda:)", "gamer", "_",
            "hoarder", "dave", "david", "icecream", "magnet", "(*_*)", "(^_^)", "(^-^)", "(*-*)", "juan", "corn", "brian"};
        string[] suffixes = { "_Xx__", ":)", ";)", "!!!", "!", "()()()", "?", "(*_*)", "(^_^)", "(^-^)", "(*-*)", "theGreat", "thegreat", "theKing", 
            "theking", "theQueen", "thequeen", "(:", "(;", "__xX_", "*", ":", ";", "--", "__", "++", "==", "?", "??", "<>", "</>", "><", "<->" };
        int number;

        string adjective = includeAdj ? pickRandom(adjectives) : "";
        string suffix = includeSuffix ? pickRandom(suffixes) : "";
        string name = pickRandom(names);

        if (includeAdj && capitalizeAdj)
            adjective = capitalize(adjective);

        if (capitalizeName)
            name = capitalize(name);

        if (includeSuffix && capitalizeSuffix)
            suffix = capitalize(suffix);

        if (includeNumber)
        {
            number = (int) UnityEngine.Random.Range(0, 100001);
            return adjective + name + suffix + number.ToString();
        }

        return adjective + name + suffix;
    }

    public static string GenerateFunFact()
    {
        string[] funFacts = {
            "Blue super food is worth 5 times as much as normal food.",
            "Holding shift or space to slide lets you move faster.",
            "Puddles you make while sliding disappear faster than ones you make while jumping.",
            "Koalas love eucalpytus trees.",
            "The makers of this game are major nerds.",
            "Facts are significantly less fun when you're writing them.",
            "Running into old puddles you made kills you, but newer puddles have a delay.",
            "The kill delay only exists for puddles made by you, other slime puddles are still deadly.",
            "Score is based on kills AND food eaten.",
            "Your size scales with your energy, but your max size increases by eating food.",
            "Click to throw pellets of yourself and make mini-puddles at their landing point.",
            "It took several hours to get the slime pellets to move correctly.",
            "There's a secret hidden in the game, can you find it?",
            "The design of the food takes inspiration from the game Duck Life.",
            "Duck Life 4 was made in Unity."
        };

        return "Fun fact: " + pickRandom(funFacts);
    }

    public static string GenerateDeathMessage(bool outOfEnergy)
    {
        if (outOfEnergy)
            return generateEnergyOutDeathMessage();
        else
            return generatePuddleFallDeathMessage();
    }

    private static string generateEnergyOutDeathMessage()
    {
        return "You died from Energy loss. Eat food to stay alive.";
    }

    private static string generatePuddleFallDeathMessage()
    {
        return "You died by falling in a Puddle. Watch out for those!";
    }

    private static string pickRandom(string[] collection)
    {
        return collection[(int)UnityEngine.Random.Range(0, collection.Length)];
    }

    private static string capitalize(string word)
    {
        return word.Substring(0, 1).ToUpper() + word.Substring(1);
    }
}

