using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace Transition
{
    public class Transition : MonoBehaviour
    {
        [SerializeField] private float durationToMove = .5f;
        CinemachineBrain cineBrain;
        CinemachineVirtualCamera cinemachineVirtualCamera;
        private FadeTransition _fadeTransition;
        bool hasAnimationFinished = false;
        private Transform camerafollow;

        void Start()
        {
            cineBrain = FindObjectOfType<CinemachineBrain>();
            _fadeTransition = FindObjectOfType<FadeTransition>();
            cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        }


        public IEnumerator MoveObject(Transform from, Transform to, GameObject objectToMove, Transform center)
        {
            var position = to.position;
            //camerafollow = cinemachineVirtualCamera.Follow ;
            //cinemachineVirtualCamera.Follow = null;
            yield return StartCoroutine(_fadeTransition.FadeOut());
            //cinemachineVirtualCamera.transform.position = position;
            if (objectToMove != null)
                objectToMove.transform.position =
                    new Vector3(to.position.x, to.position.y, objectToMove.transform.position.z);
            // if (center != null)
            // {
            //     SetCameraLookAt(center);
            // }
            // else
            {
                //SetCameraFollow(position, objectToMove.transform);
            }
            yield return new WaitForSeconds(.3f);

            //cinemachineVirtualCamera.Follow = camerafollow;
            yield return StartCoroutine(_fadeTransition.FadeIn());
        }


        private void SetCameraFollow(Vector3 position, Transform objectToMove)
        {
            if (cinemachineVirtualCamera != null && cineBrain != null)
            {
                cineBrain.ActiveVirtualCamera.Follow = objectToMove;
            }
        }

        private void SetCameraLookAt(Transform objectToMove)
        {
            if (cinemachineVirtualCamera != null && cineBrain != null)
            {
                cineBrain.ActiveVirtualCamera.Follow = objectToMove;
            }
        }


        // void SwitchConfiner(CinemachineVirtualCamera cam1, CinemachineVirtualCamera  cam2)
        // {
        //     cam1.Priority = 0;
        //     cam2.Priority = 10;
        // }
    }
}