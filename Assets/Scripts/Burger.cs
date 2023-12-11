using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burger : MonoBehaviour
{
    public GameObject vfxIn;

    public List<Componance> componances;

    bool isCompleted;

    private void Awake()
    {
        foreach(Componance obj in componances)
        {
            obj.gameObject.SetActive(false);
        }

        isCompleted = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.CompareTag("Ingredient"))
        {
            if (EnableComponance(collision.GetComponent<DragAndDrop>().type))
            {
                GameObject vfx = Instantiate(vfxIn, transform.position, Quaternion.identity) as GameObject;
                Destroy(vfx, 1f);
                collision.gameObject.SetActive(false);
            }
            else
            {
                return;
            }
        }
    }

    private bool EnableComponance(Enum type) 
    {
        for (int i = 0; i < componances.Count; i++)
        {
            if (componances[i].type == type && componances[i].gameObject.activeSelf == false && !isCompleted)
            {
                componances[i].gameObject.SetActive(true);
                CheckIsComplete();
                return true;
            }
        }
        return false;
    }

    private void CheckIsComplete()
    {
        foreach (Componance obj in componances)
        {
            if (!obj.gameObject.activeSelf)
            {
                isCompleted = false;
                return;
            }
        }
        isCompleted = true;
        GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].gameObjects.Remove(gameObject);
        GameManager.Instance.CheckLevelUp();
    }
}
