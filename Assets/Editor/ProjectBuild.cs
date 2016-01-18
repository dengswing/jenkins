using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System;

class ProjectBuild : Editor
{
    //ÔÚÕâÀïÕÒ³öÄãµ±Ç°¹¤³ÌËùÓÐµÄ³¡¾°ÎÄ¼þ£¬¼ÙÉèÄãÖ»Ïë°Ñ²¿·ÖµÄsceneÎÄ¼þ´ò°ü ÄÇÃ´ÕâÀï¿ÉÒÔÐ´ÄãµÄÌõ¼þÅÐ¶Ï ×ÜÖ®·µ»ØÒ»¸ö×Ö·û´®Êý×é¡£
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

    //»ñÈ¡ÃüÁîÐÐÖÐµÄ²ÎÊý£¬Æ¥Åä³öproject-ºó×Ö·û´®
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
        //MultiLanguageTool.GenAllConfig();   //ÔÝÊ±È¡Ïû BY LANE
    }

    //shell½Å±¾Ö±½Óµ÷ÓÃÕâ¸ö¾²Ì¬·½·¨
    [MenuItem("AutoBuild/BuildForIPhone")]
    static void BuildForIPhone()
    {
        // prepare for build
        PrepareBuild();

        //´ò°üÖ®Ç°ÏÈÉèÖÃÒ»ÏÂ Ô¤¶¨Òå±êÇ©£¬ ÎÒ½¨Òé´ó¼Ò×îºÃ ×öÒ»Ð©  91 Í¬²½ÍÆ ¿ìÓÃ PPÖúÊÖÒ»ÀàµÄ±êÇ©¡£ ÕâÑùÔÚ´úÂëÖÐ¿ÉÒÔÁé»îµÄ¿ªÆô »òÕß¹Ø±Õ Ò»Ð©´úÂë¡£
        //ÒòÎª ÕâÀïÎÒÊÇ³Ð½Ó ÉÏÒ»ÆªÎÄÕÂ£¬ ÎÒ¾ÍÒÔsharesdk×öÀý×Ó £¬ÕâÑù·½±ã´ó¼ÒÑ§Ï° £¬
        // PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iPhone, "USE_SHARE");
        //ÕâÀï¾ÍÊÇ¹¹½¨xcode¹¤³ÌµÄºËÐÄ·½·¨ÁË£¬ 
        //²ÎÊý1 ÐèÒª´ò°üµÄËùÓÐ³¡¾°
        //²ÎÊý2 ÐèÒª´ò°üµÄÃû×Ó£¬ ÕâÀïÈ¡µ½µÄ¾ÍÊÇ shell´«½øÀ´µÄ×Ö·û´® 91
        //²ÎÊý3 ´ò°üÆ½Ì¨
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

        //EditorPrefs.SetString("AndroidSdkRoot", "C:/Users/dsw/AppData/Local/Android/sdk");

        Debug.Log("create AndroidSdkRoot");
        BuildPipeline.BuildPlayer(GetBuildScenes(), path, BuildTarget.Android, BuildOptions.None);

        Debug.Log("create finish");
        //    MovieToolMenuItems.RestMovieClips();

    }
}
