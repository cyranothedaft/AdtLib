using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace adt.lib.Graphs {
   internal enum DfsVertexColor {
      /// <summary>
      /// Indicates that a graph node has not yet been discovered by the depth-first search.
      /// </summary>
      White,

      /// <summary>
      /// Indicates that a graph node has been discovered but not finished by the depth-first search visitation.
      /// </summary>
      Grey,

      /// <summary>
      /// Indicates that a graph node has been finished by the depth-first search visitation.
      /// </summary>
      Black,
   }


   partial class GraphVertex<T> {
      internal DfsVertexColor DfsColor { get; set; }
      internal int DfsDiscoveryTime { get; set; }
      internal int DfsFinishingTime { get; set; }

      internal void DfsInitialize() {
         DfsColor = DfsVertexColor.White;
         DfsDiscoveryTime = 0;
         DfsFinishingTime = 0;
      }
   }
}



//namespace adt.lib.Graphs.Algorithms {
//   public enum DfsVertexColor {
//      White,
//      Grey,
//      Black,
//   }
//
//
//   public class DfsNode : IGraphNode {
//      public DfsNode(IGraphNode sourceVertex) {
//         SourceVertex = sourceVertex;
//      }
//
//
//      internal IGraphNode SourceVertex { get; private set; }
//
//      public DfsVertexColor Color { get; internal set; }
//
//      public IEnumerable<IGraphNode> VertexesIn { get; private set; }
//      public IEnumerable<IGraphNode> VertexesOut { get; private set; }
//      public IEnumerable<IGraphEdge> EdgesIn { get; private set; }
//      public IEnumerable<IGraphEdge> EdgesOut { get; private set; }
//   }
//}
