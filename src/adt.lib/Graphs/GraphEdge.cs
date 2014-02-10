using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace adt.lib.Graphs {
   public partial class GraphEdge<T> {
      public GraphNode<T> From { get; private set; }
      public GraphNode<T> To { get; private set; }


      public GraphEdge(GraphNode<T> @from, GraphNode<T> to) {
         From = @from;
         To = to;
      }
   }
}
