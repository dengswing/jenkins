using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System;

class ProjectBuild : Editor
{
    //在这里找出你当前工程所有的场景文件，假设你只想把部分的scene文件打包 那么这里可以写你的条件判断 总之返回一个字符串数组。
    static string[] GetBuildScenes()
    {
        List<string> names = new List<string>();

        foreach (EditorBuildSettingsScene e in EditorBuildSettings.scenes)
        {
            if (e == null)
                continue;
            if (e.enabled)
                names.Add(e.path);
        }
        return names.ToArray();
    }

    //获取命令行中的参数，匹配出project-后字符串
    static string GetProjectSuffix()
    {
        foreach (string arg in System.Environment.GetCommandLineArgs())
        {
            if (arg.StartsWith("project"))
            {
                return arg.Split("-"[0])[1];
            }
        }
        return "Android";
    }

    // execute before build
    static void PrepareBuild()
    {
      //  MovieToolMenuItems.MoveMovie();
     //   ResourceToolMenuItems.GenResourceConfig();
        //MultiLanguageTool.GenAllConfig();   //暂时取消 BY LANE
    }

    //shell脚本直接调用这个静态方法
    [MenuItem("AutoBuild/BuildForIPhone")]
    static void BuildForIPhone()
    {
        // prepare for build
        PrepareBuild();

        //打包之前先设置一下 预定义标签， 我建议大家最好 做一些  91 同步推 快用 PP助手一类的标签。 这样在代码中可以灵活的开启 或者关闭 一些代码。
        //因为 这里我是承接 上一篇文章， 我就以sharesdk做例子 ，这样方便大家学习 ，
        // PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iPhone, "USE_SHARE");
        //这里就是构建xcode工程的核心方法了， 
        //参数1 需要打包的所有场景
        //参数2 需要打包的名子， 这里取到的就是 shell传进来的字符串 91
        //参数3 打包平台
        BuildPipeline.BuildPlayer(GetBuildScenes(), "IOS", BuildTarget.iOS, BuildOptions.None);
     //   MovieToolMenuItems.RestMovieClips();

    }

    [MenuItem("AutoBuild/BuildForAndroid")]
    static void BuildForAndroid()
    {
        Debug.Log("BuildForAndroid");
        // prepare for build
        PrepareBuild();
        Debug.Log("PrepareBuild ");
        // Function.DeleteFolder(Application.dataPath+"/Plugins/Android");

        // if(Function.projectName == "91")
        // {
        // 	Function.CopyDirectory(Application.dataPath+"/91",Application.dataPath+"/Plugins/Android");
        // 	PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, "USE_SHARE");
        // }

        // string version = ProjectTools.GetBGitVersion();

        string version = "1.0";
        ProjectTools.UpdateVersionFile(version);

        Debug.Log("set version");
        string path = Application.dataPath + "/../Export/";

        if (!System.IO.Directory.Exists(path))
        {
            System.IO.Directory.CreateDirectory(path);
        }
        Debug.Log("create file");

        path = path + GetProjectSuffix() + version + ".apk";

        Debug.Log("create apk");

        EditorPrefs.SetString("AndroidSdkRoot", "C:/Users/dsw/AppData/Local/Android/sdk");

        Debug.Log("create AndroidSdkRoot");
        BuildPipeline.BuildPlayer(GetBuildScenes(), path, BuildTarget.Android, BuildOptions.None);

        Debug.Log("create finish");
        //    MovieToolMenuItems.RestMovieClips();

    }
}
