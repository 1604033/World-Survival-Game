using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBuildingScript : MonoBehaviour
{

                public virtual bool CanConstructBuilding()
                {
                    return false;
                }

                public virtual void CanNowBuild()
                {
                }

                public virtual void ConstructBuilding()
                {
                }

                public virtual void DestroyBuilding()
                {

                }

                public virtual void ConsumeRawMaterials()
                {
                }

                public virtual void UpdateProgress()
                {


                }

                public virtual void AddInstantiatedGameObject()
                {
                }

                public virtual void DeleteRawMaterials()
                {

                }


                public virtual bool ScatterRawMaterials()
                {
                    return false;
                }
        }
