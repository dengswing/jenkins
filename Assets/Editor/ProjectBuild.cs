using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System;

class ProjectBuild : Editor
{
    //�������ҳ��㵱ǰ�������еĳ����ļ���������ֻ��Ѳ��ֵ�scene�ļ���� ��ô�������д��������ж� ��֮����һ���ַ������顣
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

    //��ȡ�������еĲ�����ƥ���project-���ַ���
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
        //MultiLanguageTool.GenAllConfig();   //��ʱȡ�� BY LANE
    }

    //shell�ű�ֱ�ӵ��������̬����
    [MenuItem("AutoBuild/BuildForIPhone")]
    static void BuildForIPhone()
    {
        // prepare for build
        PrepareBuild();

        //���֮ǰ������һ�� Ԥ�����ǩ�� �ҽ�������� ��һЩ  91 ͬ���� ���� PP����һ��ı�ǩ�� �����ڴ����п������Ŀ��� ���߹ر� һЩ���롣
        //��Ϊ �������ǳн� ��һƪ���£� �Ҿ���sharesdk������ ������������ѧϰ ��
        // PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iPhone, "USE_SHARE");
        //������ǹ���xcode���̵ĺ��ķ����ˣ� 
        //����1 ��Ҫ��������г���
        //����2 ��Ҫ��������ӣ� ����ȡ���ľ��� shell���������ַ��� 91
        //����3 ���ƽ̨
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
