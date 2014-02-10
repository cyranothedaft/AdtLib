using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace adt.lib.Graphs {
   partial class GraphEdge<T> {
      internal void DfsInitialize() {
         
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
