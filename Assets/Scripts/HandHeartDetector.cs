using UnityEngine;
using Oculus.Interaction.Input;

public class HandHeartDetector : MonoBehaviour
{
    public OVRSkeleton leftHandSkeleton;
    public OVRSkeleton rightHandSkeleton;

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
        // 손하트 인식 시 수행할 동작
        Debug.Log("Hand heart gesture detected!");
    }
}