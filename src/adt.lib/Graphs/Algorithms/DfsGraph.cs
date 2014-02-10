using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace adt.lib.Graphs {
   partial class Graph<T> {
      internal void DfsInitialize() {
         foreach ( var vertex in _vertexes ) vertex.DfsInitialize();
         foreach ( var edge in _edges ) edge.DfsInitialize();
      }
   }
}



//namespace adt.lib.Graphs.Algorithms {
//   /// <summary>
//   /// The result of the depth-first search algorithm.
//   /// Represents a version of a graph as decorated by the depth-first search of that graph.
//   /// </summary>
//   public class DfsGraph : IGraph {
//      internal DfsGraph(IGraph sourceGraph) {
//         _sourceGraph = sourceGraph;
//      }
//
//
//      private readonly IGraph _sourceGraph;
//
//      private readonly List<DfsNode> _dfsVertexes = new List<DfsNode>();
//      private readonly List<DfsEdge> _dfsEdges = new List<DfsEdge>();
//
//      public IEnumerable<IGraphNode> Vertexes { get { return _dfsVertexes; } }
//      public IEnumerable<IGraphEdge> Edges { get { return _dfsEdges; } }
//
//
//      internal void Initialize() {
//         foreach ( var sourceVertex in _sourceGraph.Vertexes )
//            _dfsVertexes.Add(new DfsNode(sourceVertex));
//
//         foreach ( var sourceEdge in _sourceGraph.Edges ) {
//            DfsNode dfsFrom = addDfsVertex(sourceEdge.From),
//                    dfsTo = addDfsVertex(sourceEdge.To);
//            addDfsEdge(sourceEdge, dfsFrom, dfsTo);
//         }
//      }
//
//
//      private DfsNode addDfsVertex(IGraphNode sourceVertex) {
//         var dfsVertex = _dfsVertexes.FirstOrDefault(v => object.ReferenceEquals(v.SourceVertex, sourceVertex));
//         if ( dfsVertex == null ) {
//            dfsVertex = new DfsNode(sourceVertex);
//            _dfsVertexes.Add(dfsVertex);
//         }
//         return dfsVertex;
//      }
//
//
//      private void addDfsEdge(IGraphEdge sourceEdge, DfsNode dfsFrom, DfsNode dfsTo) {
//         throw new NotImplementedException();
//      }
//   }
//}
