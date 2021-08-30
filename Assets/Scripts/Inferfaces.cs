using UnityEngine;

public interface IDamagable
{
    void SetDamage(int damage);

    void Destroy();
}

public interface IUpgradeItem
{
    void UpgradeSmartBomb();

    void HealItem(int healValue);

    void AddLife();
}
public interface IDetectable
{
    Transform GetTransform();

    void ShowArrow(bool state);
}

public interface IReflect
{
    bool IsReflective();
}

public interface IStageLimit
{
    void DOUTurn(Transform direction);
}

public interface IUpgradeWeapon
{
    void UpgradeWeapon();
}

public class ReadOnlyAttribute : PropertyAttribute { }