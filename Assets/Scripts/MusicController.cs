using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MusicController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 小節に来たフレームで true になる
        if (Music.IsJustChangedBar())
        {
            DOTween
                .To(value => OnRotate(value), 0, 1, 0.5f)
                .SetEase(Ease.OutCubic)
                .OnComplete(() => transform.localEulerAngles = new Vector3(45, 45, 0))
            ;
        }
        // 拍に来たフレームで true になる
        else if (Music.IsJustChangedBeat())
        {
            DOTween
                .To(value => OnScale(value), 0, 1, 0.1f)
                .SetEase(Ease.InQuad)
                .SetLoops(2, LoopType.Yoyo)
            ;
        }
    }

    private void OnScale(float value)
    {
        var scale = Mathf.Lerp(1, 1.2f, value);
        transform.localScale = new Vector3(scale, scale, scale);
    }

    private void OnRotate(float value)
    {
        var rot = transform.localEulerAngles;
        rot.z = Mathf.Lerp(0, 360, value);
        transform.localEulerAngles = rot;
    }
}
