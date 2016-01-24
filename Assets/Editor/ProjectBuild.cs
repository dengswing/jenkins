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
#if UNITY_4_6_1 || UNITY_4_6_5 || UNITY_4_6_6
        BuildPipeline.BuildPlayer(GetBuildScenes(), "IOS", BuildTarget.iPhone, BuildOptions.None);
#else 
        BuildPipeline.BuildPlayer(GetBuildScenes(), "IOS", BuildTarget.iOS, BuildOptions.None);
#endif
        //   MovieToolMenuItems.RestMovieClips();

    }

    [MenuItem("AutoBuild/BuildForAndroid")]
    static void BuildForAndroid()
    {
        PrepareBuild();
        string version = "1.0";
        ProjectTools.UpdateVersionFile(version);
        Debug.Log("set version" + version);
        string path = Application.dataPath + "/../Export/";

        if (!System.IO.Directory.Exists(path))
        {
            System.IO.Directory.CreateDirectory(path);
        }

        path = path + GetProjectSuffix() + version + ".apk";

       // EditorPrefs.SetString("AndroidSdkRoot", "C:/Users/dsw/AppData/Local/Android/sdk");


        BuildPipeline.BuildPlayer(GetBuildScenes(), path, BuildTarget.Android, BuildOptions.None);
    }
}
