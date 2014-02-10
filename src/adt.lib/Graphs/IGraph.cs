//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//
//namespace adt.lib.Graphs {
//   public interface IGraph {
//      IEnumerable<IGraphNode> Vertexes { get; }
//      IEnumerable<IGraphEdge> Edges { get; }
//   }
//
//
//
//   public interface IGraphNode {
//      IEnumerable<IGraphNode> VertexesIn { get; }
//      IEnumerable<IGraphNode> VertexesOut { get; }
//
//      IEnumerable<IGraphEdge> EdgesIn { get; }
//      IEnumerable<IGraphEdge> EdgesOut { get; }
//   }
//
//
//
//   public interface IGraphEdge {
//      IGraphNode From { get; }
//      IGraphNode To { get; }
//   }
//}
