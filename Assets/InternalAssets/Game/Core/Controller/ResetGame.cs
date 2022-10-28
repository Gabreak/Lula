using Sirenix.OdinInspector;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ResetGame : MonoBehaviour
{
    [Required]
    public VideoGirlsData _videoGirls;
    [Button("NewGame")]
    private void NewGame()
    {
        DefaultGirls();
    }

    private void DefaultGirls()
    {
        for (int i = 0; i < _videoGirls.Girls.Length; i++)
        {
            GirlBase girl = _videoGirls.Girls[i];
            if (girl.Id == 1)
            {
                _videoGirls.Girls[i].IsActive = true;
            }
            else
                _videoGirls.Girls[i].IsActive = false;
        }
        Debug.Log("Ready Girls");
    }

    private void BuildNormal()
    {

    }
}
