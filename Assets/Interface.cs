using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interface : MonoBehaviour
{
    public interface IAttackable
    {
        void TakeDamage(float amount);
        bool IsAlive { get; }
        Team TeamId { get; }
        Vector3 Position { get; }
    }

}
