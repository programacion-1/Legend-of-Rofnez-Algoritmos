using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.UI;

namespace RPG.Obstacle
{
    public class ArenaObstacle : MonoBehaviour
    {
        [SerializeField] Transform door;
        [SerializeField] float newYRotation;
        [SerializeField] float rotationSpeed;
        [SerializeField] bool _hasPlayerPickedUpTheKey = false;
        [SerializeField] bool _hasPlayerEnteredTheArena = false;
        EventText eventText;

        public void SetEventText()
        {
            eventText = GameObject.FindObjectOfType<EventText>();
        }
        
        void Update()
        {
            if(_hasPlayerPickedUpTheKey && !_hasPlayerEnteredTheArena)
            {
                Quaternion worldRotation = transform.rotation * door.rotation;
                if(worldRotation.y * newYRotation >= newYRotation/8)
                {
                    door.Rotate(new Vector3(0, -1 * rotationSpeed * Time.deltaTime, 0));
                }
            }
            if(_hasPlayerEnteredTheArena)
            {
                Quaternion worldRotation = transform.rotation * door.rotation;
                if(worldRotation.y * newYRotation <= newYRotation/2)
                {
                    door.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
                }
            }
        }
        public void pickUpTheKey()
        {
            _hasPlayerPickedUpTheKey = true;
            eventText.SetEventText("You can enter the Arena and fight the boss!");
        }

        public void EnterArena()
        {
            StartCoroutine("EnterArenaCo");
        }

        IEnumerator EnterArenaCo()
        {
            yield return new WaitForSeconds(1f);
            _hasPlayerEnteredTheArena = true;
        }
    }
}