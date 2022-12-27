using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    [SerializeField] float period = 5f;

    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        // Continually growing over time
        if (period <= Mathf.Epsilon) 
        {
            return;
        }

        float cycles = Time.time / period;

        // Constant value of 6.283
        const float tau = Mathf.PI * 2;

        // Going from -1 to 1
        float rawSinWave = Mathf.Sin(cycles * tau);

        // Recalculate to go from 0 to 1 so its cleaner
        movementFactor = (rawSinWave + 1f) / 2f;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
