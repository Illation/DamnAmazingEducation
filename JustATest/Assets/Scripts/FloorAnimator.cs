using UnityEngine;
[RequireComponent(typeof(TextureOffsetController))]

public class FloorAnimator : MonoBehaviour {

    public float wallAnimationDuration = 2.0f;
    public float UpgradeAnimationDuration = 2.0f;

    private TextureOffsetController texOffController;
    private float duration = 0.0f;

	void Start () {
        texOffController = GetComponent<TextureOffsetController>();
    }

    void Update() {
        if (duration > 0.0f) duration -= Time.deltaTime;
        else Reset();
    }

    public void ActivateWallMovingAnimation(bool leftActive) {
        if (leftActive) texOffController.xOffsetSpeed = 0.02f;
        else texOffController.xOffsetSpeed = -0.02f;
        duration = wallAnimationDuration;
    }

    public void ActivateUpgradeAnimation() {
        texOffController.yOffsetSpeed = -0.01f;
        duration = UpgradeAnimationDuration;
    }

    private void Reset() {
        texOffController.xOffsetSpeed = 0;
        texOffController.yOffsetSpeed = 0;
        texOffController.ResetOffset();
    }
}
