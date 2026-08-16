using UnityEngine;

public class sceneTransitionFounder : MonoBehaviour
{
   public void FindSceneTransition()
   {
        SceneTransition transition = FindObjectOfType<SceneTransition>();
      if (transition != null)
      {
         transition.LoadScene("0"); // Replace "NextSceneName" with the actual name of the scene you want to transition to.
      }
      else
      {
         Debug.LogError("No sceneTransition component found in the scene.");
      }
    }
}
