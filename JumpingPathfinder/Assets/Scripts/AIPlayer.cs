using Unity.VisualScripting;
using UnityEngine;

public class AIPlayer : MonoBehaviour
{
    private NavMesh myNavMesh;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GetNavMesh()
    {
        CharacterController controller = GetComponent<CharacterController>();
        AIMovementParameters movementParams = new AIMovementParameters();
        movementParams.movementSpeed = controller.speed;
        movementParams.jumpPower = controller.jumpPower;
        movementParams.gravityStrength = controller.gravityStrength;
        movementParams.linearDamping = controller.linearDamping;
        myNavMesh = GameObject.Find("Level").GetComponent<NavMeshGenerator>().GenerateNavMesh(movementParams);
    }
}
