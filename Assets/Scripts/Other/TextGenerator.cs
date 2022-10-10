using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextGenerator
{
    public static string GenerateAIName()
    {
        bool includeAdj = UnityEngine.Random.Range(0f, 1f) < 0.95;
        bool includeSuffix = UnityEngine.Random.Range(0f, 1f) < 0.15;
        bool capitalizeAdj = UnityEngine.Random.Range(0f, 1f) < 0.5;
        bool capitalizeName = UnityEngine.Random.Range(0f, 1f) < 0.5;
        bool capitalizeSuffix = UnityEngine.Random.Range(0f, 1f) < 0.5;
        bool includeNumber = UnityEngine.Random.Range(0f, 1f) < 0.21;

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

        string space1 = (UnityEngine.Random.Range(0, 2) == 1) ? " " : "";
        string space2 = (UnityEngine.Random.Range(0, 2) == 1) ? " " : "";

        if (includeNumber)
        {
            number = UnityEngine.Random.Range(0, 100);
            return adjective + space1 + name + space2 + suffix + number.ToString();
        }

        return adjective + name + suffix;
    }

    public static string GenerateFunFact()
    {
        string[] funFacts = {
            "Blue super food is worth 5 times as much as normal food.",
            "Holding shift or space to slide lets you move faster.",
            "Sliding is slightly faster than bouncing.",
            "Koalas love eucalpytus trees... said someone we know.",
            "The makers of this game pulled an all nighter to finish it.",
            "You can raise your score by eating food or smaller slimes.",
            "Your size will deplete as you lose energy.",
            "Click to throw pellets of yourself and make mini-acid puddles.",
            "It took several hours to get the slime pellets to throw correctly.",
            "There's a secret hidden in the game, can you find it?",
            "The design of the food takes inspiration from the game Duck Life.",
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
        return "You died from energy loss. Eat more food and avoid trudging through slime.";
    }

    private static string generatePuddleFallDeathMessage()
    {
        return "You were eaten by a bigger slime.";
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

