// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/HexMapControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @HexMapControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @HexMapControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""HexMapControls"",
    ""maps"": [
        {
            ""name"": ""HexMap"",
            ""id"": ""9e211ffe-865e-48a8-824c-d8a7f10e452c"",
            ""actions"": [
                {
                    ""name"": ""ClickCell"",
                    ""type"": ""Button"",
                    ""id"": ""139de23d-a03e-4005-a8f9-66c7054b509f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""AlternativeAction"",
                    ""type"": ""Button"",
                    ""id"": ""93be6812-9df1-4fdc-8ece-a980e827f0e8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""b6d65502-9971-45ae-b482-c8b2a2e2705c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""36b2122f-32d5-48b7-b5d2-c8f433622cfd"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ClickCell"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5d0b3885-c4fe-4378-8c5f-89bcdc32e1c5"",
                    ""path"": ""<Keyboard>/leftAlt"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AlternativeAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""26acceaa-49ba-4722-9542-7a81b9b6dc15"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Camera"",
            ""id"": ""64dc6206-5978-4437-8fc5-881a06f312a5"",
            ""actions"": [
                {
                    ""name"": ""Camera Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""6c69b4f3-668f-4838-b165-f03959a2b100"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CameraZoom"",
                    ""type"": ""Value"",
                    ""id"": ""f45e8463-f437-4eb6-8691-195605099b61"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CameraRotate"",
                    ""type"": ""Button"",
                    ""id"": ""ffc7c1e9-93b8-471f-a7d0-dcf508ab805b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""TopDownView"",
                    ""type"": ""Button"",
                    ""id"": ""a045ad98-72d6-4791-941c-5e4083efa93c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""d5465cca-2d4f-4d1f-ac0b-a88bc9dba334"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""da6ae152-380b-474a-841e-3706b3a92792"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""91afe073-2870-4924-ae8b-65e8a70d5f23"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""f0a5e48d-4da5-4f12-b082-af16b4778696"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""bc9dfb14-862e-40cd-8210-f0c9202d1144"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""03813d2f-5922-499a-a63d-62e119061d2f"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraZoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7af0fce5-7801-4685-b610-eec65a201b8e"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraRotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cba0edfe-7497-464f-848a-e54dc3ab8dd0"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TopDownView"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // HexMap
        m_HexMap = asset.FindActionMap("HexMap", throwIfNotFound: true);
        m_HexMap_ClickCell = m_HexMap.FindAction("ClickCell", throwIfNotFound: true);
        m_HexMap_AlternativeAction = m_HexMap.FindAction("AlternativeAction", throwIfNotFound: true);
        m_HexMap_Cancel = m_HexMap.FindAction("Cancel", throwIfNotFound: true);
        // Camera
        m_Camera = asset.FindActionMap("Camera", throwIfNotFound: true);
        m_Camera_CameraMovement = m_Camera.FindAction("Camera Movement", throwIfNotFound: true);
        m_Camera_CameraZoom = m_Camera.FindAction("CameraZoom", throwIfNotFound: true);
        m_Camera_CameraRotate = m_Camera.FindAction("CameraRotate", throwIfNotFound: true);
        m_Camera_TopDownView = m_Camera.FindAction("TopDownView", throwIfNotFound: true);
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

    // HexMap
    private readonly InputActionMap m_HexMap;
    private IHexMapActions m_HexMapActionsCallbackInterface;
    private readonly InputAction m_HexMap_ClickCell;
    private readonly InputAction m_HexMap_AlternativeAction;
    private readonly InputAction m_HexMap_Cancel;
    public struct HexMapActions
    {
        private @HexMapControls m_Wrapper;
        public HexMapActions(@HexMapControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @ClickCell => m_Wrapper.m_HexMap_ClickCell;
        public InputAction @AlternativeAction => m_Wrapper.m_HexMap_AlternativeAction;
        public InputAction @Cancel => m_Wrapper.m_HexMap_Cancel;
        public InputActionMap Get() { return m_Wrapper.m_HexMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(HexMapActions set) { return set.Get(); }
        public void SetCallbacks(IHexMapActions instance)
        {
            if (m_Wrapper.m_HexMapActionsCallbackInterface != null)
            {
                @ClickCell.started -= m_Wrapper.m_HexMapActionsCallbackInterface.OnClickCell;
                @ClickCell.performed -= m_Wrapper.m_HexMapActionsCallbackInterface.OnClickCell;
                @ClickCell.canceled -= m_Wrapper.m_HexMapActionsCallbackInterface.OnClickCell;
                @AlternativeAction.started -= m_Wrapper.m_HexMapActionsCallbackInterface.OnAlternativeAction;
                @AlternativeAction.performed -= m_Wrapper.m_HexMapActionsCallbackInterface.OnAlternativeAction;
                @AlternativeAction.canceled -= m_Wrapper.m_HexMapActionsCallbackInterface.OnAlternativeAction;
                @Cancel.started -= m_Wrapper.m_HexMapActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_HexMapActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_HexMapActionsCallbackInterface.OnCancel;
            }
            m_Wrapper.m_HexMapActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ClickCell.started += instance.OnClickCell;
                @ClickCell.performed += instance.OnClickCell;
                @ClickCell.canceled += instance.OnClickCell;
                @AlternativeAction.started += instance.OnAlternativeAction;
                @AlternativeAction.performed += instance.OnAlternativeAction;
                @AlternativeAction.canceled += instance.OnAlternativeAction;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
            }
        }
    }
    public HexMapActions @HexMap => new HexMapActions(this);

    // Camera
    private readonly InputActionMap m_Camera;
    private ICameraActions m_CameraActionsCallbackInterface;
    private readonly InputAction m_Camera_CameraMovement;
    private readonly InputAction m_Camera_CameraZoom;
    private readonly InputAction m_Camera_CameraRotate;
    private readonly InputAction m_Camera_TopDownView;
    public struct CameraActions
    {
        private @HexMapControls m_Wrapper;
        public CameraActions(@HexMapControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @CameraMovement => m_Wrapper.m_Camera_CameraMovement;
        public InputAction @CameraZoom => m_Wrapper.m_Camera_CameraZoom;
        public InputAction @CameraRotate => m_Wrapper.m_Camera_CameraRotate;
        public InputAction @TopDownView => m_Wrapper.m_Camera_TopDownView;
        public InputActionMap Get() { return m_Wrapper.m_Camera; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CameraActions set) { return set.Get(); }
        public void SetCallbacks(ICameraActions instance)
        {
            if (m_Wrapper.m_CameraActionsCallbackInterface != null)
            {
                @CameraMovement.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnCameraMovement;
                @CameraMovement.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnCameraMovement;
                @CameraMovement.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnCameraMovement;
                @CameraZoom.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnCameraZoom;
                @CameraZoom.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnCameraZoom;
                @CameraZoom.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnCameraZoom;
                @CameraRotate.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnCameraRotate;
                @CameraRotate.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnCameraRotate;
                @CameraRotate.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnCameraRotate;
                @TopDownView.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnTopDownView;
                @TopDownView.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnTopDownView;
                @TopDownView.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnTopDownView;
            }
            m_Wrapper.m_CameraActionsCallbackInterface = instance;
            if (instance != null)
            {
                @CameraMovement.started += instance.OnCameraMovement;
                @CameraMovement.performed += instance.OnCameraMovement;
                @CameraMovement.canceled += instance.OnCameraMovement;
                @CameraZoom.started += instance.OnCameraZoom;
                @CameraZoom.performed += instance.OnCameraZoom;
                @CameraZoom.canceled += instance.OnCameraZoom;
                @CameraRotate.started += instance.OnCameraRotate;
                @CameraRotate.performed += instance.OnCameraRotate;
                @CameraRotate.canceled += instance.OnCameraRotate;
                @TopDownView.started += instance.OnTopDownView;
                @TopDownView.performed += instance.OnTopDownView;
                @TopDownView.canceled += instance.OnTopDownView;
            }
        }
    }
    public CameraActions @Camera => new CameraActions(this);
    public interface IHexMapActions
    {
        void OnClickCell(InputAction.CallbackContext context);
        void OnAlternativeAction(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
    }
    public interface ICameraActions
    {
        void OnCameraMovement(InputAction.CallbackContext context);
        void OnCameraZoom(InputAction.CallbackContext context);
        void OnCameraRotate(InputAction.CallbackContext context);
        void OnTopDownView(InputAction.CallbackContext context);
    }
}
