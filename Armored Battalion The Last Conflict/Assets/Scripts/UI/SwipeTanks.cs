using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeTanks : MonoBehaviour
{
    public GameObject scrollbar;
    [Range(1f, 1.5f)] public float selectedSize = 1.1f;
    [Range(0f, 0.1f)] public float animationSpeed = 0.1f;
    private float scrollPosition;
    private float[] position;
    private float distance;
    private float distanceThreshold;
    private Scrollbar scroll;
    private Vector3 selectedSizeVerVector3;
    private bool start = false;

    public int selectedIndex = 0;
    public Button selectButton;
    private TankSelectSystem tankSelectSystem;

    private void OnEnable()
    {
        tankSelectSystem = FindObjectOfType<TankSelectSystem>();
        position = new float[transform.childCount];
        distance = 1f / (position.Length - 1f);
        distanceThreshold = distance / 2f;
        selectedSizeVerVector3 = new Vector3(selectedSize, selectedSize, 1f);
        scroll = scrollbar.GetComponent<Scrollbar>();
        for (int i = 0; i < position.Length; i++)
        {
            position[i] = distance * i;
        }
        start = true;
    }

    private void Update()
    {
        if (tankSelectSystem.selectionPoints >= tankSelectSystem.selectTankImages[selectedIndex].cost)
            selectButton.interactable = true;
        else
            selectButton.interactable = false;

        if (start)
        {
            if (Input.GetMouseButton(0))
            {
                scrollPosition = scroll.value;
            }
            else
            {
                foreach (float currentSwipePosition in position)
                {
                    if (scrollPosition < currentSwipePosition + distanceThreshold && scrollPosition > currentSwipePosition - distanceThreshold)
                    {
                        scroll.value = Mathf.Lerp(scroll.value, currentSwipePosition, animationSpeed);
                    }
                }
            }

            for (int i = 0; i < position.Length; i++)
            {
                Transform selected = transform.GetChild(i);
                if (scrollPosition < position[i] + distanceThreshold && scrollPosition > position[i] - distanceThreshold)
                {
                    selected.localScale = Vector3.Lerp(selected.localScale, selectedSizeVerVector3, animationSpeed);
                    selectedIndex = i;
                    for (int j = 0; j < position.Length; j++)
                    {
                        if (j == i)
                        {
                            continue;
                        }

                        var unselected = transform.GetChild(j);
                        unselected.localScale = Vector3.Lerp(unselected.localScale, Vector3.one, animationSpeed);

                    }
                }
            }
        }
    }
}
