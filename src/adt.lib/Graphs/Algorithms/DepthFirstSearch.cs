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
      private Stack<GraphEdge<T>> _currentEdgePath; 
      private List<GraphPath<T>> _cycles;


      #region Properties for analyzing results after the search is performed

      public string Trace {
         get { return _searchTrace.ToString().Substring(1) /* omit the initial ' ' char */ ; }
      }

      public IEnumerable<GraphPath<T>> Cycles {
         get { return _cycles; }
      }

      #endregion


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
         _currentEdgePath = new Stack<GraphEdge<T>>();
         _cycles = new List<GraphPath<T>>();

         _graph.DfsInitialize();
         foreach ( var vertex in _graph.Vertexes ) {
            if ( vertex.DfsColor == DfsVertexColor.White )
               dfsVisit(vertex);
         }
      }


      internal IEnumerable<GraphEdge<T>> EdgesOfType(DfsEdgeType edgeType) {
         return _graph.Edges.Where(e => e.DfsType == edgeType);
      }


      private void dfsVisit(GraphVertex<T> vertex) {
         vertex.DfsColor = DfsVertexColor.Grey;
         vertex.DfsDiscoveryTime = _t++;
         _searchTrace.AppendFormat(" ({0}", vertex.Value);

         foreach ( var edge in vertex.EdgesOut ) {
            // handle edge classification and cycle detection
            edge.DfsType = classifyEdge(edge);
            if ( edge.DfsType == DfsEdgeType.Tree ) {
//Console.WriteLine("edge push:  {0}-{1}", edge.From.Value, edge.To.Value);
               _currentEdgePath.Push(edge);
            }
            else if ( edge.DfsType == DfsEdgeType.Back )
               _cycles.Add(getCyclePath(_currentEdgePath, edge));

            // proceed with to-vertex visitation
            var v = edge.To;
            if ( v.DfsColor == DfsVertexColor.White )
               dfsVisit(v);

            if ( edge.DfsType == DfsEdgeType.Tree ) {
//Console.WriteLine("edge pop:   {0}-{1}", edge.From.Value, edge.To.Value);
               _currentEdgePath.Pop();
            }
         }

         vertex.DfsColor = DfsVertexColor.Black;
         vertex.DfsFinishingTime = _t++;
         _searchTrace.AppendFormat(" {0})", vertex.Value);
      }


      private DfsEdgeType classifyEdge(GraphEdge<T> edge) {
         switch ( edge.To.DfsColor ) {
            case DfsVertexColor.White:
               return DfsEdgeType.Tree;

            case DfsVertexColor.Grey:
               return DfsEdgeType.Back;

            case DfsVertexColor.Black:
            default:
               return ( edge.From.DfsDiscoveryTime < edge.To.DfsDiscoveryTime )
                         ? DfsEdgeType.Forward
                         : DfsEdgeType.Cross;
         }
      }


      private static GraphPath<T> getCyclePath(Stack<GraphEdge<T>> currentEdgePath, GraphEdge<T> backEdge) {
         // identify the tail segment of the current path relevant to the detected cycle
         var cycleTail = currentEdgePath.Reverse()
                                        .SkipWhile(e => !ReferenceEquals(e.From, backEdge.To));

         // build the path by adding the back edge
         GraphPath<T> cyclePath = new GraphPath<T>(cycleTail);
         cyclePath.Add(backEdge);
         return cyclePath;
      }
   }
}
