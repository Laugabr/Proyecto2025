using UnityEngine;

public class vallasmanager : MonoBehaviour
{
    private bool _visitedPharmacy = false;
    private bool _visitedYarnShop = false;

    [SerializeField] private GameObject[] barricades; // Vallas y conos

    public void VisitPharmacy()
    {
        _visitedPharmacy = true;
        CheckForBarricadeRemoval();
    }

    public void VisitYarnShop()
    {
        _visitedYarnShop = true;
        CheckForBarricadeRemoval();
    }

    private void CheckForBarricadeRemoval()
    {
        if (_visitedPharmacy && _visitedYarnShop)
        {
            foreach (GameObject barricade in barricades)
            {
                barricade.SetActive(false);
            }
        }
    }
}
