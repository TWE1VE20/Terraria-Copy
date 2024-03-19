using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    [SerializeField] CharacterController playerController;

    public override IEnumerator LoadingRoutine()
    {
        Debug.Log("GameScene Load");
        // fake loading
        yield return new WaitForSeconds(1f);
        Manager.Scene.SetLoadingBarValue(0.3f);
        Debug.Log("Player Spawn");
        Manager.Scene.SetLoadingBarValue(0.4f);
        yield return new WaitForSeconds(0.5f);
        Debug.Log("오브젝트 풀 준비");
        Manager.Scene.SetLoadingBarValue(0.6f);
        yield return new WaitForSeconds(0.5f);
        Debug.Log("몬스터 스폰");
        Manager.Scene.SetLoadingBarValue(0.7f);
        yield return new WaitForSeconds(0.5f);
        Manager.Scene.SetLoadingBarValue(0.9f);
        Debug.Log("게임씬 로딩 끝");
    }

    public override void SceneLoad()
    {
        playerController.enabled = false;
        playerController.enabled = true;
    }
}
