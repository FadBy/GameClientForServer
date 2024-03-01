using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Characters;
using GameCreator.Runtime.Characters.IK;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.VisualScripting
{
    [Version(0, 1, 1)]

    [Title("Stop Looking At")]
    [Description("Stops looking at a target using the Look At IK system")]

    [Category("Characters/IK/Stop Looking At")]

    [Parameter("Character", "The character target")]
    [Parameter("Target", "The targeted Transform to look at")]
    [Parameter("Layer", "The priority of this IK over other Look At attempts")]

    [Keywords("Inverse", "Kinematics", "IK")]
    [Image(typeof(IconEye), ColorTheme.Type.Blue, typeof(OverlayCross))]

    [Serializable]
    public class InstructionCharacterIKLookStop : Instruction
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField] private PropertyGetGameObject m_Character = GetGameObjectPlayer.Create();
        [SerializeField] private PropertyGetGameObject m_Target = GetGameObjectInstance.Create();
        [SerializeField] private int m_Priority = 1;
        
        // PROPERTIES: ----------------------------------------------------------------------------
        
        public override string Title => $"{this.m_Character} stop look at {this.m_Target}";

        // RUN METHOD: ----------------------------------------------------------------------------
        
        protected override Task Run(Args args)
        {
            Character character = this.m_Character.Get<Character>(args);
            if (character == null) return DefaultResult;

            Transform target = this.m_Target.Get<Transform>(args);
            if (target == null || !character.IK.HasRig<RigLookTrack>()) return DefaultResult;

            LookTrackTransform track = new LookTrackTransform(
                this.m_Priority, 
                target,
                Vector3.zero
            );

            character.IK.GetRig<RigLookTrack>().RemoveTarget(track);
            return DefaultResult;
        }
    }
}