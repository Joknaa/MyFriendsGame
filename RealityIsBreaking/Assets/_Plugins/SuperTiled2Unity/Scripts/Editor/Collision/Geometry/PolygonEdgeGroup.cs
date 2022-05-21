using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SuperTiled2Unity.Editor.Geometry {
    // Keeps a collection of polygon edges that are shared bewteen two polygons
    // Assumes that all polygons have CCW winding to them
    public class PolygonEdgeGroup {
        public List<PolygonEdge> PolygonEdges { get; set; }

        public void Initialize(List<Vector2[]> polygons) {
            PolygonEdges = new List<PolygonEdge>();

            var polygonId = 0;
            foreach (var polygon in polygons) {
                // Our polygon will be added to each edge
                var compPolygon = new CompositionPolygon(polygon, polygonId++);

                // Process all edges of the polygon
                for (int p = polygon.Length - 1, q = 0; q < polygon.Length; p = q++) {
                    var P = polygon[p];
                    var Q = polygon[q];

                    // The clockwise edge may already exist if it was added by an earlier polygon as the counter-clockwise edge
                    // If so, add this polygon as the CW partner of that edge
                    var edge = PolygonEdges.FirstOrDefault(e => e.P == Q && e.Q == P);
                    if (edge != null) {
                        // Add ourselves as the Minor/CW partner
                        edge.AssignMinorPartner(compPolygon);
                        compPolygon.AddEdge(edge);
                    }
                    else {
                        // If this edge is new to the collection then add it with this polygon being the CCW partner
                        var newEdge = new PolygonEdge(compPolygon, p);
                        compPolygon.AddEdge(newEdge);
                        PolygonEdges.Add(newEdge);
                    }
                }
            }
        }
    }
}