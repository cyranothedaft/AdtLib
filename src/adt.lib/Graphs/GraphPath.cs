using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace adt.lib.Graphs {
   public class GraphPath<T> {
      public GraphPath(IEnumerable<GraphEdge<T>> edges) {
         _edges.AddRange(edges);
      }


      private readonly List<GraphEdge<T>> _edges = new List<GraphEdge<T>>();


      public IEnumerable<GraphEdge<T>> Edges {
         get { return _edges; }
      }

      public IEnumerable<GraphVertex<T>> Vertexes {
         get {
            if ( _edges.Count > 0 ) {
               yield return _edges.First().From;
               foreach ( var edge in _edges )
                  yield return edge.To;
            }
         }
      }


      public int Length {
         get { return _edges.Count; }
      }


      public void Add(GraphEdge<T> edge) {
         _edges.Add(edge);
      }
   }
}
