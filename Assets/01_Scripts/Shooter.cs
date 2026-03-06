using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour

{
    [SerializeField] private InputAction shootInput;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject arrowObject;
    [SerializeField] private float shootForce;
    
     void OnEnable()
    {
        shootInput.Enable();
        shootInput.performed += Shoot;
    } 
     
     void OnDisable()
     {
         shootInput.performed -= Shoot;
     }
     
     private void Shoot(InputAction.CallbackContext context)
     {
         GameObject arrow = Instantiate(arrowObject, shootPoint.position, shootPoint.rotation);
         arrow.GetComponent<Rigidbody>().AddForce(shootForce * shootPoint.forward);

     }
    
     
}
