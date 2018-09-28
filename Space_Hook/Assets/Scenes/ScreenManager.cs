using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

class ScreenManager : MonoBehaviour {

    //Screen to Open automatically at the start of the Scene
    public Animator initallyOpen;   

    //Currently Open Screen
    private Animator m_Open;

    //Hash of the parameter we use to control the transitions
    private int m_OpenParameterId;

    //The Gameobject Selected before we opened the current screen
    //Used when closing a Screen, so we can go back to the button that opened it
    private GameObject m_PreviouslySelected;

    //Animator State and Transition names we need to check against
    const string k_OpenTransitionName = "Open";
    const string k_ClosedStateName = "Closed";

    public void OnEnable()
    {
        //We cache the Hash to the "Open" Parameter, so we can feed to Animator.SetBool.
        m_OpenParameterId = Animator.StringToHash (k_OpenTransitionName);

        //If Set, open the inital screen now.
        if (initiallyOpen == null)
        {
            return;
        }
        OpenPanel(initiallyOpen);
    }
    //Closes the currently open Panel and opens the provided one 
    //It also takes care of handling the navigation, setting the new Selected Element 

    public void OpenPanel(Animator anim)
    {
        if (m_open == anim)
        {
        return;
        }

        //Activate the new Screen hierarchy so we can animate it
        anim.gameObject.SetActive(true);
        //Save the currently selected button that was used to open this Screen. (CloseCurrent will modify it)
        var newPreviouslySelected = EventSystem.current.currentSelectedGameObject;
        //Move the Screen to the front
        anim.transform.SetAsLastSibling();

        CloseCurrent();

        m_PreviouslySelected = newPreviouslySelected;

        //Set the new screen as then open one.
        m_Open = anim;
        //Start the open animation
        m_Open.SetBool(m_OpenParameterId, true);

        //Set an element in the new screen as the new Selected one.
        GameObject go = FindFirstEnabledSelectable(anim.gameObject);
        SetSelected(go);
    }

    //Finds the first Selectable element in the providade hierarchy.
    static GameObject FindFirstEnabledSelectable(GameObject gameObject)
    {
        GameObject go = null;
        var selectables = gameObject.GetComponentsInChildren<Selectable>(true);
        foreach (var selectable in selectables)
        {
            if (selectable.IsActive () && selectable.IsInteractable ())
            {
                go = selectable.gameObject;
                break;
            }

        }
        return go;
    }
        
    //Closes the currently Open Screen 
    //It also takes care of navigation.
    //Reverting selection to the Selectable used before opening the current screen.
    public void CloseCurrent()
    {
        if (m_open == null)
        {
            return;
        }

        //Start the close Animation
        m_Open.SetBool(m_OpenParameterId, false);

        //Reverting selection to the Selectable used before opening the current Screen.
        SetSelected(m_PreviouslySelected);
        //Start Coroutine to disable the hierarchy when closing animation finishes
        StartCoroutine(DisablePanelDeleyed(m_Open));
        //No Screen open.
        m_Open = null;
    }

    //Coroutine that will detect when the closing animation is finished and it will deactivate the hierarchy.
    IEnumerator DisablePanelDeleyed(Animator anim)
    {
        bool closedStateReached = false;
        bool wantToClose = true;
        while (!closedStateReached && wantToClose)
        {
            closedStateReached = anim.GetCurrentAnimatorStateInfo(0).IsName(k_ClosedStateName);

            wantToClose = !anim.GetBool(m_OpenParameterId);

            yield return new WaitForEndOfFrame();
        }

        if (wantToClose)
        {
            anim.gameObject.SetActive(false);
        }
    }

    //Make the provided GameObject Selected
    //When using the mouse/touch we actually want to set it as the previously selected
    //and set nothing as Selected for now.

    private void SetSelected(GameObject go)
    {
        //Selected the GameObject.
        EventSystem.current.SetSelectedGameObject(go);

        //If we are using the keyboard right now, that's all we need to do
        var standaloneInputModule = EventSystem.current.currentInputModule as StandaloneInputModule;
        if (standaloneInputModule != null)
            {
                return;
            }
        //Since we are using a pointer device, we don't want anything selected.
        //But if the user switches to the keyboard, we want to start navigation from the provided game object. 
        //So here we set the current Selected to Null, so the provided GameObject becomes the Last Selected in the EventSystem.
        EventSystem.current.SetSelectedGameObject(null);
    }  
	
}
