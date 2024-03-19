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
        Debug.Log("������Ʈ Ǯ �غ�");
        Manager.Scene.SetLoadingBarValue(0.6f);
        yield return new WaitForSeconds(0.5f);
        Debug.Log("���� ����");
        Manager.Scene.SetLoadingBarValue(0.7f);
        yield return new WaitForSeconds(0.5f);
        Manager.Scene.SetLoadingBarValue(0.9f);
        Debug.Log("���Ӿ� �ε� ��");
    }

    public override void SceneLoad()
    {
        playerController.enabled = false;
        playerController.enabled = true;
    }
}
