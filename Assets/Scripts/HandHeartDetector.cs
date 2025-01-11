using DG.Tweening;
using UnityEngine;
using Oculus.Interaction.Input;

public class HandHeartDetector : MonoBehaviour
{
    public OVRSkeleton leftHandSkeleton;
    public OVRSkeleton rightHandSkeleton;
    public GameObject airplane;
    private int handHeartGestureCount = 0; // 손하트 제스처 호출 카운트
    private const int Threshold = 20; // 특정 동작을 실행하기 위한 호출 횟수 기준
    private Ease[] easeOptions = { Ease.InCubic, Ease.InQuad, Ease.InExpo }; // 랜덤 선택할 Ease 배열

    void Update()
    {
        if (leftHandSkeleton != null && rightHandSkeleton != null)
        {
            // 엄지와 검지 끝 부분 가져오기
            Vector3 leftThumbTip = GetBonePosition(leftHandSkeleton, OVRSkeleton.BoneId.Hand_ThumbTip);
            Vector3 leftIndexTip = GetBonePosition(leftHandSkeleton, OVRSkeleton.BoneId.Hand_IndexTip);

            Vector3 rightThumbTip = GetBonePosition(rightHandSkeleton, OVRSkeleton.BoneId.Hand_ThumbTip);
            Vector3 rightIndexTip = GetBonePosition(rightHandSkeleton, OVRSkeleton.BoneId.Hand_IndexTip);

            // 엄지와 검지가 만났는지 확인
            if (IsClose(leftThumbTip, rightIndexTip) && IsClose(leftIndexTip, rightThumbTip))
            {
                Debug.Log("Detected hand heart gesture!");
                OnHandHeartGesture();
            }
        }
    }

    private Vector3 GetBonePosition(OVRSkeleton skeleton, OVRSkeleton.BoneId boneId)
    {
        foreach (var bone in skeleton.Bones)
        {
            if (bone.Id == boneId)
            {
                Debug.Log(bone.Transform.position);
                return bone.Transform.position;
            }
        }

        Debug.Log($"Count: {skeleton.Bones.Count}, Zero");
        return Vector3.zero;
    }

    private bool IsClose(Vector3 pointA, Vector3 pointB, float threshold = 0.05f)
    {
        return Vector3.Distance(pointA, pointB) <= threshold;
    }


    private void OnHandHeartGesture()
    {
        // 손하트 제스처 인식
        Debug.Log("Hand heart gesture detected!");
        handHeartGestureCount++; // 호출 카운트 증가

        // 호출 횟수가 Threshold에 도달했는지 확인
        if (handHeartGestureCount >= Threshold)
        {
            PerformSpecialAction();
            handHeartGestureCount = 0; // 카운트 초기화
        }
    }

    private void PerformSpecialAction()
    {
        // 특정 동작 실행
        Debug.Log("Special action performed after 10 hand heart gestures!");
        Transform airplaneTransform = Instantiate(airplane,
            GetBonePosition(leftHandSkeleton, OVRSkeleton.BoneId.Hand_IndexTip), Quaternion.identity).transform;

        // 카메라의 앞 방향 계산
        Vector3 forwardDirection = mainCamera.transform.forward;

        // 이동 목표 위치 계산
        Vector3 targetPosition = airplaneTransform.position + forwardDirection * moveDistance;

        // Ease를 랜덤 선택
        Ease randomEase = easeOptions[Random.Range(0, easeOptions.Length)];

        // 초기 흔들림 각도 랜덤 설정 (5도 ~ 20도)
        shakeAngle = Random.Range(-20f, 20f);
        // DOTween을 사용하여 이동
        airplaneTransform.DOMove(targetPosition, duration)
            .SetEase(randomEase) // 랜덤 Ease 적용
            .OnComplete(() => Debug.Log($"Movement Complete with Ease: {randomEase}"));

        // Z축을 기준으로 흔들림 추가
        airplaneTransform.DORotate(new Vector3(0, 0, shakeAngle), shakeDuration)
            .SetEase(Ease.InOutSine)
            .SetLoops(shakeLoops, LoopType.Yoyo)
            .OnStepComplete(() =>
            {
                // 흔들림 각도를 매 사이클 랜덤화 (5도 ~ 20도)
                shakeAngle = Random.Range(-20f, 20f);
                airplaneTransform.DORotate(new Vector3(0, 0, shakeAngle), shakeDuration)
                    .SetEase(Ease.InOutSine)
                    .SetLoops(shakeLoops, LoopType.Yoyo);
            });
    }

    public Camera mainCamera; // 카메라 참조
    public float moveDistance = 10f; // 이동 거리
    public float duration = 2f; // 이동 시간
    public float shakeDuration = 0.5f; // 흔들림 한 사이클의 지속 시간
    private float shakeAngle; // 흔들림 각도
    public int shakeLoops = -1; // 무한 반복 (-1)
}