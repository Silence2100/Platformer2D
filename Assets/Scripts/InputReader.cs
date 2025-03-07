using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string HorizontalAxis = "Horizontal";
    private const KeyCode JumpKey = KeyCode.Space;

    public event Action JumpPressed;

    public float MoveInput { get; private set; }

    private void Update()
    {
        MoveInput = Input.GetAxis(HorizontalAxis);

        if (Input.GetKeyDown(JumpKey))
        {
            JumpPressed?.Invoke();
        }
    }
}