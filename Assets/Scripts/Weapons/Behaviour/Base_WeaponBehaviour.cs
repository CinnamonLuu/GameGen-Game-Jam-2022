using UnityEngine;

[System.Serializable]
public class Base_WeaponBehaviour
{
    protected CharacterController2D m_character;
    protected CombatConfiguration m_characterCombatConfiguration;
    public void SetCharacter(CharacterController2D myCharacter)
    {
        m_character = myCharacter;
    }
    public void SetCombatConfiguration(CombatConfiguration newCombatConfig)
    {
        m_characterCombatConfiguration = newCombatConfig;
    }
    public virtual void SetConfiguration(Base_WeaponConfiguration baseConfiguration)
    {

    }
    public virtual void Attack()
    {

    }
}
