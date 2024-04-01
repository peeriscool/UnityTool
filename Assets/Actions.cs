//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.3
//     from Assets/Actions.inputactions
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

public partial class @Actions: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Actions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Actions"",
    ""maps"": [
        {
            ""name"": ""InputMapping"",
            ""id"": ""505d70c6-9119-4ae7-b48b-1b638b6687d7"",
            ""actions"": [
                {
                    ""name"": ""Menucontrolls"",
                    ""type"": ""Button"",
                    ""id"": ""85ab34a3-5274-4087-9d34-c66881803f96"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""89cdab22-5dd7-44ef-9172-dfcc251e15ae"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Letters"",
                    ""type"": ""Button"",
                    ""id"": ""5e615418-51f1-4485-a56b-c3e130d0ff59"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Mouse"",
                    ""type"": ""Button"",
                    ""id"": ""a567fe36-4006-48b2-8ca4-960a724c50c7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Shortcuts"",
                    ""type"": ""Button"",
                    ""id"": ""30823afc-e093-4c78-ac60-ea5cddd7339b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Scroll"",
                    ""type"": ""PassThrough"",
                    ""id"": ""db346e90-7df7-461c-802b-9a8f0651f422"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d3a6dfe0-c1df-4618-862f-c3f23728fe0e"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Menucontrolls"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""16b2e546-fbce-4851-8bd1-0429fbf92a74"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Menucontrolls"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cafc9e50-b7c1-4dc1-9ccb-c4564d6bcd42"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Menucontrolls"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bc9838c3-de4e-45c8-bc14-0cd6ced72edb"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Menucontrolls"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""52b29b2a-d245-48de-bfdd-87c881eef8a7"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3058c3c2-a1e3-4a86-98b5-fbb91305eee2"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0474086f-e07b-4db1-b3c6-c7fdf7302eeb"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5146637d-dcf3-4231-baaa-526ad7ea4fe8"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""495df7cb-929a-4204-ace3-d61eec1ed11c"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Letters"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c14307c5-12b2-475c-a464-be607b10e25a"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Letters"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d068698b-5026-4d9b-a980-0ecc57cfb88e"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Letters"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aaa8b444-0f7a-4d9d-835e-c4273fd0058b"",
                    ""path"": ""<Keyboard>/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Letters"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""56786544-9427-427d-a13a-8eb4111e0b91"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Letters"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8ab2933e-de27-432b-87f0-b02ee33d217f"",
                    ""path"": ""<Keyboard>/v"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Letters"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""73374997-2747-4c1d-ae83-197c27aa0b76"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Letters"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""df9f85d6-fb82-4c05-95e6-487624d9f645"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6b0dc301-6314-4ee1-ba88-af6d96d0925a"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a2c6ebda-2c93-4fc9-8c13-2bf562479714"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""61ba5a3b-e2f0-4d27-bb16-e3c4fdb6d646"",
                    ""path"": ""<Mouse>/forwardButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""84b6634c-b222-4970-a296-1a6b499cf2b1"",
                    ""path"": ""<Mouse>/backButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""94a395a5-6fd3-4eef-b713-20b83e8db021"",
                    ""path"": ""<Keyboard>/f1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shortcuts"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""59f25567-acab-497f-a22d-6aca93669123"",
                    ""path"": ""<Keyboard>/f2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shortcuts"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d4ec4418-df80-4d6e-b44f-a02278e82e7d"",
                    ""path"": ""<Keyboard>/f3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shortcuts"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8b406093-6199-43b1-9f59-66458dac77b1"",
                    ""path"": ""<Keyboard>/f4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shortcuts"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f83288ba-e3e1-4143-9a1d-354626892942"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Scroll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // InputMapping
        m_InputMapping = asset.FindActionMap("InputMapping", throwIfNotFound: true);
        m_InputMapping_Menucontrolls = m_InputMapping.FindAction("Menucontrolls", throwIfNotFound: true);
        m_InputMapping_Movement = m_InputMapping.FindAction("Movement", throwIfNotFound: true);
        m_InputMapping_Letters = m_InputMapping.FindAction("Letters", throwIfNotFound: true);
        m_InputMapping_Mouse = m_InputMapping.FindAction("Mouse", throwIfNotFound: true);
        m_InputMapping_Shortcuts = m_InputMapping.FindAction("Shortcuts", throwIfNotFound: true);
        m_InputMapping_Scroll = m_InputMapping.FindAction("Scroll", throwIfNotFound: true);
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

    // InputMapping
    private readonly InputActionMap m_InputMapping;
    private List<IInputMappingActions> m_InputMappingActionsCallbackInterfaces = new List<IInputMappingActions>();
    private readonly InputAction m_InputMapping_Menucontrolls;
    private readonly InputAction m_InputMapping_Movement;
    private readonly InputAction m_InputMapping_Letters;
    private readonly InputAction m_InputMapping_Mouse;
    private readonly InputAction m_InputMapping_Shortcuts;
    private readonly InputAction m_InputMapping_Scroll;
    public struct InputMappingActions
    {
        private @Actions m_Wrapper;
        public InputMappingActions(@Actions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Menucontrolls => m_Wrapper.m_InputMapping_Menucontrolls;
        public InputAction @Movement => m_Wrapper.m_InputMapping_Movement;
        public InputAction @Letters => m_Wrapper.m_InputMapping_Letters;
        public InputAction @Mouse => m_Wrapper.m_InputMapping_Mouse;
        public InputAction @Shortcuts => m_Wrapper.m_InputMapping_Shortcuts;
        public InputAction @Scroll => m_Wrapper.m_InputMapping_Scroll;
        public InputActionMap Get() { return m_Wrapper.m_InputMapping; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InputMappingActions set) { return set.Get(); }
        public void AddCallbacks(IInputMappingActions instance)
        {
            if (instance == null || m_Wrapper.m_InputMappingActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_InputMappingActionsCallbackInterfaces.Add(instance);
            @Menucontrolls.started += instance.OnMenucontrolls;
            @Menucontrolls.performed += instance.OnMenucontrolls;
            @Menucontrolls.canceled += instance.OnMenucontrolls;
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
            @Letters.started += instance.OnLetters;
            @Letters.performed += instance.OnLetters;
            @Letters.canceled += instance.OnLetters;
            @Mouse.started += instance.OnMouse;
            @Mouse.performed += instance.OnMouse;
            @Mouse.canceled += instance.OnMouse;
            @Shortcuts.started += instance.OnShortcuts;
            @Shortcuts.performed += instance.OnShortcuts;
            @Shortcuts.canceled += instance.OnShortcuts;
            @Scroll.started += instance.OnScroll;
            @Scroll.performed += instance.OnScroll;
            @Scroll.canceled += instance.OnScroll;
        }

        private void UnregisterCallbacks(IInputMappingActions instance)
        {
            @Menucontrolls.started -= instance.OnMenucontrolls;
            @Menucontrolls.performed -= instance.OnMenucontrolls;
            @Menucontrolls.canceled -= instance.OnMenucontrolls;
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
            @Letters.started -= instance.OnLetters;
            @Letters.performed -= instance.OnLetters;
            @Letters.canceled -= instance.OnLetters;
            @Mouse.started -= instance.OnMouse;
            @Mouse.performed -= instance.OnMouse;
            @Mouse.canceled -= instance.OnMouse;
            @Shortcuts.started -= instance.OnShortcuts;
            @Shortcuts.performed -= instance.OnShortcuts;
            @Shortcuts.canceled -= instance.OnShortcuts;
            @Scroll.started -= instance.OnScroll;
            @Scroll.performed -= instance.OnScroll;
            @Scroll.canceled -= instance.OnScroll;
        }

        public void RemoveCallbacks(IInputMappingActions instance)
        {
            if (m_Wrapper.m_InputMappingActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IInputMappingActions instance)
        {
            foreach (var item in m_Wrapper.m_InputMappingActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_InputMappingActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public InputMappingActions @InputMapping => new InputMappingActions(this);
    public interface IInputMappingActions
    {
        void OnMenucontrolls(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnLetters(InputAction.CallbackContext context);
        void OnMouse(InputAction.CallbackContext context);
        void OnShortcuts(InputAction.CallbackContext context);
        void OnScroll(InputAction.CallbackContext context);
    }
}
