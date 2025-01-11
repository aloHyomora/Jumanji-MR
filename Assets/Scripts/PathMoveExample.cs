using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PathMoveExample : MonoBehaviour
{
    public Transform[] points;    // points[0] -> 1��, points[1] -> 2��, ... points[7] -> 8��
    public GameObject[] vfxs;

    // ����: ���⼭ �ٷ� Sequence�� �����ϸ�, ���������� Unity API(Application.isPlaying ��) üũ�� ���� ���� �߻�
    // Sequence mySequence = DOTween.Sequence();
    // �� �ʵ忡���� "����"�� �ϰ�, "����"�� Awake() �Ǵ� Start()����!
    private Sequence mySequence;

    //[Header("�׽�Ʈ�� (1~8 ����)")]
    public int startIndex; // ������ (1-based)
    public int endIndex;   // ���� (1-based)

    // DOTween ����
    public float duration = 3f;
    public PathType pathType = PathType.CatmullRom;
    public Ease easeType = Ease.Linear;

    //====================================================================================================
    // ������ �κ�: Awake()�� Start() ���� Unity �̺�Ʈ �޼��忡�� Sequence�� ����
    //====================================================================================================
    private void Awake()
    {
        // MonoBehaviour�� ������ �ʱ�ȭ�� ����(Awake)���� DOTween.Sequence() ȣ�� �� OK
        mySequence = DOTween.Sequence();
    }

    [ContextMenu("index 2")]
    public void Index2test()
    {
        pathMoveDotween(2);
    }

    [ContextMenu("index 3")]
    public void Index3test()
    {
        pathMoveDotween(3);
    }

    [ContextMenu("index 4")]
    public void Index4test()
    {
        pathMoveDotween(4);
    }

    [ContextMenu("index 5")]
    public void Index5test()
    {
        pathMoveDotween(5);
    }

    [ContextMenu("index 6")]
    public void Index6test()
    {
        pathMoveDotween(6);
    }

    [ContextMenu("index 7")]
    public void Index7test()
    {
        pathMoveDotween(7);
    }

    [ContextMenu("index 8")]
    public void Index8test()
    {
        pathMoveDotween(8);
    }



    /// <summary>
    /// ���ϴ� ������ �� �Լ��� ȣ���ϸ�,
    /// startIndex���� endIndex���� ��� �̵� ��, �������� vfx�� 1�ʰ� �״ٰ� ���ϴ�.
    /// </summary>
    /// <param name="endIndex">1~8 ������ ���� �ε���</param>
    public void pathMoveDotween(int endIndex)
    {
        // ��� ���
        Vector3[] pathPositions = BuildCyclicPath(points, startIndex, endIndex);

        // �̵� Tween
        transform.DOPath(pathPositions, duration, pathType)
                 .SetEase(easeType)
                 .SetOptions(false)
                 .SetLookAt(0.01f);

        Debug.Log("vfxs[endIndex - 2]");
        DOVirtual.DelayedCall(3f, () =>
        {
            vfxs[endIndex - 2].SetActive(true);
            
        });
        DOVirtual.DelayedCall(8f, () =>
        {
            vfxs[endIndex - 2].SetActive(false);
           
        });
       
        startIndex = endIndex;
    }

    /// <summary>
    /// startIndex ~ endIndex ���̸� ��ȯ(circular)�ؼ�,
    /// ��) (start=3, end=1) -> 3->4->5->6->7->8->1 ������ ��θ� �����.
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
