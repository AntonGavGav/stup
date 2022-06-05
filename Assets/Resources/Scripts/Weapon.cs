namespace Resources.Scripts
{
    public class Weapon
    {
        public float damage { get; set; }
        public float reloadingSpeed { get; set; }
        public string name { get; set; }

        public Weapon(string name, float damage, float reloadingSpeed)
        {
            name = this.name;
            damage = this.damage;
            reloadingSpeed = this.reloadingSpeed;
        }
    }
}