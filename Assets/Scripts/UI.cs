using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject helthBarPanle;
    void Start()
    {
        MainCharacterController.onHelthChange += ChangeUIHelthPoint;
    }

    private void ChangeUIHelthPoint(int hp)
    {
        for (int i = helthBarPanle.transform.childCount-1; i >= 0 ; i--)
        {
            if (i < hp)
            {
                helthBarPanle.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                helthBarPanle.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
 
}
