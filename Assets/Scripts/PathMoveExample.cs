using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PathMoveExample : MonoBehaviour
{
    public Transform[] points; // points[0] -> 1번, points[1] -> 2번, ... points[7] -> 8번

    [Header("테스트용 (1~8 사이)")]
    public int startIndex = 0; // 시작점 (1-based)
    public int endIndex = 1; // 끝점 (1-based)

    // DOTween 설정
    public float duration = 3f;         // 전체 경로 이동 시간
    public PathType pathType = PathType.CatmullRom;
    public Ease easeType = Ease.Linear;

    /*private void Start()
    {

        Vector3[] pathPositions = BuildCyclicPath(points, startIndex, endIndex);

        transform.DOPath(pathPositions, duration, pathType)
                 .SetEase(easeType)
                 .SetOptions(false) // true면 닫힌 곡선(Loop), 여기서는 false로
                 .SetLookAt(0.01f); // 이동 방향을 바라보도록 회전 (원하는 경우)
    }*/


    public void pathMoveDotween(int endIndex)
    {
        Vector3[] pathPositions = BuildCyclicPath(points, startIndex, endIndex);

        transform.DOPath(pathPositions, duration, pathType)
                 .SetEase(easeType)
                 .SetOptions(false) // true면 닫힌 곡선(Loop), 여기서는 false로
                 .SetLookAt(0.01f); // 이동 방향을 바라보도록 회전 (원하는 경우)

        startIndex = endIndex;
    }

    /// <summary>
    /// startIndex ~ endIndex 사이를 순환(circular)해서,
    /// 예) (start=3, end=1) -> 3->4->5->6->7->8->1 순서로 경로를 만든다.
    /// </summary>
    private Vector3[] BuildCyclicPath(Transform[] pointArray, int start, int end)
    {
        // Unity 배열은 0-based이므로, 1-based를 0-based로 변환
        int length = pointArray.Length;  // 8
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
