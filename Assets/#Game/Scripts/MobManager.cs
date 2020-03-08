using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityDLL;

public class MobManager : SingletonMonoBehaviour<MobManager>
{
    [SerializeField]
    Mob mobPrefab = null;


    List<Mob> mobs = new List<Mob>();

    public int GetRemainingMobs()
    {
        return mobs.Count;
    }

    public Mob Create()
    {
        var mob = Instantiate(mobPrefab);
        mobs.Add(mob);

        return mob;
    }

    public void DestroyAttackedMob(List<int> instanceIdList)
    {
        foreach (var id in instanceIdList)
        {
            foreach (var mob in mobs)
            {
                if (!mob.gameObject.GetInstanceID().Equals(id))
                    continue;

                EventManager.BroadcastChangeMobState(eMobStateType.Dead, id);
            }
        }
    }

    public void Remove(Mob mob)
    {
        Destroy(mob.gameObject, 1f);
        mobs.Remove(mob);
    }

    public void Remove(int instanceId)
    {
        var mob = mobs.Find(element => element.gameObject.GetInstanceID().Equals(instanceId));
        if (!mob)
            return;

        Destroy(mob.gameObject, 1f);
        mobs.Remove(mob);
    }
}
