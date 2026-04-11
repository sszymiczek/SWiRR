using UnityEngine;

public class CauldronTrigger : MonoBehaviour
{
    public GameObject gate;

    private void OnTriggerEnter(Collider other)
    {
        print("Triggered!");
        if (other.gameObject.CompareTag("Skull"))
        {
            print("Rotating door!");

            gate.transform.Rotate(0, 70, 0);
        }
    }
}
