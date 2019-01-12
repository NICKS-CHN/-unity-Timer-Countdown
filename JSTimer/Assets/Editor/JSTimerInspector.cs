using UnityEditor;
using UnityEngine;

[CustomEditor(typeof (JSTimer))]
public class JSTimerInspector : Editor
{
    JSTimer _mJsTimer;
    private bool _coolDownToggle = true;
    private bool _timerToggle = true;

    public override void OnInspectorGUI()
    {
        _mJsTimer = target as JSTimer;
        if (_mJsTimer == null)
            return;

        var coolDownTaskList = _mJsTimer.CdTasks;
        GUILayout.Label("----------倒计时---------");
        _coolDownToggle = EditorGUILayout.Foldout(_coolDownToggle, string.Format("CoolDown:{0}", coolDownTaskList.Count));
        GUILayout.Label("-------------------------");
        if (_coolDownToggle)
        {
            for (int i = 0; i < coolDownTaskList.Count; i++)
            {
                DrawCoolDownTask(coolDownTaskList[i]);
            }
        }

        var timerTaskList = _mJsTimer.TimeTasks;
        GUILayout.Label("----------计时器---------");
        _timerToggle = EditorGUILayout.Foldout(_timerToggle, string.Format("CoolDown:{0}", coolDownTaskList.Count));
        GUILayout.Label("-------------------------");
        if (_timerToggle)
        {
            for (int i = 0; i < timerTaskList.Count; i++)
            {
                DrawTimerTask(timerTaskList[i]);
            }
        }

        this.Repaint();
    }

    void DrawCoolDownTask(JSTimer.CDTask cdTask)
    {
        GUILayout.BeginVertical("GroupBox");
        {
            GUILayout.Label("name:"+cdTask.taskName);
            if (cdTask.isVaild)
            {
                GUILayout.Label(string.Format("剩余时间:{0}/{1}", cdTask.remainTime, cdTask.totalTime));
                GUILayout.Label("更新频率:"+cdTask.updateFrequence);
                GUILayout.Label("timeScale:"+cdTask.timeScale);
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Finish"))
                {
                    cdTask.remainTime = 0f;
                }
                if (GUILayout.Button("Cancel"))
                {
                   _mJsTimer.CancelCd(cdTask.taskName);
                }
                if (GUILayout.Button(cdTask.isPause?"Resume":"Pause"))
                {
                    cdTask.isPause = !cdTask.isPause;
                }
                GUILayout.EndHorizontal();
            }
            else
            {
                GUILayout.Label("已失效");
            }
            GUILayout.EndVertical();
        }
    }

    void DrawTimerTask(JSTimer.TimerTask timerTask)
    {
        GUILayout.BeginVertical("GroupBox");
        {
            GUILayout.Label("name:" + timerTask.taskName);
            if (timerTask.isVaild)
            {
                GUILayout.Label("累计时间:"+ timerTask.currentAccumulate);
                GUILayout.Label("更新频率:" + timerTask.updateFrequence);
                GUILayout.Label("timeScale:" + timerTask.timeScale);
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Cancel"))
                {
                    _mJsTimer.CancelTimer(timerTask.taskName);
                }
                if (GUILayout.Button(timerTask.isPause ? "Resume" : "Pause"))
                {
                    timerTask.isPause = !timerTask.isPause;
                }
                GUILayout.EndHorizontal();
            }
            else
            {
                GUILayout.Label("已失效");
            }
            GUILayout.EndVertical();
        }
    }
}