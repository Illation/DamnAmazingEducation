using UnityEngine;
[RequireComponent(typeof(TextureOffsetController))]

public class FloorAnimator : MonoBehaviour {

    public float wallAnimationDuration = 2.0f;
    public float UpgradeAnimationDuration = 2.0f;
    public float wallMovingAnimationSpeed = 1.2f;
    public float upgradeAnimationSpeed = 1.0f;
    public bool active = false;

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
        Reset();
        if (leftActive) texOffController.xOffsetSpeed = wallMovingAnimationSpeed;
        else texOffController.xOffsetSpeed = -wallMovingAnimationSpeed;
        duration = wallAnimationDuration;
        active = true;
    }

    public void ActivateUpgradeAnimation() {
        Reset();
        texOffController.yOffsetSpeed = -upgradeAnimationSpeed;
        duration = UpgradeAnimationDuration;
        active = true;
    }

    private void Reset() {
        texOffController.xOffsetSpeed = 0;
        texOffController.yOffsetSpeed = 0;
        texOffController.ResetOffset();
        active = false;
    }
}
