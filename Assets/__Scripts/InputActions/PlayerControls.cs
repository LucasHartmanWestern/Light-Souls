//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/__Scripts/InputActions/PlayerControls.inputactions
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

public partial class @PlayerControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""PlayerMovement"",
            ""id"": ""5aae012b-bbe1-4791-92e9-52660e6ffc1a"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""85644ba0-c73f-433d-8879-c8f402ae107e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Camera"",
                    ""type"": ""PassThrough"",
                    ""id"": ""40000f71-b809-494d-9042-032260d50c4f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""7c22765d-6c3f-423c-9d61-857a44c9d868"",
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
                    ""id"": ""3d5a985e-f79e-49fc-8a91-6000b4c7732c"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""e2b17b3f-781e-410b-bcf9-835a8685d895"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""dfacd09f-fa1e-42dd-957a-bd5e91b5ad26"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""86e15f72-3f56-4dc7-8a6a-7616256ac819"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""59b2860b-7f67-40d1-9fdc-20fa489148d1"",
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
                    ""id"": ""c27bddfc-acd2-420d-b58d-3d124dc1fe6e"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""56f9e440-d5c9-4433-a6c2-46be5975a027"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""cc0c4bc0-5ec2-4ccd-8274-23f73beb3c78"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""501b4a92-dcf0-4f1f-aea0-38d17d2825c0"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""LeftJoyStick"",
                    ""id"": ""c5fb11df-f938-45ec-9c7e-8d552671ad54"",
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
                    ""id"": ""fe5d8a59-e182-432b-bfcd-510969b67a65"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""9e45a1d9-b185-410b-a3d7-4428b657b271"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""62188785-86ea-4753-89a9-cee242ef9529"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""66e10a12-6f55-4522-9368-132a27f20f72"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""ee2a5461-b82d-4afe-a9b1-2e20d289c0ca"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""RightJoyStick"",
                    ""id"": ""20fff7ec-9c41-4441-85de-6d268971baca"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""18b32ee4-02f0-4897-b2cc-1c3912632cb8"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""65657bea-3979-490f-a143-15b80dbbed0c"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""089e5a42-56ac-47bb-9849-5fd1417ece3e"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""d6813a0b-10ff-48aa-9c9f-6571976a71e1"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""PlayerActions"",
            ""id"": ""775db97e-3faa-462e-8e9f-0b44c00e6fc1"",
            ""actions"": [
                {
                    ""name"": ""SprintButton"",
                    ""type"": ""Button"",
                    ""id"": ""ce3ad605-81ce-4966-b444-0212158c03e9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""JumpButton"",
                    ""type"": ""Button"",
                    ""id"": ""334ea586-1caa-4212-ba1f-a874b4a0ca90"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""AimButton"",
                    ""type"": ""Button"",
                    ""id"": ""3233d14f-5a39-4522-8245-9260bf3de5ce"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""AttackButton"",
                    ""type"": ""Button"",
                    ""id"": ""6e7d89d4-3b50-4f9a-9ac3-94ed2d81a63f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SpecialMoveButton"",
                    ""type"": ""Button"",
                    ""id"": ""4a184746-d71b-4f1e-b199-924fc8b8674d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SpecialAbilityButton"",
                    ""type"": ""Button"",
                    ""id"": ""a3ad236c-a457-4e1b-9bc6-5bdad0461837"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""LockOnButton"",
                    ""type"": ""Button"",
                    ""id"": ""d548ee20-944b-45d6-bd12-b07a1d86144c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""LockOnTargetLeft"",
                    ""type"": ""Button"",
                    ""id"": ""f5981cad-e2bf-465c-880c-7d91bf304727"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""LockOnTargetRight"",
                    ""type"": ""Button"",
                    ""id"": ""efdd8728-70c9-4837-b724-2f51cc9699f7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ReloadButton"",
                    ""type"": ""Button"",
                    ""id"": ""8772c3a4-fe64-4d98-85f0-a01667fd900a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6ed989aa-986c-4d6f-ad9e-e7a7c34e3b43"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SprintButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0721ebd2-85e6-4b4f-83b9-819717802a44"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SprintButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""254dcf01-8785-4b42-9cc0-d5cead6ba4eb"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""JumpButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cb9f6644-158f-4613-bd36-84bdd1438ae6"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""JumpButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""83059b07-9eb7-4e86-9f25-ed27a81dd8bc"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AimButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3688d4cd-e4bd-4749-a822-ba689a85e7ec"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AimButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fdf9876f-06a0-4a7f-8e04-158f75cc60a4"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AttackButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""23964d01-492d-4deb-9856-41ede2c988ec"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AttackButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""00076714-ddfd-49a1-9d01-8164648f9d44"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SpecialMoveButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""de25bf1c-1d94-48ee-8816-f09fe678dcdb"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SpecialMoveButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e480bd16-e815-4988-9367-8e9b3ca9a182"",
                    ""path"": ""<Gamepad>/rightStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LockOnButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3e165254-26e6-48b9-88b0-b7f2affe0a7c"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LockOnButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8c347418-4d8b-4019-8a76-bafb03139047"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LockOnTargetLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e6bc2d67-9ab1-452c-8c5c-2932532bd9af"",
                    ""path"": ""<Mouse>/forwardButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LockOnTargetLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2a34e390-02b4-4969-af2f-539af518bfad"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LockOnTargetRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6c3312ad-47d8-4af5-a2fd-48f18c59b648"",
                    ""path"": ""<Mouse>/backButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LockOnTargetRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fcd5cce7-c82f-4690-a52b-7142e1148091"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SpecialAbilityButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""360497d5-4851-4fb9-9eec-42239c40fd65"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SpecialAbilityButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ff2beb89-02b7-40c7-93f5-74897107ea5d"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ReloadButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e796c468-97c8-492f-b4e6-c8e989e7b321"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ReloadButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerMovement
        m_PlayerMovement = asset.FindActionMap("PlayerMovement", throwIfNotFound: true);
        m_PlayerMovement_Movement = m_PlayerMovement.FindAction("Movement", throwIfNotFound: true);
        m_PlayerMovement_Camera = m_PlayerMovement.FindAction("Camera", throwIfNotFound: true);
        // PlayerActions
        m_PlayerActions = asset.FindActionMap("PlayerActions", throwIfNotFound: true);
        m_PlayerActions_SprintButton = m_PlayerActions.FindAction("SprintButton", throwIfNotFound: true);
        m_PlayerActions_JumpButton = m_PlayerActions.FindAction("JumpButton", throwIfNotFound: true);
        m_PlayerActions_AimButton = m_PlayerActions.FindAction("AimButton", throwIfNotFound: true);
        m_PlayerActions_AttackButton = m_PlayerActions.FindAction("AttackButton", throwIfNotFound: true);
        m_PlayerActions_SpecialMoveButton = m_PlayerActions.FindAction("SpecialMoveButton", throwIfNotFound: true);
        m_PlayerActions_SpecialAbilityButton = m_PlayerActions.FindAction("SpecialAbilityButton", throwIfNotFound: true);
        m_PlayerActions_LockOnButton = m_PlayerActions.FindAction("LockOnButton", throwIfNotFound: true);
        m_PlayerActions_LockOnTargetLeft = m_PlayerActions.FindAction("LockOnTargetLeft", throwIfNotFound: true);
        m_PlayerActions_LockOnTargetRight = m_PlayerActions.FindAction("LockOnTargetRight", throwIfNotFound: true);
        m_PlayerActions_ReloadButton = m_PlayerActions.FindAction("ReloadButton", throwIfNotFound: true);
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

    // PlayerMovement
    private readonly InputActionMap m_PlayerMovement;
    private IPlayerMovementActions m_PlayerMovementActionsCallbackInterface;
    private readonly InputAction m_PlayerMovement_Movement;
    private readonly InputAction m_PlayerMovement_Camera;
    public struct PlayerMovementActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerMovementActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_PlayerMovement_Movement;
        public InputAction @Camera => m_Wrapper.m_PlayerMovement_Camera;
        public InputActionMap Get() { return m_Wrapper.m_PlayerMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerMovementActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerMovementActions instance)
        {
            if (m_Wrapper.m_PlayerMovementActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovement;
                @Camera.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCamera;
                @Camera.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCamera;
                @Camera.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCamera;
            }
            m_Wrapper.m_PlayerMovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Camera.started += instance.OnCamera;
                @Camera.performed += instance.OnCamera;
                @Camera.canceled += instance.OnCamera;
            }
        }
    }
    public PlayerMovementActions @PlayerMovement => new PlayerMovementActions(this);

    // PlayerActions
    private readonly InputActionMap m_PlayerActions;
    private IPlayerActionsActions m_PlayerActionsActionsCallbackInterface;
    private readonly InputAction m_PlayerActions_SprintButton;
    private readonly InputAction m_PlayerActions_JumpButton;
    private readonly InputAction m_PlayerActions_AimButton;
    private readonly InputAction m_PlayerActions_AttackButton;
    private readonly InputAction m_PlayerActions_SpecialMoveButton;
    private readonly InputAction m_PlayerActions_SpecialAbilityButton;
    private readonly InputAction m_PlayerActions_LockOnButton;
    private readonly InputAction m_PlayerActions_LockOnTargetLeft;
    private readonly InputAction m_PlayerActions_LockOnTargetRight;
    private readonly InputAction m_PlayerActions_ReloadButton;
    public struct PlayerActionsActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerActionsActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @SprintButton => m_Wrapper.m_PlayerActions_SprintButton;
        public InputAction @JumpButton => m_Wrapper.m_PlayerActions_JumpButton;
        public InputAction @AimButton => m_Wrapper.m_PlayerActions_AimButton;
        public InputAction @AttackButton => m_Wrapper.m_PlayerActions_AttackButton;
        public InputAction @SpecialMoveButton => m_Wrapper.m_PlayerActions_SpecialMoveButton;
        public InputAction @SpecialAbilityButton => m_Wrapper.m_PlayerActions_SpecialAbilityButton;
        public InputAction @LockOnButton => m_Wrapper.m_PlayerActions_LockOnButton;
        public InputAction @LockOnTargetLeft => m_Wrapper.m_PlayerActions_LockOnTargetLeft;
        public InputAction @LockOnTargetRight => m_Wrapper.m_PlayerActions_LockOnTargetRight;
        public InputAction @ReloadButton => m_Wrapper.m_PlayerActions_ReloadButton;
        public InputActionMap Get() { return m_Wrapper.m_PlayerActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActionsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActionsActions instance)
        {
            if (m_Wrapper.m_PlayerActionsActionsCallbackInterface != null)
            {
                @SprintButton.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnSprintButton;
                @SprintButton.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnSprintButton;
                @SprintButton.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnSprintButton;
                @JumpButton.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnJumpButton;
                @JumpButton.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnJumpButton;
                @JumpButton.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnJumpButton;
                @AimButton.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnAimButton;
                @AimButton.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnAimButton;
                @AimButton.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnAimButton;
                @AttackButton.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnAttackButton;
                @AttackButton.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnAttackButton;
                @AttackButton.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnAttackButton;
                @SpecialMoveButton.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnSpecialMoveButton;
                @SpecialMoveButton.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnSpecialMoveButton;
                @SpecialMoveButton.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnSpecialMoveButton;
                @SpecialAbilityButton.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnSpecialAbilityButton;
                @SpecialAbilityButton.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnSpecialAbilityButton;
                @SpecialAbilityButton.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnSpecialAbilityButton;
                @LockOnButton.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnLockOnButton;
                @LockOnButton.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnLockOnButton;
                @LockOnButton.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnLockOnButton;
                @LockOnTargetLeft.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnLockOnTargetLeft;
                @LockOnTargetLeft.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnLockOnTargetLeft;
                @LockOnTargetLeft.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnLockOnTargetLeft;
                @LockOnTargetRight.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnLockOnTargetRight;
                @LockOnTargetRight.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnLockOnTargetRight;
                @LockOnTargetRight.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnLockOnTargetRight;
                @ReloadButton.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnReloadButton;
                @ReloadButton.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnReloadButton;
                @ReloadButton.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnReloadButton;
            }
            m_Wrapper.m_PlayerActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @SprintButton.started += instance.OnSprintButton;
                @SprintButton.performed += instance.OnSprintButton;
                @SprintButton.canceled += instance.OnSprintButton;
                @JumpButton.started += instance.OnJumpButton;
                @JumpButton.performed += instance.OnJumpButton;
                @JumpButton.canceled += instance.OnJumpButton;
                @AimButton.started += instance.OnAimButton;
                @AimButton.performed += instance.OnAimButton;
                @AimButton.canceled += instance.OnAimButton;
                @AttackButton.started += instance.OnAttackButton;
                @AttackButton.performed += instance.OnAttackButton;
                @AttackButton.canceled += instance.OnAttackButton;
                @SpecialMoveButton.started += instance.OnSpecialMoveButton;
                @SpecialMoveButton.performed += instance.OnSpecialMoveButton;
                @SpecialMoveButton.canceled += instance.OnSpecialMoveButton;
                @SpecialAbilityButton.started += instance.OnSpecialAbilityButton;
                @SpecialAbilityButton.performed += instance.OnSpecialAbilityButton;
                @SpecialAbilityButton.canceled += instance.OnSpecialAbilityButton;
                @LockOnButton.started += instance.OnLockOnButton;
                @LockOnButton.performed += instance.OnLockOnButton;
                @LockOnButton.canceled += instance.OnLockOnButton;
                @LockOnTargetLeft.started += instance.OnLockOnTargetLeft;
                @LockOnTargetLeft.performed += instance.OnLockOnTargetLeft;
                @LockOnTargetLeft.canceled += instance.OnLockOnTargetLeft;
                @LockOnTargetRight.started += instance.OnLockOnTargetRight;
                @LockOnTargetRight.performed += instance.OnLockOnTargetRight;
                @LockOnTargetRight.canceled += instance.OnLockOnTargetRight;
                @ReloadButton.started += instance.OnReloadButton;
                @ReloadButton.performed += instance.OnReloadButton;
                @ReloadButton.canceled += instance.OnReloadButton;
            }
        }
    }
    public PlayerActionsActions @PlayerActions => new PlayerActionsActions(this);
    public interface IPlayerMovementActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnCamera(InputAction.CallbackContext context);
    }
    public interface IPlayerActionsActions
    {
        void OnSprintButton(InputAction.CallbackContext context);
        void OnJumpButton(InputAction.CallbackContext context);
        void OnAimButton(InputAction.CallbackContext context);
        void OnAttackButton(InputAction.CallbackContext context);
        void OnSpecialMoveButton(InputAction.CallbackContext context);
        void OnSpecialAbilityButton(InputAction.CallbackContext context);
        void OnLockOnButton(InputAction.CallbackContext context);
        void OnLockOnTargetLeft(InputAction.CallbackContext context);
        void OnLockOnTargetRight(InputAction.CallbackContext context);
        void OnReloadButton(InputAction.CallbackContext context);
    }
}
