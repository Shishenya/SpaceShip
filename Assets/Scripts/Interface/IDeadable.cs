public interface IDeadable
{
    // Handle на уничтожение объейкта
    public void DeathEvent_OnDeath(DeathEventArgs deathEventArgs);

    // Уничтожение объекта
    public void Death(); 
}
