using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace adt.lib.Graphs.Algorithms {
   public class DepthFirstSearch<T> {
      private readonly Graph<T> _graph;


      internal DepthFirstSearch(Graph<T> graph) {
         _graph = graph;
      }


      private int _t;
      private StringBuilder _searchTrace;

      internal string Trace { get { return _searchTrace.ToString(); } }


      /// <summary>
      /// Performs a comprehensive depth-first search of the graph.  The search is performed recursively.
      /// Returns a spanning tree of the graph, starting at the specified node.
      /// </summary>
      /// <param name="graph"></param>
      /// <returns></returns>
      public static void PerformSearch(Graph<T> graph) {
         new DepthFirstSearch<T>(graph).PerformSearch();
      }


      internal void PerformSearch() {
         _t = 0;
         _searchTrace = new StringBuilder();

         _graph.DfsInitialize();
         foreach ( var vertex in _graph.Vertexes ) {
            if ( vertex.DfsColor == DfsVertexColor.White )
               dfsVisit(vertex);
         }
      }


      private void dfsVisit(GraphNode<T> vertex) {
         vertex.DfsColor = DfsVertexColor.Grey;
         vertex.DfsDiscoveryTime = _t++;
         _searchTrace.AppendFormat("({0}", vertex.Value);

         foreach ( var edge in vertex.EdgesOut ) {
            var v = edge.To;
            if ( v.DfsColor == DfsVertexColor.White )
               dfsVisit(v);
         }

         vertex.DfsColor = DfsVertexColor.Black;
         vertex.DfsFinishingTime = _t++;
         _searchTrace.Append(")");
      }
   }
}
