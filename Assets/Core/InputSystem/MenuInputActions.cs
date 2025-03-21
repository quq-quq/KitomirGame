//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.11.2
//     from Assets/Core/InputSystem/MenuInputActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @MenuInputActions: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @MenuInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MenuInputActions"",
    ""maps"": [
        {
            ""name"": ""Navigation"",
            ""id"": ""0d9353be-f0e1-440f-b771-ff216035325a"",
            ""actions"": [
                {
                    ""name"": ""UpSwitch"",
                    ""type"": ""Button"",
                    ""id"": ""c25f869e-a5f0-4f14-868d-a2f707df208d"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""DownSwitch"",
                    ""type"": ""Button"",
                    ""id"": ""d5af1dc9-a2b1-4b9a-94c0-7f2efaa69fd5"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PressButton"",
                    ""type"": ""Button"",
                    ""id"": ""f1dea11b-3f7a-4ca6-a2d8-dda727774357"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""faa0c548-6528-4364-871f-4b66d2b833cb"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UpSwitch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""94f64962-765e-41aa-b8e0-1a8b0df2e8d9"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UpSwitch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""09348631-38b8-4c87-8f9c-52af1c6c4dce"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DownSwitch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a64fb2e8-e122-4182-a6e4-e2e7ff08c348"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DownSwitch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""76fa8b4a-3513-410e-80b3-0a66fc4f1b7c"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PressButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4bb4270f-6913-42ab-bb8a-756d7b75a00d"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PressButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Navigation
        m_Navigation = asset.FindActionMap("Navigation", throwIfNotFound: true);
        m_Navigation_UpSwitch = m_Navigation.FindAction("UpSwitch", throwIfNotFound: true);
        m_Navigation_DownSwitch = m_Navigation.FindAction("DownSwitch", throwIfNotFound: true);
        m_Navigation_PressButton = m_Navigation.FindAction("PressButton", throwIfNotFound: true);
    }

    ~@MenuInputActions()
    {
        UnityEngine.Debug.Assert(!m_Navigation.enabled, "This will cause a leak and performance issues, MenuInputActions.Navigation.Disable() has not been called.");
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Navigation
    private readonly InputActionMap m_Navigation;
    private List<INavigationActions> m_NavigationActionsCallbackInterfaces = new List<INavigationActions>();
    private readonly InputAction m_Navigation_UpSwitch;
    private readonly InputAction m_Navigation_DownSwitch;
    private readonly InputAction m_Navigation_PressButton;
    public struct NavigationActions
    {
        private @MenuInputActions m_Wrapper;
        public NavigationActions(@MenuInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @UpSwitch => m_Wrapper.m_Navigation_UpSwitch;
        public InputAction @DownSwitch => m_Wrapper.m_Navigation_DownSwitch;
        public InputAction @PressButton => m_Wrapper.m_Navigation_PressButton;
        public InputActionMap Get() { return m_Wrapper.m_Navigation; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(NavigationActions set) { return set.Get(); }
        public void AddCallbacks(INavigationActions instance)
        {
            if (instance == null || m_Wrapper.m_NavigationActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_NavigationActionsCallbackInterfaces.Add(instance);
            @UpSwitch.started += instance.OnUpSwitch;
            @UpSwitch.performed += instance.OnUpSwitch;
            @UpSwitch.canceled += instance.OnUpSwitch;
            @DownSwitch.started += instance.OnDownSwitch;
            @DownSwitch.performed += instance.OnDownSwitch;
            @DownSwitch.canceled += instance.OnDownSwitch;
            @PressButton.started += instance.OnPressButton;
            @PressButton.performed += instance.OnPressButton;
            @PressButton.canceled += instance.OnPressButton;
        }

        private void UnregisterCallbacks(INavigationActions instance)
        {
            @UpSwitch.started -= instance.OnUpSwitch;
            @UpSwitch.performed -= instance.OnUpSwitch;
            @UpSwitch.canceled -= instance.OnUpSwitch;
            @DownSwitch.started -= instance.OnDownSwitch;
            @DownSwitch.performed -= instance.OnDownSwitch;
            @DownSwitch.canceled -= instance.OnDownSwitch;
            @PressButton.started -= instance.OnPressButton;
            @PressButton.performed -= instance.OnPressButton;
            @PressButton.canceled -= instance.OnPressButton;
        }

        public void RemoveCallbacks(INavigationActions instance)
        {
            if (m_Wrapper.m_NavigationActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(INavigationActions instance)
        {
            foreach (var item in m_Wrapper.m_NavigationActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_NavigationActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public NavigationActions @Navigation => new NavigationActions(this);
    public interface INavigationActions
    {
        void OnUpSwitch(InputAction.CallbackContext context);
        void OnDownSwitch(InputAction.CallbackContext context);
        void OnPressButton(InputAction.CallbackContext context);
    }
}
