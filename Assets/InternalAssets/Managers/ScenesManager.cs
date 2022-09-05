using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    [SerializeField] private LoadingRedirector _redirector;


    public static int SceneOpen = 0;
    public void OpenMenu(int index)
    {
        OpenScene(index, 3);

    }

    public void NewGame(int index)
    {
        OpenScene(index, 0);


    }

    public void LoadGame(int index)
    {
        OpenScene(index, 1);
    }


    public void OpenScene(int index, int isNew = 0)
    {
        SceneOpen = isNew;
        StartCoroutine(Loading(index));
    }

    private IEnumerator Loading(int index)
    {
        LoadingRedirector redirector = Instantiate(_redirector);
        float prograss = 0;
        AsyncOperation asyncOperation;
        yield return null;
        asyncOperation = SceneManager.LoadSceneAsync(index);
        while (!asyncOperation.isDone)
        {
            prograss = asyncOperation.progress / 0.9f;
            redirector.PrograssLoading.fillAmount = prograss;
            yield return null;
        }
        Destroy(redirector.gameObject);
        //StartCoroutine(Daley(isNew));
    }

    //private static IEnumerator Daley(int isNew)
    //{
    //    if (isNew == 0)
    //        SaveManager.Instance.DeleteSave(false);
    //    else if (isNew == 1)
    //        SaveManager.Instance.LoadGame();
    //    else if (isNew == 3)
    //        SaveManager.Instance.SaveGame();
    //    yield return null;
    //}
}
