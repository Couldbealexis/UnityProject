namespace GoogleVR.HelloVR {
  using UnityEngine;
  using UnityEngine.EventSystems;

  [RequireComponent(typeof(Collider))]
  public class instruccionesController : MonoBehaviour {
    private Vector3 startingPosition;
    private Renderer myRenderer;

    public Material inactiveMaterial;
    public Material gazedAtMaterial;

    public Score scoreScript;

		public GameObject goBack;
		public GameObject inst;

		public GameObject howTo;

		public GameObject facil;
		public GameObject medio;
		public GameObject dificil;

		public GameObject nombre;
		public GameObject reset;

    void Start() {
      startingPosition = transform.localPosition;
      myRenderer = GetComponent<Renderer>();
      SetGazedAt(false);
			goBack = GameObject.FindWithTag("back");
			inst = GameObject.FindWithTag("instructions");
			howTo = GameObject.FindWithTag("howto");
			facil = GameObject.FindWithTag("dificultad1");
			medio = GameObject.FindWithTag("dificultad2");
			dificil = GameObject.FindWithTag("dificultad3");
			nombre = GameObject.FindWithTag("presentation");
			scoreScript.clearText();
      hideMenu();

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
				Destroy(GameObject.FindWithTag("bullet"));
				showMenu();
    }

    void OnTriggerEnter (Collider collider) 
    {
        Debug.Log("howto");
        Debug.Log(collider);
    }

    public void hideLevels(){
        howTo.SetActive(false);
				facil.SetActive(false);
				medio.SetActive(false);
				dificil.SetActive(false);
				nombre.SetActive(false);
				try{
					hideMenu();
				}
				catch{

				}
				
    }

		public void showLevels(){
        howTo.SetActive(false);
				facil.SetActive(false);
				medio.SetActive(false);
				dificil.SetActive(false);
				nombre.SetActive(false);
    }

    public void hideMenu(){
        goBack.SetActive(false);
        inst.SetActive(false);
    }

    public void showMenu(){
        goBack.SetActive(true);
        inst.SetActive(true);
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
