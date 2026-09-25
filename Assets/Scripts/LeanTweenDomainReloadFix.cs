using UnityEngine;

public class LeanTweenDomainReloadFix : MonoBehaviour {
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetLeanTween() {
        // Xóa sạch trạng thái tween cũ trước khi Scene mới chạy
        LeanTween.reset();
    }
}
