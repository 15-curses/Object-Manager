using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Assets.ObjectManager
{
    public class DinamicManager : MonoBehaviour
    {
        public static DinamicManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        public void ExecuteEvent(
            MovementType movmentType,
            GameObject gameObject,
            Vector3? endV3 = null,
            Vector3? startV3 = null,
            float? speed = null
            )
        {
            if (gameObject == null)
            {
                // ошибку 
                return;
            }
            switch (movmentType)
            {
                case MovementType.PushInDirection: PushInDirectionLogic(gameObject, (Vector3)endV3, startV3); break;
                case MovementType.Teleport: TeleportLogic(gameObject, (Vector3)endV3); break;
                case MovementType.MoveToPosition: MoveToPositionLogic(gameObject, (Vector3)endV3, (float)speed); break;
            }
        }
        #region Методы для управления физикой
        private Rigidbody RigidbodyOfThis(GameObject gameObject) => gameObject.GetComponent<Rigidbody>();

        private GameObject TeleportLogic(GameObject gameObject, Vector3 newPosition)
        {
            gameObject.transform.position = newPosition;
            return gameObject;
        }

        #region [Некинематик]
        private void PushInDirectionLogic(GameObject gameObject, Vector3 endV3, Vector3? startV3 = null)
        {
            if (startV3 != null)
            {
                gameObject = TeleportLogic(gameObject, (Vector3)startV3);
            }

            var rb = RigidbodyOfThis(gameObject);

            if (rb == null)
            {
                // ошибку
                return;
            }
            rb.AddForce(endV3, ForceMode.Impulse);
        }

        #endregion

        #region [Кинематик]
        private void MoveToPositionLogic(GameObject gameObject, Vector3 endV3, float speed)
        {
            if (gameObject == null) return;

            gameObject.transform.position = Vector3.MoveTowards(
                gameObject.transform.position,
                endV3,
                speed * Time.deltaTime
            );
        }
        #endregion
        #endregion
    }
}