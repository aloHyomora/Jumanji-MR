using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PathMoveExample : MonoBehaviour
{
    public Transform[] points; // points[0] -> 1��, points[1] -> 2��, ... points[7] -> 8��

    [Header("�׽�Ʈ�� (1~8 ����)")]
    public int startIndex = 0; // ������ (1-based)
    public int endIndex = 1; // ���� (1-based)

    // DOTween ����
    public float duration = 3f;         // ��ü ��� �̵� �ð�
    public PathType pathType = PathType.CatmullRom;
    public Ease easeType = Ease.Linear;

    /*private void Start()
    {

        Vector3[] pathPositions = BuildCyclicPath(points, startIndex, endIndex);

        transform.DOPath(pathPositions, duration, pathType)
                 .SetEase(easeType)
                 .SetOptions(false) // true�� ���� �(Loop), ���⼭�� false��
                 .SetLookAt(0.01f); // �̵� ������ �ٶ󺸵��� ȸ�� (���ϴ� ���)
    }*/


    public void pathMoveDotween(int endIndex)
    {
        Vector3[] pathPositions = BuildCyclicPath(points, startIndex, endIndex);

        transform.DOPath(pathPositions, duration, pathType)
                 .SetEase(easeType)
                 .SetOptions(false) // true�� ���� �(Loop), ���⼭�� false��
                 .SetLookAt(0.01f); // �̵� ������ �ٶ󺸵��� ȸ�� (���ϴ� ���)

        startIndex = endIndex;
    }

    /// <summary>
    /// startIndex ~ endIndex ���̸� ��ȯ(circular)�ؼ�,
    /// ��) (start=3, end=1) -> 3->4->5->6->7->8->1 ������ ��θ� �����.
    /// </summary>
    private Vector3[] BuildCyclicPath(Transform[] pointArray, int start, int end)
    {
        // Unity �迭�� 0-based�̹Ƿ�, 1-based�� 0-based�� ��ȯ
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
