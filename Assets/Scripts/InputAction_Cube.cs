// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/InputAction_Cube.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputAction_Cube : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputAction_Cube()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputAction_Cube"",
    ""maps"": [
        {
            ""name"": ""CubeController"",
            ""id"": ""15fe327c-b7f0-4ca0-a36b-955e34facc8f"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""1732fa3a-964a-4122-9aa2-b55aedc904f0"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Right Lean"",
                    ""type"": ""Button"",
                    ""id"": ""e4bab2e6-56b3-4c76-80fe-0d9ca33f3cc7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                },
                {
                    ""name"": ""Left Lean"",
                    ""type"": ""Button"",
                    ""id"": ""244886d3-d4ae-41eb-b0c2-4915e4515fca"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""b4d30b71-17af-41eb-a036-6aa32c5980d2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Somersult"",
                    ""type"": ""Button"",
                    ""id"": ""86d2c565-471b-435e-b445-d9168611a327"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""bd685c04-f947-4232-b435-eb3e738b4cd8"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""918ebb90-e7cb-4b71-a70f-7f7301600451"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": ""AxisDeadzone"",
                    ""groups"": ""CubeKeyboardInput"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""658540b4-1b7f-412b-896b-095906ab1b4c"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": ""AxisDeadzone"",
                    ""groups"": ""CubeKeyboardInput"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""4e0cff20-a3d0-4840-bbf5-170f9730d4f8"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": ""AxisDeadzone"",
                    ""groups"": ""CubeKeyboardInput"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""0b2166c6-29d4-41ab-91e1-2483efa79fde"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": ""AxisDeadzone"",
                    ""groups"": ""CubeKeyboardInput"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""868bfc5a-fd7f-4c27-9ff0-725db4248777"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""9bf91ef3-8bd3-4c8d-938f-f9fb0d52e9d5"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": ""AxisDeadzone"",
                    ""groups"": ""CubeControllerInput"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""8cd97258-54c8-4860-a622-dd127a1f0235"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": ""AxisDeadzone"",
                    ""groups"": ""CubeControllerInput"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""3548a844-06ec-4eb5-b7e0-fb222963f99b"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": ""AxisDeadzone"",
                    ""groups"": ""CubeControllerInput"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""1ce35326-37e8-4193-b9d8-ca21b66af1a9"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": ""AxisDeadzone"",
                    ""groups"": ""CubeControllerInput"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""b0a2597a-645b-4ea5-970a-33d55859b1f2"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""CubeKeyboardInput"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""16e6e0ac-2250-403e-84c7-dcf4ca973bb1"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""CubeControllerInput"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9d02da05-bc3b-46e0-826f-a647a13c0de2"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardInput;CubeKeyboardInput"",
                    ""action"": ""Left Lean"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2d4265b4-8c0d-40c2-911c-4ca096a81998"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""CubeControllerInput"",
                    ""action"": ""Left Lean"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""558005a7-bb68-4e72-bbba-a97835e382f6"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardInput;CubeKeyboardInput"",
                    ""action"": ""Right Lean"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3aab5335-156f-4e8e-8a35-68aed0554fa5"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""CubeControllerInput"",
                    ""action"": ""Right Lean"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""305383c8-4244-48c1-a4fb-dabdf008fc2e"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""CubeControllerInput"",
                    ""action"": ""Somersult"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3daf06b4-3f37-4b5f-b93a-f1253b327f88"",
                    ""path"": ""<Keyboard>/#(C)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""CubeKeyboardInput"",
                    ""action"": ""Somersult"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""CubeKeyboardInput"",
            ""bindingGroup"": ""CubeKeyboardInput"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""CubeControllerInput"",
            ""bindingGroup"": ""CubeControllerInput"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // CubeController
        m_CubeController = asset.FindActionMap("CubeController", throwIfNotFound: true);
        m_CubeController_Movement = m_CubeController.FindAction("Movement", throwIfNotFound: true);
        m_CubeController_RightLean = m_CubeController.FindAction("Right Lean", throwIfNotFound: true);
        m_CubeController_LeftLean = m_CubeController.FindAction("Left Lean", throwIfNotFound: true);
        m_CubeController_Fire = m_CubeController.FindAction("Fire", throwIfNotFound: true);
        m_CubeController_Somersult = m_CubeController.FindAction("Somersult", throwIfNotFound: true);
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

    // CubeController
    private readonly InputActionMap m_CubeController;
    private ICubeControllerActions m_CubeControllerActionsCallbackInterface;
    private readonly InputAction m_CubeController_Movement;
    private readonly InputAction m_CubeController_RightLean;
    private readonly InputAction m_CubeController_LeftLean;
    private readonly InputAction m_CubeController_Fire;
    private readonly InputAction m_CubeController_Somersult;
    public struct CubeControllerActions
    {
        private @InputAction_Cube m_Wrapper;
        public CubeControllerActions(@InputAction_Cube wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_CubeController_Movement;
        public InputAction @RightLean => m_Wrapper.m_CubeController_RightLean;
        public InputAction @LeftLean => m_Wrapper.m_CubeController_LeftLean;
        public InputAction @Fire => m_Wrapper.m_CubeController_Fire;
        public InputAction @Somersult => m_Wrapper.m_CubeController_Somersult;
        public InputActionMap Get() { return m_Wrapper.m_CubeController; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CubeControllerActions set) { return set.Get(); }
        public void SetCallbacks(ICubeControllerActions instance)
        {
            if (m_Wrapper.m_CubeControllerActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_CubeControllerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_CubeControllerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_CubeControllerActionsCallbackInterface.OnMovement;
                @RightLean.started -= m_Wrapper.m_CubeControllerActionsCallbackInterface.OnRightLean;
                @RightLean.performed -= m_Wrapper.m_CubeControllerActionsCallbackInterface.OnRightLean;
                @RightLean.canceled -= m_Wrapper.m_CubeControllerActionsCallbackInterface.OnRightLean;
                @LeftLean.started -= m_Wrapper.m_CubeControllerActionsCallbackInterface.OnLeftLean;
                @LeftLean.performed -= m_Wrapper.m_CubeControllerActionsCallbackInterface.OnLeftLean;
                @LeftLean.canceled -= m_Wrapper.m_CubeControllerActionsCallbackInterface.OnLeftLean;
                @Fire.started -= m_Wrapper.m_CubeControllerActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_CubeControllerActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_CubeControllerActionsCallbackInterface.OnFire;
                @Somersult.started -= m_Wrapper.m_CubeControllerActionsCallbackInterface.OnSomersult;
                @Somersult.performed -= m_Wrapper.m_CubeControllerActionsCallbackInterface.OnSomersult;
                @Somersult.canceled -= m_Wrapper.m_CubeControllerActionsCallbackInterface.OnSomersult;
            }
            m_Wrapper.m_CubeControllerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @RightLean.started += instance.OnRightLean;
                @RightLean.performed += instance.OnRightLean;
                @RightLean.canceled += instance.OnRightLean;
                @LeftLean.started += instance.OnLeftLean;
                @LeftLean.performed += instance.OnLeftLean;
                @LeftLean.canceled += instance.OnLeftLean;
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
                @Somersult.started += instance.OnSomersult;
                @Somersult.performed += instance.OnSomersult;
                @Somersult.canceled += instance.OnSomersult;
            }
        }
    }
    public CubeControllerActions @CubeController => new CubeControllerActions(this);
    private int m_CubeKeyboardInputSchemeIndex = -1;
    public InputControlScheme CubeKeyboardInputScheme
    {
        get
        {
            if (m_CubeKeyboardInputSchemeIndex == -1) m_CubeKeyboardInputSchemeIndex = asset.FindControlSchemeIndex("CubeKeyboardInput");
            return asset.controlSchemes[m_CubeKeyboardInputSchemeIndex];
        }
    }
    private int m_CubeControllerInputSchemeIndex = -1;
    public InputControlScheme CubeControllerInputScheme
    {
        get
        {
            if (m_CubeControllerInputSchemeIndex == -1) m_CubeControllerInputSchemeIndex = asset.FindControlSchemeIndex("CubeControllerInput");
            return asset.controlSchemes[m_CubeControllerInputSchemeIndex];
        }
    }
    public interface ICubeControllerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnRightLean(InputAction.CallbackContext context);
        void OnLeftLean(InputAction.CallbackContext context);
        void OnFire(InputAction.CallbackContext context);
        void OnSomersult(InputAction.CallbackContext context);
    }
}
