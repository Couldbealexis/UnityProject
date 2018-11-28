// Copyright 2014 Google Inc. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace GoogleVR.HelloVR {
  using UnityEngine;
  using UnityEngine.EventSystems;

  [RequireComponent(typeof(Collider))]
  public class ObjectController : MonoBehaviour {
    private Vector3 startingPosition;
    private Renderer myRenderer;

    public Material inactiveMaterial;
    public Material gazedAtMaterial;

    public Score scoreScript;
    public instruccionesController instruccionesScript;

    void Start() {
      startingPosition = transform.localPosition;
      myRenderer = GetComponent<Renderer>();
      SetGazedAt(false);
      
      //hideMenu();
    }

    public void SetGazedAt(bool gazedAt) {
      if (inactiveMaterial != null && gazedAtMaterial != null) {
        myRenderer.material = gazedAt ? gazedAtMaterial : inactiveMaterial;
        return;
      }
    }

    public void Reset() {
      
      int sibIdx = transform.GetSiblingIndex();
      int numSibs = transform.parent.childCount;
      for (int i=0; i<numSibs; i++) {
        GameObject sib = transform.parent.GetChild(i).gameObject;
        sib.transform.localPosition = startingPosition;
        sib.SetActive(i == sibIdx);
      }
    }

    public void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.gameObject.tag);
      if(scoreScript.dificultad > 0){
          if(scoreScript.intentos < scoreScript.disponibles){
            if(col.gameObject.tag=="door3"){
                //TeleportRandomly ();
                GameObject.Destroy(col.gameObject);
                scoreScript.UpdateScore(1);//puntuacion real
                Debug.Log("door3");
                //Destroy (GameObject.FindWithTag("gameMenu"));
            }
          }
          
      }
      else{
          if(col.gameObject.tag=="dificultad1")
          {
              scoreScript.setDificultad(1);//puntuacion real
              //destroyMenu();
          }
          else if(col.gameObject.tag=="dificultad2")
          {
              scoreScript.setDificultad(2);//puntuacion real
              //destroyMenu();
          }
          else if(col.gameObject.tag=="dificultad3")
          {
              scoreScript.setDificultad(3);//puntuacion real
              //destroyMenu();
          }
          else if(col.gameObject.tag=="howto")
          {
              //showMenu();
          }
          else if(col.gameObject.tag=="back" || col.gameObject.tag=="instructions")
          {
              //hideMenu();
          }
      }
          
    }

    void OnTriggerEnter (Collider collider) 
    {
        Debug.Log("howto");
        Debug.Log(collider);
        // collider.gameObject.tag;
        // GameObject objeto1 = GameObject.Find("objeto1");
        if (collider.gameObject.name == "howto") 
        {
            Debug.Log("howto");
        }
    }

    public void Recenter() {
#if !UNITY_EDITOR
      GvrCardboardHelpers.Recenter();
#else
      if (GvrEditorEmulator.Instance != null) {
        GvrEditorEmulator.Instance.Recenter();
      }
#endif  // !UNITY_EDITOR
    }

    public void TeleportRandomly() {
      // Pick a random sibling, move them somewhere random, activate them,
      // deactivate ourself.
      int sibIdx = transform.GetSiblingIndex();
      int numSibs = transform.parent.childCount;
      sibIdx = (sibIdx + Random.Range(1, numSibs)) % numSibs;
      GameObject randomSib = transform.parent.GetChild(sibIdx).gameObject;

      // Move to random new location ±100º horzontal.
      Vector3 direction = Quaternion.Euler(
          0,
          Random.Range(-90, 90),
          0) * Vector3.forward;
      // New location between 1.5m and 3.5m.
      float distance = 2 * Random.value + 1.5f;
      Vector3 newPos = direction * distance;
      
      // Limit vertical position to be fully in the room.
      newPos.y = Mathf.Clamp(newPos.y, -1.2f, 4f);
      randomSib.transform.localPosition = newPos;

      randomSib.SetActive(true);
      gameObject.SetActive(false);
      SetGazedAt(false);
    }
  }
}