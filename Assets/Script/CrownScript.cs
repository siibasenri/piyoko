using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//王冠に関するクラス
public class CrownScript : MonoBehaviour
{
    void Start()
    {
        //浮遊している
        this.transform.DOMove(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), 2f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutCubic)
            .SetLink(gameObject);
    }

}
