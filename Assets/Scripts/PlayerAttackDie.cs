using UnityEngine;
using Helper;

public class PlayerAttackDie : MonoBehaviour
{
    [SerializeField] private Transform orbit = default;
    [SerializeField] private PlayerInput pInput = default;
    [SerializeField] private GameObject dice = default;
    [SerializeField] private PlayerStats stats = default;
    [SerializeField] private UIManager manager = default;

    private bool chambered = false;
    private int currentMag;
    private Timer fireRateTimer;
    private Timer reloadTimer;
    
    private BulletBehaviour bullet;
    [SerializeField] private float orbitDistance = default;

    private void Start()
    {
        chambered = false;
        currentMag = stats.magSize.value;
        fireRateTimer = new Timer(1f);
        fireRateTimer.SkipTimer();
        reloadTimer = new Timer(1f);
        reloadTimer.SkipTimer();
        InitialChamber();
    }

    private void Update()
    {
        if (UIManager.IsPaused) return;
        Orbit();

        TickTimer();
        if (reloadTimer.TimerFinished())
        {
            manager.UpdateReloadIcons();
            Chamber();
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (chambered)
                    Shoot();
                else if (currentMag <= 0) Reload();
            }
            if (Input.GetKeyDown(KeyCode.R)) Reload();
        }
    }

    private void Orbit() 
    {
        Vector2 inputDirection = pInput.mouseDirection.normalized;

        float angle = (Mathf.Rad2Deg * Mathf.Atan2(inputDirection.y, inputDirection.x)) - 90;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.position = new Vector2(orbit.position.x, orbit.transform.position.y) + inputDirection * orbitDistance;
    }

    private void RollBulletStats() 
    {
        stats.damage.RollStat();
        stats.bulletSpeed.RollStat();
        stats.range.RollStat();
    }

    private void RollCharacterStats() 
    {
        stats.magSize.RollStat();
        stats.moveSpeed.RollStat();
        stats.magReloadTime.RollStat();
        stats.fireRate.RollStat();
        manager.UpdateReloadIcons();
    }

    private void TickTimer() 
    {
        fireRateTimer.TickTimer(Time.deltaTime);
        reloadTimer.TickTimer(Time.deltaTime);
    }

    private void Reload() 
    {
        RollCharacterStats();
        reloadTimer = new Timer(stats.magReloadTime.value);
        manager.UpdateReloadingIcon();
        currentMag = stats.magSize.value;
    }

    private void Chamber() 
    {
        if (chambered) return;
        if (currentMag <= 0) return;
        if (!fireRateTimer.TimerFinished()) return;

        GameObject tempBullet = Instantiate(dice, transform.position, transform.rotation, transform);
        bullet = tempBullet.GetComponent<BulletBehaviour>();
        bullet.GetComponent<Collider2D>().enabled = false;
        bullet.SetStats(stats.damage.value, stats.bulletSpeed.value, stats.range.value + 1);
        chambered = true;

    }

    private void InitialChamber() 
    {
        GameObject tempBullet = Instantiate(dice, transform.position, transform.rotation, transform);
        bullet = tempBullet.GetComponent<BulletBehaviour>();
        bullet.GetComponent<Collider2D>().enabled = false;
        bullet.SetStats(stats.damage.value, stats.bulletSpeed.value, stats.range.value + 1);
        chambered = true;
    }

    private void Shoot() 
    {
        currentMag -= 1;
        chambered = false;
        bullet.GetComponent<Collider2D>().enabled = true;
        bullet.Shoot();

        fireRateTimer = new Timer(CalculateFireRateTimer(stats.fireRate.value));
        manager.UpdateReloadIcons();

        RollBulletStats();
    }

    private float CalculateFireRateTimer(int xAttacksPerSecond) 
    {
        float oneAttackPerXSeconds = 1f / (float)xAttacksPerSecond;
        return oneAttackPerXSeconds;
    }

    public int GetCurrentMag() 
    {
        return currentMag;
    }

    public int GetMaxReloadTimer() 
    {
        return (int)reloadTimer.GetMaxTime();
    }

    public float GetChamberProgress() 
    {
        return fireRateTimer.TimerProgress();
    }

    public float GetReloadProgress()
    {
        return reloadTimer.TimerProgress();
    }
}