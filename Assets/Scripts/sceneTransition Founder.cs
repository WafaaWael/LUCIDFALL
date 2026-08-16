using UnityEngine;

public class sceneTransitionFounder : MonoBehaviour
{
    [SerializeField] private int _sceneIndexToLoad = 0; // Set the index of the scene you want to load in the Inspector
    public void FindSceneTransition()
   {
        SceneTransition transition = FindObjectOfType<SceneTransition>();
      if (transition != null)
      {
         transition.LoadScene(_sceneIndexToLoad); // Replace "NextSceneName" with the actual name of the scene you want to transition to.
      }
      else
      {
         Debug.LogError("No sceneTransition component found in the scene.");
      }
    }
}
