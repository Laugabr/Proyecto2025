using UnityEngine;

public class tiendas : MonoBehaviour
{
    public enum StoreType { Pharmacy, YarnShop }
    public StoreType storeType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            vallasmanager checkpointManager = FindFirstObjectByType<vallasmanager>();

            if (checkpointManager != null)
            {
                if (storeType == StoreType.Pharmacy)
                    checkpointManager.VisitPharmacy();
                else if (storeType == StoreType.YarnShop)
                    checkpointManager.VisitYarnShop();
            }
        }
    }
}
