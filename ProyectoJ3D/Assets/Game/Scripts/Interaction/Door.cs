using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform doorMesh;       // el objeto que rota (la hoja de la puerta)
    public bool isOpen = false;
    public float openAngle =90f;
    public float speed = 4f;

    Quaternion closedRot;
    Quaternion openRot;

    void Start()
    {
        if (!doorMesh) doorMesh = transform; // por si la puerta es el mismo transform

        closedRot = doorMesh.localRotation;
        openRot = closedRot * Quaternion.Euler(0f, openAngle, 0f);
    }

    void Update()
    {
        Quaternion target = isOpen ? openRot : closedRot;
        doorMesh.localRotation = Quaternion.Lerp(doorMesh.localRotation, target, Time.deltaTime * speed);
    }

    public void ToggleDoor()
    {
        isOpen = !isOpen;
    }
}
