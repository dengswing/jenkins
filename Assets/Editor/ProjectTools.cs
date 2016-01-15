using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Text.RegularExpressions;
using System.IO;
using System;

public class ProjectTools
{
    [MenuItem("Tools/Version")]
    public static string GetBGitVersion()
    {
        System.Diagnostics.Process svnVC = new System.Diagnostics.Process();
        svnVC.StartInfo.UseShellExecute = false;
        svnVC.StartInfo.RedirectStandardOutput = true;
        svnVC.StartInfo.FileName = "/usr/bin/git";
        svnVC.StartInfo.Arguments = "rev-list --all";
        svnVC.Start();
        string output = svnVC.StandardOutput.ReadToEnd();
        int changeline = output.Split('\n').Length;
        svnVC.WaitForExit();
        Debug.Log(output);
        return "0.0.4." + changeline.ToString();
    }

    public static void UpdateVersionFile(string version)
    {
        try
        {
            string path = Application.dataPath + "/Resources/Version/";

            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }

            FileStream fs = new FileStream(Application.dataPath + "/Resources/Version/version.txt", FileMode.Create);
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(version);
            fs.Write(bytes, 0, bytes.Length);
            fs.Flush();
            fs.Close();
        }
        catch (Exception ex)
        {
            Debug.LogError("ex is:" + ex.ToString());
        }

        AssetDatabase.Refresh();

    }
}
