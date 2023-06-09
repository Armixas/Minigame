using UnityEngine;

public class SpriteDirectionalController : MonoBehaviour
{
    [SerializeField] float backAngle = 65f;
    [SerializeField] float sideAngle = 155f;
    [SerializeField] Transform mainTransform;
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;

    private void LateUpdate()
    {
        Transform mainTransform = Camera.main.transform;
        Vector3 camForwardVector = new Vector3(mainTransform.forward.x, 0f, mainTransform.forward.z);
        Debug.DrawRay(mainTransform.position, camForwardVector * 5f, Color.magenta);

        float signedAngle = Vector3.SignedAngle(mainTransform.forward, camForwardVector, Vector3.up);

        Vector2 animationDirection = new Vector2(0f, -1f);

        float angle = Mathf.Abs(signedAngle);

        
        if (angle < backAngle)
        {
            //back animation
            animationDirection = new Vector2(0f, -1f);
        }
        else if (angle < sideAngle)
        {
            //side animation, right
            animationDirection = new Vector2(1f, 0f);

            if (signedAngle < 0)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }
        else
        {
            //front animation
            animationDirection = new Vector2(0f, 1f);
        }

        /*animator.SetFloat("Horizontal", animationDirection.x);
        animator.SetFloat("Vertical", animationDirection.y);*/
    }
}
