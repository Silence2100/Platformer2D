using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string HorizontalAxis = "Horizontal";
    private const KeyCode JumpKey = KeyCode.Space;
    private const KeyCode AttackKey = KeyCode.F;
    private const KeyCode VampireKey = KeyCode.E;

    public event Action JumpPressed;
    public event Action AttackPressed;
    public event Action VampirePressed;

    public float MoveInput { get; private set; }

    private void Update()
    {
        MoveInput = Input.GetAxis(HorizontalAxis);

        if (Input.GetKeyDown(JumpKey))
        {
            JumpPressed?.Invoke();
        }

        if (Input.GetKeyDown(AttackKey))
        {
            AttackPressed?.Invoke();
        }

        if (Input.GetKeyDown(VampireKey))
        {
            VampirePressed?.Invoke();
        }
    }
}