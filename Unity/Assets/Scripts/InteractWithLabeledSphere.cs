using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

/// <summary>
/// This class contains the script to interact with the labeled sphere
/// </summary>
public class InteractWithLabeledSphere : MonoBehaviour
{

    private InputField input;
    private GameObject rightController;
    private IEnumerator coroutine;
    private GameObject sp;
    private Color[] colors;
    public List<string> labelList;
    private Dropdown DropDownList;
    private List<string> m_dropdownList;
    public GameObject SphereGenerator;
    public bool showingSet;
    private bool isLocating;
    private bool isFetching;

    // Use this for initialization
    void Start()
    {
        input = gameObject.GetComponent(typeof(InputField)) as InputField;
        rightController = GameObject.FindGameObjectWithTag("RightController");
        colors = new Color[3] { Color.red, Color.magenta, Color.cyan };
        labelList = SphereGenerator.GetComponent<SpheresGenerator>().labelList;
        DropDownList = SphereGenerator.GetComponent<SpheresGenerator>().DropDownList;
        m_dropdownList = SphereGenerator.GetComponent<SpheresGenerator>().m_dropdownList;
        showingSet = false;
        isLocating = false;
    }

    /// <summary>
    /// Fetch the selected sphere to the front of the user
    /// </summary>
    public void FetchSphere()
    {
        if(!isLocating){
            string spName = DropDownList.options[DropDownList.value].text;
            sp = GameObject.Find(spName);
            if (sp != null)
            {
                sp.transform.position = rightController.transform.position + rightController.transform.forward;
            } else {
                int page = m_dropdownList.IndexOf(spName) / 450 + 1;
                int curPage = SphereGenerator.GetComponent<SpheresGenerator>().pageNum;
                int pageToFlip = 0;
                if(page >= curPage){
                    pageToFlip = page - curPage;
                }else{
                    pageToFlip = 6 - curPage + page;
                }
                for(int i = 0; i < pageToFlip; i++){
                    SphereGenerator.GetComponent<SpheresGenerator>().NextPage();
                }
                sp = GameObject.Find(DropDownList.options[DropDownList.value].text);

                sp.transform.position = rightController.transform.position + rightController.transform.forward;
            }
        }
    }

    /// <summary>
    /// Locate the selected sphere
    /// </summary>
    public void LocateSphere()
    {
        if(!isFetching){

        
            string spName = DropDownList.options[DropDownList.value].text;
            sp = GameObject.Find(spName);

            if (sp != null)
            {
                sp.tag = "Sphere_blinking";
                coroutine = Blink(3.0f, sp);
                StartCoroutine(coroutine);

                coroutine = Resize(3.0f, sp);
                StartCoroutine(coroutine);
            } else {
                int page = m_dropdownList.IndexOf(spName) / 450 + 1;
                int curPage = SphereGenerator.GetComponent<SpheresGenerator>().pageNum;
                int pageToFlip = 0;
                if(page >= curPage){
                    pageToFlip = page - curPage;
                }else{
                    pageToFlip = 6 - curPage + page;
                }
                for(int i = 0; i < pageToFlip; i++){
                    SphereGenerator.GetComponent<SpheresGenerator>().NextPage();
                }

                
                sp = GameObject.Find(DropDownList.options[DropDownList.value].text);

                sp.tag = "Sphere_blinking";
                coroutine = Blink(3.0f, sp);
                StartCoroutine(coroutine);

                coroutine = Resize(3.0f, sp);
                StartCoroutine(coroutine);
            }

        }

    }

