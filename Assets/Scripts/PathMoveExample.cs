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

        // �̹� ������ �� Sequence�� Callback�� �߰�
        mySequence.AppendCallback(() =>
        {
            // endIndex-2 : vfxs �迭���� �ε��� ���� (����� ������ �°� ����)
            //vfxs[endIndex - 2].SetActive(true);
            vfxs[endIndex - 2].GetComponent<ParticleSystem>().Play();

            // 1�� �Ŀ� �ٽ� ����
            DOVirtual.DelayedCall(1f, () =>
            {
                vfxs[endIndex - 2].GetComponent<ParticleSystem>().Stop();
            });
        });

        // ���� ��� ����� ���� startIndex ����
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
