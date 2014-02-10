using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace adt.lib.Graphs {
   internal enum DfsEdgeType {
      Undiscovered,
      Tree,
      Back,
      Forward,
      Cross,
   }


   partial class GraphEdge<T> {
      internal DfsEdgeType DfsType { get; set; }

      internal void DfsInitialize() {
         DfsType = DfsEdgeType.Undiscovered;
      }
   }
}



//
//namespace adt.lib.Graphs.Algorithms {
//   public class DfsEdge : IGraphEdge {
//      public DfsEdge(IGraphEdge sourceEdge, DfsNode dfsFrom, DfsNode dfsTo) {
//         _sourceEdge = sourceEdge;
//         _dfsFrom = dfsFrom;
//         _dfsTo = dfsTo;
//      }
//
//
//      private readonly IGraphEdge _sourceEdge;
//      private readonly DfsNode _dfsFrom;
//      private readonly DfsNode _dfsTo;
//
//      public IGraphNode From { get { return _dfsFrom; } }
//      public IGraphNode To { get { return _dfsTo; } }
//   }
//}
