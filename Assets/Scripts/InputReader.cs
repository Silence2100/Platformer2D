using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string HorizontalAxis = "Horizontal";
    private const KeyCode JumpKey = KeyCode.Space;

    public float MoveInput {  get; private set; }
    public bool JumpPressed { get; private set; }

    private void Update()
    {
        MoveInput = Input.GetAxis(HorizontalAxis);
        JumpPressed = Input.GetKeyDown(JumpKey);
    }
}
