using System.IO;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class Startup
{
    static string pathToSteamVR = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\SteamVR";
    static string tempPathToSteamVR =
        "C:\\Program Files (x86)\\Steam\\steamapps\\common\\SteamVR_TEMP";

    static Startup()
    {
        if (!SessionState.GetBool("FirstInitDone", false))
        {
            if (Directory.Exists(pathToSteamVR))
            {
                Directory.Move(pathToSteamVR, tempPathToSteamVR);
            }
            EditorApplication.quitting += Quit;
            SessionState.SetBool("FirstInitDone", true);
        }
    }

    static void Quit()
    {
        if (Directory.Exists(tempPathToSteamVR))
        {
            Directory.Move(tempPathToSteamVR, pathToSteamVR);
        }
    }
}
