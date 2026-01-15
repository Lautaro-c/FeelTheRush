using UnityEngine;

public class PlayerLegs : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    [SerializeField] private Vector3 initialPosition;
    private float finalZposition = 0.409f;
    private float spawnSpeed = 4f;
    private bool showLegs;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject playerArms;
    private float finalArmsRotation = 45f;
    private float finalArmsHeight = -0.26f;
    private float armsRotationSpeed = 500f;
    private float armsHeightSpeed = 2f;
    private bool stopRotating;
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
        this.transform.localPosition = initialPosition;
        showLegs = false;
        stopRotating = true;
    }

    public void Update()
    {
        if (showLegs)
        {
            if(this.transform.localPosition.z < finalZposition)
            {
                this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, this.transform.localPosition.z + (spawnSpeed * Time.deltaTime));
            }
            if(playerArms.transform.localEulerAngles.z < finalArmsRotation)
            {
                playerArms.transform.localRotation = Quaternion.Euler(playerArms.transform.localEulerAngles.x, playerArms.transform.localEulerAngles.y, playerArms.transform.localEulerAngles.z + (armsRotationSpeed * Time.deltaTime));
            }
            if(playerArms.transform.localPosition.y > finalArmsHeight)
            {
                playerArms.transform.localPosition = new Vector3(playerArms.transform.localPosition.x, playerArms.transform.localPosition.y - (armsHeightSpeed * Time.deltaTime), playerArms.transform.localPosition.z);
            }
        }
        if (!showLegs && !stopRotating)
        {
            Debug.Log("Hola3");
            if (this.transform.localPosition.z > initialPosition.z)
            {
                this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, this.transform.localPosition.z - (spawnSpeed * Time.deltaTime));
            }
            if (playerArms.transform.localEulerAngles.z > 5)
            {
                playerArms.transform.localRotation = Quaternion.Euler(playerArms.transform.localEulerAngles.x, playerArms.transform.localEulerAngles.y, playerArms.transform.localEulerAngles.z - (armsRotationSpeed * Time.deltaTime));
            }
            else
            {
                Debug.Log("Hola2");
                meshRenderer.enabled = false;
                this.transform.localPosition = initialPosition;
                playerArms.transform.localRotation = Quaternion.Euler(playerArms.transform.localEulerAngles.x, playerArms.transform.localEulerAngles.y, 0);
                playerArms.transform.localPosition = new Vector3(playerArms.transform.localPosition.x, 0, playerArms.transform.localPosition.z);
                stopRotating = true;
            }
            if (playerArms.transform.localPosition.y < 0)
            {
                playerArms.transform.localPosition = new Vector3(playerArms.transform.localPosition.x, playerArms.transform.localPosition.y + (armsHeightSpeed * Time.deltaTime), playerArms.transform.localPosition.z);
            }
        }
    }

    public void SpawnLegs()
    {
        meshRenderer.enabled = true;
        showLegs = true;
        stopRotating = false;
    }

    public void DeSpawnLegs()
    {
        showLegs = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(showLegs)
        {
            switch (other.gameObject.tag)
            {
                case "EnemyHead":
                    if (other.transform.GetComponentInParent<Actor>())
                    {
                        playerController.EnemyHeadKill(other.gameObject);
                    }
                    break;
                case "Enemy":
                    if (other.gameObject.GetComponent<Actor>())
                    {
                        other.gameObject.GetComponent<Actor>().SendEnemyFlyingUp();
                    }
                    break;
            }
        }
    }
}
