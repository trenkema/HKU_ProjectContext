using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SetForPlayer : MonoBehaviour
{
    public List<DifferentModes> differentModes = new List<DifferentModes>();
    //public GameManager.Players visibleFor;

    private void Start()
    {
        for (int i = 0; i < differentModes.Count; i++)
        {
            if (i == GameManager.playerMode)
            {
                    if (differentModes[i].visibleFor != GameManager.thisPlayer)
                    {
                        gameObject.SetActive(false);
                    }

                    if (differentModes[i].noOne)
                {
                    gameObject.SetActive(false);
                }
            }
        }

        //if (differentModes[GameManager.Instance.playerMode].visibleFor != GameManager.thisPlayer)
        //{
        //    gameObject.SetActive(false);
        //}
        //if (visibleFor != GameManager.thisPlayer)
        //{
        //    gameObject.SetActive(false);
        //}
    }
}

[System.Serializable]
public class DifferentModes
{
    public GameManager.Players visibleFor;
    public bool noOne = false;
}
