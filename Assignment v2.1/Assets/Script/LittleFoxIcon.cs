using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LittleFoxIcon : MonoBehaviour
{
    public Image foxIcon;
    public List<Image> foxIcons = new List<Image>();

    public GameObject LevelPanel;
    public Text levelText;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = LevelPanel.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetIcon(int level)
    {
        int numberOfFox;

        if (level == 1)
        {
            numberOfFox = 3;
            levelText.text = "Level One";
        }
        else if (level == 2)
        {
            numberOfFox = 4;
            levelText.text = "Level Two";
        }
        else
        {
            numberOfFox = 0;
        }

        StartCoroutine("DropLevel");
        foxIcons.Clear();
        Transform trans = transform;
        for (int i = 0; i < numberOfFox; i++)
        {
            Image newIcon = Instantiate(foxIcon, trans);
            foxIcons.Add(newIcon);
            newIcon.transform.SetParent(newIcon.transform.parent.parent);
            trans.position = new Vector3(transform.position.x - 50, transform.position.y, transform.position.z);
        }
    }

    public void CatchFox(int catchNum)
    {
        foxIcons[catchNum - 1].color = Color.white;
    }

    public void ClearIcons(int level)
    {
        int numberOfFox;

        if (level == 1)
        {
            numberOfFox = 3;
        }
        else if (level == 2)
        {
            numberOfFox = 4;
        }
        else
        {
            numberOfFox = 0;
        }

        for (int i = 0; i < numberOfFox; i++)
        {
            Destroy(foxIcons[i].gameObject);
            transform.position = new Vector3(transform.position.x + 50, transform.position.y, transform.position.z);
        }
    }

    public IEnumerator DropLevel()
    {
        animator.SetBool("Drop", true);
        yield return new WaitForSeconds(3);
        animator.SetBool("Drop", false);
    }
}
