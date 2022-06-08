using UnityEngine.InputSystem;

namespace Ocs.GameSystem
{
    public class TitleManager : SceneControl
    {
        private InputAction _pressAnyKeyAction = 
                new InputAction(type: InputActionType.PassThrough, binding: "*/<Button>", interactions: "Press");

        private void OnEnable() => _pressAnyKeyAction.Enable();
        private void OnDisable() => _pressAnyKeyAction.Disable();

        void Update()
        {
            if(_pressAnyKeyAction.triggered)
            {
                SwitchScene();
            }
        }
    }
}//end namespace Ocs.GameSystem
