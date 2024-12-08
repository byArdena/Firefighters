using UnityEngine;

namespace Saves
{
    public class SaveService
    {
        public void Save(IReadOnlyCharacteristics characteristics)
        {
            PlayerPrefs.SetString(nameof(PlayerCharacteristics), JsonUtility.ToJson(characteristics));
            PlayerPrefs.Save();
        }

        public PlayerCharacteristics Load()
        {
            return PlayerPrefs.HasKey(nameof(PlayerCharacteristics)) == false
                ? null
                : JsonUtility.FromJson<PlayerCharacteristics>(PlayerPrefs.GetString(nameof(PlayerCharacteristics)));
        }
    }
}