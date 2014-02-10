using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace adt.lib.Graphs {
   [DebuggerDisplay("{From.Value} - {To.Value}")]
   public partial class GraphEdge<T> {
      public GraphVertex<T> From { get; private set; }
      public GraphVertex<T> To { get; private set; }


      public GraphEdge(GraphVertex<T> @from, GraphVertex<T> to) {
         From = @from;
         To = to;
      }
   }
}
