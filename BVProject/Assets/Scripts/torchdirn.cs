using UnityEngine;

public class torchdirn : MonoBehaviour
{
    public movementstatemanager playerMovement;

    public Vector3 leftOffset;
    public Vector3 rightOffset;
    public Vector3 upOffset;
    public Vector3 downOffset;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.prevLastInput == movementstatemanager.LastInput.Left)
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
            transform.localPosition = leftOffset;
        }
        else if (playerMovement.prevLastInput == movementstatemanager.LastInput.Right)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
            transform.localPosition = rightOffset;
        }
        else if (playerMovement.prevLastInput == movementstatemanager.LastInput.Up)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.localPosition = upOffset;
        }
        else if (playerMovement.prevLastInput == movementstatemanager.LastInput.Down)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            transform.localPosition = downOffset;
        }
    }
}
