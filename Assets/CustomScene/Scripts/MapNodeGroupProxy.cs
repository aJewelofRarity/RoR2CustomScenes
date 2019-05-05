using RoR2;
using RoR2.Navigation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

    public class MapNodeGroupProxy : MonoBehaviour
    {
        public Bounds Bounds;
        public float spacing;
        //public NodeGraph nodeGraph;
        public Transform testPointA;
        public Transform testPointB;
        public HullClassification debugHullDef;
        public MapNodeGroup.GraphType graphType;

        public StartUp startupObject;

        private MapNodeGroup mapNodeGroup;

        private void Awake()
        {
            mapNodeGroup = gameObject.AddComponent<MapNodeGroup>();
            mapNodeGroup.testPointA = testPointA;
            mapNodeGroup.testPointB = testPointB;
            mapNodeGroup.debugHullDef = debugHullDef;
            mapNodeGroup.graphType = graphType;

            var nodeGraph = mapNodeGroup.nodeGraph = ScriptableObject.CreateInstance<NodeGraph>();

            var lowValue = transform.position + Bounds.center - Bounds.extents;
            var highValue = transform.position + Bounds.center + Bounds.extents;
            var y = highValue.y;
            var height = highValue.y - lowValue.y;
            for (var x = lowValue.x; x < highValue.x; x += spacing)
                for (var z = lowValue.z; z < highValue.z; z += spacing)
                    if (Physics.Raycast(new Ray(new Vector3(x, y, z), Vector3.down), out RaycastHit hit, height))
                        mapNodeGroup.AddNode(hit.point);

            mapNodeGroup.UpdateNoCeilingMasks();
            mapNodeGroup.UpdateTeleporterMasks();
            mapNodeGroup.Bake(nodeGraph);


            GameObject Scene = new GameObject("SceneInfo");
            //GameObject Scene = GameObject.Find("SceneInfo");
            var sceneInfo = Scene.AddComponent<RoR2.SceneInfo>();
            var classicStageInfo = Scene.AddComponent<RoR2.ClassicStageInfo>();
            sceneInfo.groundNodeGroup = GetComponent<MapNodeGroup>();
            sceneInfo.groundNodes = sceneInfo.groundNodeGroup.nodeGraph;

            //startupObject.sceneInfo.groundNodeGroup = mapNodeGroup;
            //startupObject.sceneInfo.groundNodes = nodeGraph;

            this.enabled = false;

            Destroy(this);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position + Bounds.center, Bounds.size);
        }

        //public implicit operator MapNodeGroup(MapNodeGroupProxy proxy) => mapNodeGroup;
    }