    /// <summary>
    /// Make the sphere blink
    /// </summary>
    /// <param name="waitTime">How long the blink lasts</param>
    /// <param name="go">The gameobject to blink</param>
    /// <returns></returns>
    private IEnumerator Blink(float waitTime, GameObject go)
    {
        isLocating = true;
        int i = 0;
        float endTime = Time.time + waitTime;
        while (Time.time < endTime)
        {
            go.GetComponent<VRTK_InteractableObject>().touchHighlightColor = colors[i % 3];
            go.GetComponent<VRTK_InteractableObject>().ToggleHighlight(true);
            i++;
            yield return new WaitForSeconds(0.3f);
        }
        go.GetComponent<VRTK_InteractableObject>().touchHighlightColor = Color.yellow;
        go.GetComponent<VRTK_InteractableObject>().ToggleHighlight(false);
        go.tag = "Sphere";
        isLocating = false;
    }

    /// <summary>
    /// Resize the sphere for given time
    /// </summary>
    /// <param name="waitTime">How long does the resizing last</param>
    /// <param name="go">The gameobject to resize</param>
    /// <returns></returns>
    private IEnumerator Resize(float waitTime, GameObject go)
    {
        isLocating = true;
        float endTime = Time.time + waitTime;
        bool enlarge = true;
        Vector3 originalScale = go.transform.localScale;
        while (Time.time < endTime)
        {
            if (enlarge)
            {
                go.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
                enlarge = false;
            }
            else
            {
                go.transform.localScale = originalScale;
                enlarge = true;
            }
            yield return new WaitForSeconds(0.3f);
        }
        go.transform.localScale = originalScale;
        isLocating = false;
    }

    /// <summary>
    /// Show the set/dependents of the selected sphere
    /// </summary>
    public void ShowSet(){
        sp = GameObject.Find(DropDownList.options[DropDownList.value].text);
        if(sp != null){
            showingSet = !showingSet;
            if(showingSet){
                List<GameObject> descendents = sp.GetComponent<Parent>().descendents;
                foreach(GameObject sphere in SphereGenerator.GetComponent<SpheresGenerator>().spheres){
                    if(descendents.Contains(sphere)){
                        sphere.SetActive(true);
                        sphere.GetComponent<VRTK_InteractableObject>().touchHighlightColor = Color.yellow;
                        sphere.GetComponent<VRTK_InteractableObject>().ToggleHighlight(true);
                    }else{
                        sphere.SetActive(false);
                    }
                }

                sp.SetActive(true);
                sp.GetComponent<VRTK_InteractableObject>().touchHighlightColor = Color.yellow;
                sp.GetComponent<VRTK_InteractableObject>().ToggleHighlight(true);

            }else{
                int pageNum = SphereGenerator.GetComponent<SpheresGenerator>().pageNum;
                List<GameObject> spheres = SphereGenerator.GetComponent<SpheresGenerator>().spheres;
                for(int i = 0; i < spheres.Count; i++){
                    spheres[i].GetComponent<VRTK_InteractableObject>().ToggleHighlight(false);
                    if(i >= (pageNum - 1) * 450 && i < pageNum * 450){
                        spheres[i].SetActive(true);
                    }else if(spheres[i].tag != "SphereInModel"){
                        spheres[i].SetActive(false);
                    }

                }
            }
        }
    }

    /// <summary>
    /// go to the next item in the list
    /// </summary>
    public void NextItem(){
        if(DropDownList.value < DropDownList.options.Count-1){
            DropDownList.value++;
        }else{
            int index = m_dropdownList.IndexOf(DropDownList.options[DropDownList.value].text);
            int startIndex = m_dropdownList.IndexOf(DropDownList.options[0].text) + 1;
            int range = DropDownList.options.Count;
            DropDownList.ClearOptions();
            DropDownList.AddOptions(m_dropdownList.GetRange(startIndex, range));
        }
    }

    /// <summary>
    /// go to the last item in the list
    /// </summary>
    public void LastItem(){
        if(DropDownList.value > 0){
            DropDownList.value--;
        }else{
            int index = m_dropdownList.IndexOf(DropDownList.options[DropDownList.value].text);
            int startIndex = index - 1;
            int range = DropDownList.options.Count;
            DropDownList.ClearOptions();
            DropDownList.AddOptions(m_dropdownList.GetRange(startIndex, range));
        }
        
    }

    void Update(){

        
    }
}
