using UnityEngine;

[System.Serializable]
public class Base_WeaponBehaviour
{
    protected CharacterController2D m_character;
    public void SetCharacter(CharacterController2D myCharacter)
    {
        m_character = myCharacter;
    }
    public virtual void SetConfiguration(Base_WeaponConfiguration baseConfiguration)
    {

    }
    public virtual void Attack()
    {

    }
}
