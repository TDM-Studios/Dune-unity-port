using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySystem : MonoBehaviour
{
    public Ability[] abilityList;
    private Ability selectedAbility;

    private void Awake()
    {
        foreach(Ability a in abilityList)
        {
            a.owner = this.gameObject;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // En primera instancia se guarda el input del player en una variable privada.
        if(selectedAbility == null) selectedAbility = PlayerInput();

        if(selectedAbility != null) Debug.Log(selectedAbility.name);

        // Comprobamos si la habilidad tiene cargas (charges != 0 [Hay que tener en cuenta el caso de charges = -1 para las habilidades sin cargas]) y si no está en cooldown.
        // Si se cumple con ello, se mostrará el rango de la habilidad.
        // Mientras se muestra, el Update se quedará en standby, esperando el siguiente input del player (Right or Left Mouse Button) para castear o cancelar la acción.
        if(selectedAbility != null)
        {
            if(AbilityReadyCheck(selectedAbility))
            {
                ShowAbilityRange(selectedAbility);
                Debug.Log("Ready to cast or cancel!");
                if(Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Debug.Log("Casting");
                    AbilityCast(selectedAbility);
                }
                else if(Input.GetKeyDown(KeyCode.Mouse1))
                {
                    Debug.Log("Canceling");
                    AbilityCancel();
                }
            }
            else
            {
                // Sonidito / Animación de que la habilidad no se puede lanzar.
                Debug.Log("No charges or ability on cd!\nCooldown: " + (selectedAbility.cooldown - selectedAbility.counter) + "\nCharges:" + selectedAbility.charges);
                selectedAbility = null;
            }
        }

        // Recorremos todas las habilidades del player constantemente para poder detectar si hay alguna en cooldown, y comenzar a contar tiempo en caso de que así sea.
        foreach(Ability a in abilityList)
        {
            if(a.onCooldown)
            {
                CooldownCounter(a);
            }
        }
    }

    private Ability PlayerInput()
    {
        if(Input.GetKeyDown(KeyCode.Q)) return abilityList[0];
        if(Input.GetKeyDown(KeyCode.W)) return abilityList[1];
        if(Input.GetKeyDown(KeyCode.E)) return abilityList[2];
        if(Input.GetKeyDown(KeyCode.R)) return abilityList[3];
        return null;
    }

    private bool AbilityReadyCheck(Ability selected)
    {
        if(selected.charges != 0 && !selected.onCooldown) return true;
        return false;
    }

    private void ShowAbilityRange(Ability selected)
    {
        // Mostrar el rango como se hacía en el juego base.
        if(selected.type == AbilityType.PROJECTILE_TO_GROUND || selected.type == AbilityType.PROJECTILE_TO_TARGET || selected.type == AbilityType.AREA_OF_EFFECT)
        {
            // Mostrar un área (League of Legends like).
        }
        else
        {
            // Cambiar el cursor porque se tratan de habilidades a Melee o summons de minions controlables.
        }

        Debug.Log(selected.range);
    }

    private void AbilityCast(Ability selected)
    {
        GameObject abilityPrefab;
        // Pillamos la info necesaria para poder enviarsela a la invocación de la habilidad.
        switch(selected.type)
        {
            case AbilityType.PROJECTILE_TO_TARGET:
                // Raycast to target (Enemy/Ally tag). Storage at a variable.
                // Take in account the animations! (if(animation.HasFinished()))
                // abilityPrefab = GameObject.Instantiate(selected.prefab, selected.owner.transform.position, Quaternion.identity);
                PutAbilityOnCooldown(selected);
                break;
            case AbilityType.MELEE_TO_TARGET:
                // Raycast to target (Enemy/Ally tag). Displace the player to the target. When arriving, cast the ability.
                // Take in account the animations! (if(animation.HasFinished()))
                // abilityPrefab = GameObject.Instantiate(selected.prefab, selected.owner.transform.position, Quaternion.identity);
                PutAbilityOnCooldown(selected);
                break;
            case AbilityType.PROJECTILE_TO_GROUND:
                // Raycast to target (Ground tag). Also storage the position where the ray collided. Storage at 2 variables.
                // Take in account the animations! (if(animation.HasFinished()))
                // abilityPrefab = GameObject.Instantiate(selected.prefab, selected.owner.transform.position, Quaternion.identity);
                PutAbilityOnCooldown(selected);
                break;
            default:
                // Take in account the animations! (if(animation.HasFinished()))
                // abilityPrefab = GameObject.Instantiate(selected.prefab, selected.owner.transform.position, Quaternion.identity);
                PutAbilityOnCooldown(selected);
                break;
        }

        Debug.Log("Casted Ability " + selected.name + "!");
    }

    private void AbilityCancel()
    {
        selectedAbility = null;
    }

    private void PutAbilityOnCooldown(Ability selected)
    {
        foreach(Ability a in abilityList)
        {
            if(selected == a)
            {
                a.onCooldown = true;
                if(a.charges > 0)
                {
                    a.charges -= 1;
                }
                selectedAbility = null;
            }
        }
    }

    private void CooldownCounter(Ability a)
    {
        a.counter += Time.deltaTime;
        if (a.counter >= a.cooldown)
        {
            a.onCooldown = false;
            a.counter = 0f;
        }
    }
}
