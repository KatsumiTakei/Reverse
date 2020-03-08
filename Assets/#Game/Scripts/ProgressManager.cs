using System.Collections;
using UnityDLL;
using UnityEngine;
using TMPro;

public enum eSceneState
{
    Title,
    Play,
    Result,
}

public abstract class SceneBase : MonoBehaviour
{
    public SceneBase()
    {
    }

    public abstract void Open();
    public abstract void Close();
}

public class ProgressManager : SingletonMonoBehaviour<ProgressManager>
{
    [SerializeField]
    TextMeshPro textMesh = null;
    
    [SerializeField]
    TitleScene titleScene = null;
    
    [SerializeField]
    PlayScene playScene = null;

    [SerializeField]
    ResultScene resultScene = null;

    SceneBase currentScene = null;

    const int AliveMax = 30;
    int aliveCnt = AliveMax;
    int eraseCnt = 0;
    int releaseCnt = 0;

    public int GetReleaseCnt()
    {
        return releaseCnt;
    }

    private void Start()
    {
        currentScene = titleScene;
        titleScene.gameObject.SetActive(true);
        playScene.enabled = false;
        resultScene.gameObject.SetActive(false);

        Reset();
    }

    void OnEnable()
    {
        EventManager.OnCreateMob += OnCreateMob;
        EventManager.OnDeadMob += OnDeadMob;
        EventManager.OnEraseMob += OnEraseMob;
        EventManager.OnIsCreateLimit += OnIsCreateLimit;
    }

    void OnDisable()
    {
        EventManager.OnCreateMob -= OnCreateMob;
        EventManager.OnDeadMob -= OnDeadMob;
        EventManager.OnEraseMob -= OnEraseMob;
        EventManager.OnIsCreateLimit -= OnIsCreateLimit;
    }

    public void Reset()
    {
        aliveCnt = AliveMax;
        releaseCnt = 0;
        eraseCnt = 0;
        UpdateHUD();

        EventManager.BroadcastResetSpd();
    }

    void UpdateHUD()
    {
        textMesh.text = $"残り:{aliveCnt} <color=green>解放:{releaseCnt}</color> <color=red>死亡:{eraseCnt}</color>";
    }

    void OnCreateMob()
    {
        aliveCnt--;
        UpdateHUD();
    }
    void OnDeadMob()
    {
        releaseCnt++;
        UpdateHUD();
    }
    void OnEraseMob()
    {
        eraseCnt++;
        UpdateHUD();
    }

    bool OnIsCreateLimit()
    {
        return (aliveCnt <= 0);
    }

    private void Update()
    {
        if(MobManager.Instance.GetRemainingMobs() <= 0 
            && OnIsCreateLimit()
            && currentScene != resultScene)
        {
            MoveScene(eSceneState.Result);
        }
    }

    public void MoveScene(eSceneState sceneState)
    {
        currentScene.Close();

        switch (sceneState)
        {
            case eSceneState.Title:
                Reset();
                currentScene = titleScene;
                    break;
            case eSceneState.Play:
                currentScene = playScene;
                break;
            case eSceneState.Result:
                currentScene = resultScene;
                break;
        }

        currentScene.Open();
    }

}
