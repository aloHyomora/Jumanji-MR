using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PathMoveExample : MonoBehaviour
{
    public Transform[] points;    // points[0] -> 1번, points[1] -> 2번, ... points[7] -> 8번
    public GameObject[] vfxs;

    // 원인: 여기서 바로 Sequence를 생성하면, 내부적으로 Unity API(Application.isPlaying 등) 체크로 인해 오류 발생
    // Sequence mySequence = DOTween.Sequence();
    // ↓ 필드에서는 "선언"만 하고, "생성"은 Awake() 또는 Start()에서!
    private Sequence mySequence;

    //[Header("테스트용 (1~8 사이)")]
    public int startIndex; // 시작점 (1-based)
    public int endIndex;   // 끝점 (1-based)

    // DOTween 설정
    public float duration = 3f;
    public PathType pathType = PathType.CatmullRom;
    public Ease easeType = Ease.Linear;

    //====================================================================================================
    // 수정된 부분: Awake()나 Start() 같은 Unity 이벤트 메서드에서 Sequence를 생성
    //====================================================================================================
    private void Awake()
    {
        // MonoBehaviour가 완전히 초기화된 시점(Awake)에서 DOTween.Sequence() 호출 → OK
        mySequence = DOTween.Sequence();
    }





    /// <summary>
    /// 원하는 시점에 이 함수를 호출하면,
    /// startIndex에서 endIndex까지 경로 이동 후, 마지막에 vfx를 1초간 켰다가 끕니다.
    /// </summary>
    /// <param name="endIndex">1~8 사이의 끝점 인덱스</param>
    public void pathMoveDotween(int endIndex)
    {
        // 경로 계산
        Vector3[] pathPositions = BuildCyclicPath(points, startIndex, endIndex);

        // 이동 Tween
        transform.DOPath(pathPositions, duration, pathType)
                 .SetEase(easeType)
                 .SetOptions(false)
                 .SetLookAt(0.01f);

        // 이미 생성해 둔 Sequence에 Callback을 추가
        mySequence.AppendCallback(() =>
        {
            // endIndex-2 : vfxs 배열에서 인덱스 조정 (사용자 로직에 맞게 조정)
            //vfxs[endIndex - 2].SetActive(true);
            vfxs[endIndex - 2].GetComponent<ParticleSystem>().Play();

            // 1초 후에 다시 끄기
            DOVirtual.DelayedCall(1f, () =>
            {
                vfxs[endIndex - 2].GetComponent<ParticleSystem>().Stop();
            });
        });

        // 다음 경로 계산을 위해 startIndex 갱신
        startIndex = endIndex;
    }

    /// <summary>
    /// startIndex ~ endIndex 사이를 순환(circular)해서,
    /// 예) (start=3, end=1) -> 3->4->5->6->7->8->1 순서로 경로를 만든다.
    /// </summary>
    private Vector3[] BuildCyclicPath(Transform[] pointArray, int start, int end)
    {
        int length = pointArray.Length;
        int current = (start - 1) % length;
        int target = (end - 1) % length;

        List<Vector3> pathList = new List<Vector3>();
        pathList.Add(pointArray[current].position);

        while (current != target)
        {
            current = (current + 1) % length;
            pathList.Add(pointArray[current].position);
        }

        return pathList.ToArray();
    }
}
