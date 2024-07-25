using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeViewer : MonoBehaviour
{
    public GameObject recipePane;
    private bool isRecipeActive;
    public void ShowRecipe()
    {
        if(!isRecipeActive)
        {
            recipePane.SetActive(true);
            isRecipeActive = true;
        }

        else
        {
            recipePane.SetActive(false);
            isRecipeActive = false;
        }
    }
}
