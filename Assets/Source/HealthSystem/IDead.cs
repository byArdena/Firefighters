using System;

namespace HealthSystem
{
    public interface IDead
    {
        public event Action Dying;
    }
}