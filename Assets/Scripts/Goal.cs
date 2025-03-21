using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

public class Goal : MonoBehaviour, ITriggerObject
{
    [Required, SerializeField, BoxGroup("参照")]
    private bool color = false;

    [Required, SerializeField, BoxGroup("参照")]
    private COLORSTATE colorState;

    public ReactiveProperty<bool> isGoal = new ReactiveProperty<bool>(false);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (color)
        {
            spriteRenderer.color = ColorState.StateToColor(colorState);
        }
        else
        {
            spriteRenderer.color = UnityEngine.Color.black;
        }
    }

    public void OnHit(PlayerController playerController)
    {
        if (!color){
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = new Color(1, 1, 1, 0);
            isGoal.Value = true;
        }
        else if(color && colorState == playerController.colorState)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = new Color(1, 1, 1, 0);
            isGoal.Value = true;
        }
    }

    public void Reset()
    {
    }
}
